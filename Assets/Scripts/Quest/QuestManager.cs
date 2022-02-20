using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private List<Quest> Quests = new List<Quest>();
    [CustomUtils.ReadOnly, SerializeField] private List<Quest> ActiveQuests = new List<Quest>();
    [SerializeField] private int MaxActiveQuest;

    public static UnityAction<bool> ReceiveData;
    public static UnityAction<string> SetQuestUIText;


    private void Start()
    {
        for (int i = 0; i < Quests.Count; i++)
        {
            if (ActiveQuests.Count <= MaxActiveQuest)
            {
                ActivateQuest(Quests[i]);
            }
        }
    }

    public void UpdateQuests(QuestCheckList checkList)
    {
        for(int i = 0; i < ActiveQuests.Count; i++)
        {
            if (!ActiveQuests[i]) return;
            
            ActiveQuests[i].UpdateQuest(checkList);
            
            if (ActiveQuests[i].GetQuestState() == QuestState.Completed)
            {
                ActiveQuests.RemoveAt(i);
                i--;
            }
        }

        if (ActiveQuests.Count <= 0)
        {
            ReceiveData?.Invoke(false);
        }
    }

    public Quest GetQuestById(string id)
    {
        foreach (Quest quest in Quests.Where(quest => quest))
        {
            if (quest.GetId() == id)
                return quest;
            
        }

        Debug.LogError("Couldn't find quest");
        return null;
    }

    public void ActivateQuest(Quest quest)
    {
        if (!quest || quest.GetQuestState() != QuestState.Inactive || ActiveQuests.Count > MaxActiveQuest) return;
        
        SetQuestUIText?.Invoke(quest.GetName());
        quest.SetQuestState(QuestState.Active);
        ActiveQuests.Add(quest);
        ReceiveData?.Invoke(true);
    }

    public void AddQuest(Quest quest)
    {
        if (quest)
        {
            Quests.Add(quest);
        }
    }

    public Quest GetFirstActiveQuest()
    {
        return ActiveQuests[0];
    }
     
}