using UnityEngine;
using UnityEngine.Events;

public abstract class TaskBase : MonoBehaviour
{
    protected QuestCheckList CheckList;

    public static UnityAction<QuestCheckList> SendData;

    protected bool ShouldSendData = false;

    private void Start()
    {
        QuestEvaluator.ShouldSendData += SetShouldSendData;
    }
    
    void SetShouldSendData(bool value)
    {
        ShouldSendData = value;
        if(!value)
            CheckList.Reset();
    }

    private void OnDestroy()
    {
        QuestEvaluator.ShouldSendData -= SetShouldSendData;
    }

    protected abstract void CollectData();
}
