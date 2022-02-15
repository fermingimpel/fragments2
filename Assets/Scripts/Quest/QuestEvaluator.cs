using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestEvaluator : MonoBehaviour
{
    [SerializeField] private QuestManager questManager;
    public static UnityAction<bool> ShouldSendData;

    private void Start()
    {
        TaskBase.SendData += SendData;
        QuestManager.ReceiveData += GetShouldSendData;
    }

    private void GetShouldSendData(bool value)
    {
        ShouldSendData?.Invoke(value);
    }

    private void SendData(QuestCheckList checkList)
    {
        questManager.UpdateQuests(checkList);
    }

    private void OnDestroy()
    {
        TaskBase.SendData -= SendData;
        QuestManager.ReceiveData -= GetShouldSendData;
    }
}