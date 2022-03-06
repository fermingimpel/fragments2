using UnityEngine;

public class CombinableItem : ItemBase
{
    private PlayerController player;
    
    public override void HandleInteraction()
    {
        base.HandleInteraction();
    }

    public override void HandleInteraction(PlayerController player)
    {
        base.HandleInteraction(player);
        this.player = player;
    }

    public override void UseItem()
    {
        if(player)
            player.SetEquippedItem(this);
    }
}