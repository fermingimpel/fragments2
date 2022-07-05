using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Weapon : MonoBehaviour {

    [SerializeField] float range;

    [SerializeField] float damage;

    [SerializeField] GameObject shootImpactHole;
    [SerializeField] float timeToShoot;
    float timerPreparing = 0f;

    [SerializeField] float horizontalRecoil;
    [SerializeField] float verticalRecoil;

    [SerializeField] float timeToReload;
    float timerReloading = 0f;
    [SerializeField] int totalAmmo;
    [SerializeField] int ammoPerMagazine;
    int actualAmmo;
    int maxAmmo;

    [SerializeField] AudioSource audioSource;
    [SerializeField] List<AudioClip> shootSounds;
    [SerializeField] AudioClip noAmmoSound;
    [SerializeField] AudioClip reloadingSound;
    [SerializeField] AudioClip ammoRefilledSound;

    [SerializeField] Animator animator;

    PlayerCameraMovement recoil;
    [SerializeField] Camera weaponCamera;

    public enum WeaponState {
        Ready, Preparing, Reloading, NoAmmo
    }
    [SerializeField] WeaponState weaponState;

    public enum WeaponSightState {
        Normal, Ads
    }
    [SerializeField] WeaponSightState weaponSightState = WeaponSightState.Normal;

    public static Action AmmoChanged;

    [SerializeField] GameObject carabineSight;
    [SerializeField] bool equipedCarabineSight = false;

    [SerializeField] Vector3 carabineNormalPosition;
    [SerializeField] Vector3 carabineUsingSightPosition;

    [Range(0.01f, Mathf.Infinity)]
    [SerializeField] float timeToChangeAds;

    [SerializeField] float fovSight;
    [SerializeField] float fovNormal;

    [Header("ADS Settings")]
    [SerializeField] private float damageMultiplier;
    [SerializeField] private float rateOfFireAds;
    [SerializeField] AudioClip adsShootClip;


    private float currentRateOfFire = 0;
    private float currentDamageMultiplier = 1;

    void Start() {
        actualAmmo = ammoPerMagazine;
        maxAmmo = totalAmmo;
        AmmoChanged?.Invoke();
        recoil = GetComponentInParent<PlayerCameraMovement>();
        currentRateOfFire = timeToShoot;
    }

    void Update() {
        if (PauseController.instance.IsPaused)
            return;

        switch (weaponState) {
            case WeaponState.Reloading:
                timerReloading += Time.deltaTime;
                if (timerReloading >= timeToReload) {
                    int diff = ammoPerMagazine - actualAmmo;
                    totalAmmo -= diff;

                    if (totalAmmo <= 0) {
                        totalAmmo += ammoPerMagazine;
                        actualAmmo = totalAmmo;
                        totalAmmo = 0;
                    }
                    else
                        actualAmmo = ammoPerMagazine;

                    timerReloading = 0f;
                    weaponState = WeaponState.Ready;
                    AmmoChanged?.Invoke();
                }
                break;
            case WeaponState.Preparing:
                timerPreparing += Time.deltaTime;
                if (timerPreparing >= currentRateOfFire) {
                    timerPreparing = 0f;
                    weaponState = WeaponState.Ready;
                }
                break;
        }
    }

    public void Shoot() {
        if (weaponState != WeaponState.Ready) {
            if (weaponState == WeaponState.NoAmmo)
                audioSource.PlayOneShot(noAmmoSound);

            return;
        }

        animator.SetTrigger("Shoot");

        if (weaponSightState == WeaponSightState.Normal)
            audioSource.PlayOneShot(shootSounds[UnityEngine.Random.Range(0, shootSounds.Count)]);
        else
            audioSource.PlayOneShot(adsShootClip);

        if (Camera.main != null) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, range)) {
                if (hit.collider.CompareTag("Enemy")) {
                    Enemy e = hit.transform.GetComponent<Enemy>();
                    if (e != null)
                        e.Hit(damage * currentDamageMultiplier, hit.point + (hit.normal * 0.1f), transform.position);
                }
                else if (hit.collider.CompareTag("Map")) {
                    GameObject hole = Instantiate(shootImpactHole, hit.point + hit.normal * 0.001f, Quaternion.LookRotation(hit.normal));
                    Destroy(hole, 5f);
                }
                else if (hit.collider.CompareTag("Interactable")) {
                    PuzzleInteractable pi = hit.collider.GetComponent<PuzzleInteractable>();
                    if (pi != null)
                        pi.Interact();
                }
            }
        }

        if (recoil)
            recoil.AddRecoil(verticalRecoil, UnityEngine.Random.Range(-horizontalRecoil, horizontalRecoil));

        weaponState = WeaponState.Preparing;

        actualAmmo--;
        if (actualAmmo <= 0) {
            actualAmmo = 0;
            weaponState = WeaponState.NoAmmo;
        }

        AmmoChanged?.Invoke();
    }

    public void Reload() {
        if (weaponState == WeaponState.Reloading || totalAmmo <= 0 || actualAmmo == ammoPerMagazine)
            return;

        animator.SetTrigger("Reload");

        audioSource.PlayOneShot(reloadingSound);
        weaponState = WeaponState.Reloading;
        timerReloading = 0f;
    }

    public void UseSight() {
        if (!equipedCarabineSight)
            return;

        if (weaponSightState == WeaponSightState.Ads)
            return;

        weaponSightState = WeaponSightState.Ads;

        StopCoroutine(ChangeWeaponPosition(Vector3.zero, 0));

        transform.localPosition = carabineNormalPosition;
        if (Camera.main != null) Camera.main.fieldOfView = fovNormal;
        weaponCamera.fieldOfView = fovNormal;
        currentRateOfFire = rateOfFireAds;
        currentDamageMultiplier = damageMultiplier;

        StartCoroutine(ChangeWeaponPosition(carabineUsingSightPosition, fovSight));
    }

    public void ReleaseSight() {
        if (!equipedCarabineSight)
            return;


        if (weaponSightState == WeaponSightState.Normal)
            return;

        weaponSightState = WeaponSightState.Normal;

        StopCoroutine(ChangeWeaponPosition(Vector3.zero, 0));

        transform.localPosition = carabineUsingSightPosition;
        if (Camera.main != null) Camera.main.fieldOfView = fovSight;
        weaponCamera.fieldOfView = fovSight;
        currentRateOfFire = timeToShoot;
        currentDamageMultiplier = 1;

        StartCoroutine(ChangeWeaponPosition(carabineNormalPosition, fovNormal));
    }

    IEnumerator ChangeWeaponPosition(Vector3 pos, float fov) {

        float t = 0;

        if (Camera.main != null) {
            float initialFov = Camera.main.fieldOfView;
            Vector3 initialPos = transform.localPosition;

            while (transform.localPosition != pos) {
                if (PauseController.instance.IsPaused)
                    yield return new WaitForEndOfFrame();
                else {
                    transform.localPosition = Vector3.Lerp(initialPos, pos, t);
                    Camera.main.fieldOfView = Mathf.Lerp(initialFov, fov, t);
                    weaponCamera.fieldOfView = Mathf.Lerp(initialFov, fov, t);

                    t += Time.deltaTime * (1f / timeToChangeAds);
                    yield return new WaitForEndOfFrame();
                }
                yield return null;
            }
        }

        yield return null;
    }

    public void AddAmmo(int value) {
        totalAmmo += value;
        if (totalAmmo >= maxAmmo)
            totalAmmo = maxAmmo;
        audioSource.PlayOneShot(ammoRefilledSound);
        AmmoChanged?.Invoke();
    }

    public void EnableAccesories() {
        equipedCarabineSight = true;
        carabineSight.SetActive(true);
    }

    public int GetActualAmmo() {
        return actualAmmo;
    }
    public int GetTotalAmmo() {
        return totalAmmo;
    }
    public int GetMaxAmmo() {
        return maxAmmo;
    }
    public int GetAmmoPerMagazine() {
        return ammoPerMagazine;
    }

    public WeaponSightState GetSightState() {
        return weaponSightState;
    }

    public bool GetEquipedCarabineSight()
    {
        return equipedCarabineSight;
    }


}
