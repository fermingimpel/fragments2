using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public enum WeaponState {
        Ready, Preparing, Reloading, NoAmmo
    }
    [SerializeField] WeaponState weaponState;

    void Start() {
        actualAmmo = ammoPerMagazine;
        maxAmmo = totalAmmo;
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

        audioSource.PlayOneShot(shootSounds[Random.Range(0, shootSounds.Count)]);

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

        weaponState = WeaponState.Preparing;

        actualAmmo--;
        if (actualAmmo <= 0)
            weaponState = WeaponState.NoAmmo;
    }

    public void Reload() {
        if (weaponState == WeaponState.Reloading || totalAmmo <= 0 || actualAmmo == ammoPerMagazine)
            return;

        audioSource.PlayOneShot(reloadingSound);
        weaponState = WeaponState.Reloading;
        timerReloading = 0f;
    }

    public void AddAmmo(int value) {
        totalAmmo += value;
        if (totalAmmo >= maxAmmo)
            totalAmmo = maxAmmo;
        audioSource.PlayOneShot(ammoRefilledSound);
    }

    public int GetMaxAmmo() {
        return maxAmmo;
    }

    public int GetTotalAmmo() {
        return totalAmmo;
    }


}
