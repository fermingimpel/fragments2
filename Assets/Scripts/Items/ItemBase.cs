using System;
using Items;
using UnityEngine;

[Serializable]
public class ItemBase : MonoBehaviour, InteractionInterface
{
    [SerializeField] public ItemInfo itemInfo;

    public void HandleInteraction()
    {
        Debug.Log("HOLA PA");
    }

    public void Use()
    {
        Debug.Log("Used: " + gameObject.name);   
    }
    
}