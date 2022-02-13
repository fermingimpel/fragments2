using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCounterTask : TaskBase
{
    private void Update()
    {
        if (!ShouldSendData) return;
        CollectData();
    }

    protected override void CollectData()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            CheckList.KillCount++;
            SendData(CheckList);
        }
    }
}
