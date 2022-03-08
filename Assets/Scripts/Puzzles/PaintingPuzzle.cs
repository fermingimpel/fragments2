using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingPuzzle : MonoBehaviour, PuzzleInteractable
{
    [SerializeField]  private Weapon weapon;

    public void Interact()
    {
        if (weapon.GetSightState() == Weapon.WeaponSightState.Ads)
        {
            Debug.Log("Complete quest");
            QuestManager.Instance.CompleteActiveQuest();
        }
    }
}
