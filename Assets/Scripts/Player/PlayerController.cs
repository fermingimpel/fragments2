using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;


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

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip playerDeathClip;

    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerCameraMovement playerCameraMovement;
    [SerializeField] PlayerHUD playerHUD;
    [SerializeField] Animator animator;
    [SerializeField] Inventory inventory;

    [SerializeField] AudioSource audioSourceHeart;

    private ItemBase equippedItem = null;

    public static Action PlayerDead;
    
    public UnityEvent CheckQuest;
    public static UnityAction<PlayerController> TakeDamage;
    
    void Awake() {
        Weapon.AmmoChanged += UpdateAmmoHUD;
        PauseController.Pause += Pause;
    }
    void Start() {
        actualHealth = maxHealth;
        timerHeal = 0f;

        GetComponentInChildren<InitialCutScene>().enabled = true;
        GetComponentInChildren<InitialCutScene>().StartInitialCutScene();
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
                    if (actualHealth >= (maxHealth / 2f) && audioSourceHeart.isPlaying) {
                        audioSourceHeart.Stop();
                    }

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

        if (Input.GetKeyDown(KeyCode.J))
        {
            CheckQuest?.Invoke();
        }

        if (weapon) {
            if (Input.GetMouseButtonDown(0))
                weapon.Shoot();
            if (Input.GetKeyDown(KeyCode.R))
                weapon.Reload();

            if (Input.GetMouseButton(1) && weapon.GetEquipedCarabineSight()) {
                weapon.UseSight();
                playerHUD.SetEnabledCrosshair(false);
            }
            else {
                weapon.ReleaseSight();
                playerHUD.SetEnabledCrosshair(true);
            }

            if (weapon.GetSightState() == Weapon.WeaponSightState.Ads) {
                playerCameraMovement.ReduceSensitivity();
                playerMovement.ReduceSpeed();
            }
            else {
                playerCameraMovement.ResetSensitivity();
                playerMovement.ResetSpeed();
            }
        }
    }


    public void Hit(float damage) {
        if (playerState == PlayerState.Dead)
            return;

        healState = HealState.NotHealing;
        timerHeal = 0f;

        actualHealth -= damage;

        if (actualHealth <= maxHealth / 2.5f && !audioSourceHeart.isPlaying)
            audioSourceHeart.Play();

        if (actualHealth <= 0f) {
            PlayerDead?.Invoke();
            audioSourceHeart.Stop();

            audioSource.PlayOneShot(playerDeathClip);
            actualHealth = 0f;
            playerState = PlayerState.Dead;
            TakeDamage?.Invoke(this);
            
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

        inventory.gameObject.SetActive(true);
        playerHUD.SetGameplayHUD(true);
    }

    public void SetEquippedItem(ItemBase newItem) {
        equippedItem = newItem;
        if (newItem == null)
            playerHUD.SetEquipedItem(null);
        else
            playerHUD.SetEquipedItem(equippedItem.itemInfo.item.inventoryImage);
    }

    public ItemBase GetEquippedItem()
    {
        return equippedItem;
    }

    public void PlayClip(AudioClip clip) {
        audioSource.PlayOneShot(clip);
    }
}
