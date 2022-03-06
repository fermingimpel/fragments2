using System;
using Items;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class ItemBase : MonoBehaviour, InteractionInterface
{
    [SerializeField] public ItemInfo itemInfo;
    public static UnityAction<ItemBase> PickUp;

    public virtual void HandleInteraction()
    {   
        PickUp?.Invoke(this);
    }

    public virtual void HandleInteraction(PlayerController player)
    {
        
    }

    public virtual void UseItem()
    {
        
    }

    public virtual void UsedItem()
    {
        
    }
    
}