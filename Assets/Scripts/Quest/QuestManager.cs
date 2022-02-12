using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private List<Quest> Quests = new List<Quest>();
    [CustomUtils.ReadOnly, SerializeField] private List<Quest> ActiveQuests = new List<Quest>();
    [SerializeField] private int MaxActiveQuest;

    void Start()
    {
        for (int i = 0; i < Quests.Count; i++)
        {
            if (ActiveQuests.Count <= MaxActiveQuest)
            {
                ActivateQuest(Quests[i]);
            }
        }
    }

    public Quest GetQuestById(string id)
    {
        foreach (Quest quest in Quests)
        {
            if (quest && quest.GetId() == id)
            {
                return quest;
            }
        }

        Debug.LogError("Couldn't find quest");
        return null;
    }

    void ActivateQuest(Quest quest)
    {
        if (quest && quest.GetQuestState() == QuestState.Inactive && ActiveQuests.Count <= MaxActiveQuest)
        {
            quest.SetQuestState(QuestState.Active);
            ActiveQuests.Add(quest);
        }
    }

    void AddQuest(Quest quest)
    {
        if (quest)
        {
            Quests.Add(quest);
        }
    }
     
}