using UnityEngine;


public class InteractionTask : TaskBase
{
    private ItemBase interactedObject;
    protected override void Start()
    {
        base.Start();
        InteractionController.InteractedObject += SetInteractedObject;
    }

    protected override void CollectData()
    {
        CheckList.InteractedObject = interactedObject;
        SendData(CheckList);
    }

    void SetInteractedObject(ItemBase newInteractedObject)
    {
        if (!ShouldSendData && newInteractedObject)
            return;
        interactedObject = newInteractedObject;
        CollectData();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        InteractionController.InteractedObject -= SetInteractedObject;

    }
}