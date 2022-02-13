using System;
using UnityEngine;

public class TriggerTask : TaskBase
{
    private GameObject collidedObject = null;
    protected override void CollectData()
    {
        CheckList.CollidedObject = collidedObject;
        SendData?.Invoke(CheckList);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!ShouldSendData) return;
        collidedObject = other.gameObject;
        CollectData();
    }
}