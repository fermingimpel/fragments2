using System;
using UnityEngine;

public class PickUpObjective : ObjectiveBase
{
    [SerializeField] private GameObject neededObject;
    private ItemBase item = null;

    private void Start()
    {
        item = neededObject.GetComponent<ItemBase>();
        if (!item)
            item = neededObject.GetComponentInChildren<ItemBase>();
    }

    public override void CheckObjectiveCompletion(QuestCheckList listToCompare)
    {
        base.CheckObjectiveCompletion(listToCompare);

        if (item && listToCompare.InteractedObject.itemInfo.item.name == item.itemInfo.item.name)
        {

            isCompleted = true;
        }
    }
}