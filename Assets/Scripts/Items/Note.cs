using UnityEngine.Events;

public class Note : ItemBase
{
    public UnityEvent showNote;
    public override void HandleInteraction()
    {
        base.HandleInteraction();
    }

    public override void UseItem()
    {
        base.UseItem();
        showNote?.Invoke();
    }
}