using UnityEngine.Events;
using UnityEngine;
public class Note : ItemBase
{
    public UnityEvent showNote;
    public bool openFirstTime = true;
    [SerializeField] AudioClip clip;

    public override void HandleInteraction()
    {
        FindObjectOfType<PlayerController>().PlayClip(clip);
        base.HandleInteraction();
    }

    public override void UseItem()
    {
        openFirstTime = false;
        base.UseItem();
        showNote?.Invoke();
    }
}