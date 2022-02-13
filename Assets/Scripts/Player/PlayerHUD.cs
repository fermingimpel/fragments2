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

    [SerializeField] Image healthPanel;
    [SerializeField] Color healthPanelColor;

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

}
