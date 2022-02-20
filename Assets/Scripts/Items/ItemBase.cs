using UnityEngine;

public class ItemBase : MonoBehaviour, InteractionInterface
{
    public string itemName = "";
    public int id = 0;
    
    public virtual void HandleInteraction() { }
}