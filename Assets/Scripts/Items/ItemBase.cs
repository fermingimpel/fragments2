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
        Debug.Log("PickUp");
        PickUp?.Invoke(this);
    }

    public virtual void Use()
    {
        //Mostrar nota en pantalla
    }
    
}