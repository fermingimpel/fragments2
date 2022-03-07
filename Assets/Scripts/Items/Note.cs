using UnityEngine.Events;
using UnityEngine;
public class Note : ItemBase
{
    public UnityEvent showNote;

    [SerializeField] AudioClip clip;

    public override void HandleInteraction()
    {
        FindObjectOfType<PlayerController>().PlayClip(clip);
        base.HandleInteraction();
    }

    public override void UseItem()
    {
        base.UseItem();
        showNote?.Invoke();
    }
}