using UnityEngine;

public class CombinableItem : ItemBase
{
    private PlayerController player;

    [SerializeField] AudioClip clip;

    public override void HandleInteraction()
    {
        FindObjectOfType<PlayerController>().PlayClip(clip);
        base.HandleInteraction();
    }

    public override void HandleInteraction(PlayerController player)
    {
        FindObjectOfType<PlayerController>().PlayClip(clip);
        base.HandleInteraction(player);
        this.player = player;
    }

    public override void UseItem()
    {
        if(player)
            player.SetEquippedItem(this);
    }

    public override void UnequippedItem() {
        if (player)
            player.SetEquippedItem(null);
    }
}