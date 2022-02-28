using UnityEngine;

public class Note : ItemBase
{
    public override void HandleInteraction()
    {
        base.HandleInteraction();
    }

    public override void Use()
    {
        base.Use();
        Debug.Log("Read "+ itemInfo.item.name);
    }
}