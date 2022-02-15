using UnityEngine;

public class KillCounterTask : TaskBase
{
    private void Update()
    {
        if (!ShouldSendData) return;
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            CollectData();
        }
    }

    protected override void Start() 
    {
        base.Start();
    }
    protected override void CollectData()
    {
        CheckList.KillCount++;
        SendData(CheckList);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}