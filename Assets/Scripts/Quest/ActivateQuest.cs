using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateQuest : MonoBehaviour
{
    [SerializeField] private string QuestToActivateName = "";
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            QuestManager.Instance.CompleteActiveQuest();
            QuestManager.Instance.ActivateQuest(QuestManager.Instance.GetQuestByName(QuestToActivateName));
            Destroy(this);
        }
    }
}
