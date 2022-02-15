using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] LayerMask layerDoors;
    [SerializeField] LayerMask layerKeys;
   
    [SerializeField] Weapon weapon;

    [SerializeField] float actualHealth;
    [SerializeField] float maxHealth;
    [SerializeField] float healthPerSecond;
    [SerializeField] float timeToStarHealing;
    float timerHeal = 0f;
    public enum HealState {
        Healing, NotHealing
    }
    [SerializeField] HealState healState;

    public enum PlayerState {
        Alive, Dead
    }
    [SerializeField] PlayerState playerState;
    
    
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerCameraMovement playerCameraMovement;
    [SerializeField] PlayerHUD playerHUD;
    [SerializeField] Animator animator;

    void Start() {
        actualHealth = maxHealth;
        timerHeal = 0f;
        Weapon.AmmoChanged += UpdateAmmoHUD;
        PauseController.Pause += Pause;
    }

    void OnDestroy() {
        Weapon.AmmoChanged -= UpdateAmmoHUD;
        PauseController.Pause -= Pause;
    }

    void OnDisable() {
        Weapon.AmmoChanged -= UpdateAmmoHUD;
        PauseController.Pause -= Pause;
    }

    void Update() {
        if (PauseController.instance.IsPaused)
            return;

        if (playerState == PlayerState.Dead)
            return;

        if(actualHealth != maxHealth) 
            switch (healState) {
                case HealState.Healing:
                    actualHealth += healthPerSecond * Time.deltaTime;
                    if (actualHealth >= maxHealth) {
                        actualHealth = maxHealth;
                        healState = HealState.NotHealing;
                    }
                    playerHUD.UpdateHealthRedScreen(actualHealth, maxHealth);
                    break;
                case HealState.NotHealing:
                    timerHeal += Time.deltaTime;
                    if (timerHeal >= timeToStarHealing)
                        healState = HealState.Healing;                  
                    break;
            }


        if (weapon) {
            if (Input.GetMouseButtonDown(0))
                weapon.Shoot();
            if (Input.GetKeyDown(KeyCode.R))
                weapon.Reload();
        }

    }


    public void Hit(float damage) {
        if (playerState == PlayerState.Dead)
            return;

        healState = HealState.NotHealing;
        timerHeal = 0f;

        actualHealth -= damage;
        if (actualHealth <= 0f) {
            actualHealth = 0f;
            playerState = PlayerState.Dead;

            weapon.gameObject.SetActive(false);
            playerMovement.SetCanMove(false);
            playerCameraMovement.SetCanMove(false);
            playerHUD.SetGameplayHUD(false);
            animator.SetTrigger("Death");
        }

        playerHUD.UpdateHealthRedScreen(actualHealth, maxHealth);
    }

    public Weapon GetActualWeapon() {
        return weapon;
    }

    void UpdateAmmoHUD() {
        playerHUD.ChangeAmmoText(weapon.GetActualAmmo(), weapon.GetAmmoPerMagazine(), weapon.GetTotalAmmo());
    }

    void Pause() {
        if (PauseController.instance.IsPaused) {
            playerHUD.SetGameplayHUD(false);
            return;
        }

        playerHUD.SetGameplayHUD(true);
    }
}
