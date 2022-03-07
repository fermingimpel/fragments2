using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour {

    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] TextMeshProUGUI ammoTotalText;
    [SerializeField] GameObject reload;
    [SerializeField] GameObject crosshair;
    [SerializeField] GameObject gameplayHUD;

    [SerializeField] Image healthPanel;
    [SerializeField] Color healthPanelColor;

    [SerializeField] Image equipedItemImage;

    public void ChangeAmmoText(int actualAmmo, int ammoPerMagazine, int maxAmmo) {
        ammoText.text = actualAmmo.ToString();
        ammoTotalText.text = " / " + maxAmmo;

        if (actualAmmo == ammoPerMagazine)
            ammoText.color = Color.cyan;
        else if (actualAmmo != ammoPerMagazine && actualAmmo > 0)
            ammoText.color = Color.white;
        else
            ammoText.color = Color.red;

        if( ((float)actualAmmo / (float)ammoPerMagazine) <= 0.25f) {
            if(actualAmmo <= 0) {
                crosshair.SetActive(false);
                reload.SetActive(true);
            }
        }
        else {
            crosshair.SetActive(true);
            reload.SetActive(false);
        }
    }

    public void UpdateHealthRedScreen(float actualHP, float maxHP) {
        float alpha = (maxHP - actualHP) / maxHP;
        healthPanel.color = new Color(healthPanelColor.r, healthPanelColor.g, healthPanelColor.b, alpha);
    }

    public void SetGameplayHUD(bool value) {
        gameplayHUD.SetActive(value);
    }

    public void SetEquipedItem(Sprite value) {
        if(value == null) {
            equipedItemImage.gameObject.SetActive(false);
            return;
        }

        equipedItemImage.gameObject.SetActive(true);
        equipedItemImage.sprite = value;
        equipedItemImage.color = Color.white;

    }
}
