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

    public enum WeaponState {
        Ready, Preparing, Reloading, NoAmmo
    }
    [SerializeField] WeaponState weaponState;

    public static Action AmmoChanged;

    void Start() {
        actualAmmo = ammoPerMagazine;
        maxAmmo = totalAmmo;
        AmmoChanged?.Invoke();
        recoil = GetComponentInParent<PlayerCameraMovement>();
    }

    void Update() {
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
                if(timerPreparing >= timeToShoot) {
                    timerPreparing = 0f;
                    weaponState = WeaponState.Ready;
                }
                break;
        }
    }

    public void Shoot() {
        if (weaponState != WeaponState.Ready) {
            if(weaponState == WeaponState.NoAmmo)
                audioSource.PlayOneShot(noAmmoSound);

            return;
        }

        animator.SetTrigger("Shoot");

        audioSource.PlayOneShot(shootSounds[UnityEngine.Random.Range(0, shootSounds.Count)]);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, range)) {
            if (hit.collider.CompareTag("Enemy")) {
                Enemy e = hit.transform.GetComponent<Enemy>();
                if(e != null) 
                    e.Hit(damage, hit.point + (hit.normal * 0.1f), transform.position);
            }
            else if (hit.collider.CompareTag("Map")) {
                GameObject hole = Instantiate(shootImpactHole, hit.point + hit.normal * 0.001f, Quaternion.LookRotation(hit.normal));
                Destroy(hole, 5f);
            }    
        }

        if(recoil)
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

    public void AddAmmo(int value) {
        totalAmmo += value;
        if (totalAmmo >= maxAmmo)
            totalAmmo = maxAmmo;
        audioSource.PlayOneShot(ammoRefilledSound);
        AmmoChanged?.Invoke();
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




}
