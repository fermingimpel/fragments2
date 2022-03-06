using System;
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

    private static QuestManager instance;

    public static QuestManager Instance => instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        else
            Destroy(gameObject);
    }

    private void Start()
    {
        for (int i = 0; i < Quests.Count; i++)
        {
            if (ActiveQuests.Count <= MaxActiveQuest && Quests[i].GetActiveOnStart())
            {
                ActivateQuest(Quests[i]);
            }
        }
        PuzzleObjective.SendPuzzleCompletion += CheckQuestState;
        SurviveObjective.SendHordeCompletion += CheckQuestState;
    }

    public void UpdateQuests(QuestCheckList checkList)
    {
        string questToActivate = "";
        for (int i = 0; i < ActiveQuests.Count; i++)
        {
            if (!ActiveQuests[i]) return;

            ActiveQuests[i].UpdateQuest(checkList);


            if (ActiveQuests[i].GetQuestState() == QuestState.Completed)
            {
                questToActivate = ActiveQuests[i].GetQuestToActivateName();
                if(ActiveQuests[i].GetItemToSpawn())
                    Instantiate(ActiveQuests[i].GetItemToSpawn(),ActiveQuests[i].GetItemSpawnPosition().position, Quaternion.identity);
                
                ActiveQuests.RemoveAt(i);
                i--;
            }
        }

        if (GetQuestByName(questToActivate))
            ActivateQuest(GetQuestByName(questToActivate));
        else
            SetQuestUIText?.Invoke("");

        if (ActiveQuests.Count <= 0)
        {
            ReceiveData?.Invoke(false);
        }
    }

    private void CheckQuestState()
    {
        string questToActivate = "";
        for (int i = 0; i < ActiveQuests.Count; i++)
        {
            if (!ActiveQuests[i]) return;

            if (ActiveQuests[i].GetQuestState() == QuestState.Completed)
            {
                questToActivate = ActiveQuests[i].GetQuestToActivateName();
                if (ActiveQuests[i].GetItemToSpawn())
                {
                    GameObject item = Instantiate(ActiveQuests[i].GetItemToSpawn(),ActiveQuests[i].GetItemSpawnPosition().localPosition, Quaternion.Euler(0,90,0), ActiveQuests[i].GetItemSpawnPosition());
                    item.transform.localPosition = Vector3.zero;                                    
                }
                
                ActiveQuests.RemoveAt(i);
                i--;
            }
        }

        if (GetQuestByName(questToActivate))
            ActivateQuest(GetQuestByName(questToActivate));
        else
            SetQuestUIText?.Invoke("");

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

    public Quest GetQuestByName(string questName)
    {
        foreach (Quest quest in Quests.Where(quest => quest))
        {
            if (quest.GetName() == questName)
                return quest;
        }

        Debug.Log("Couldn't find quest");
        return null;
    }

    public void ActivateQuest(Quest quest)
    {
        if (!quest || quest.GetQuestState() != QuestState.Inactive || ActiveQuests.Count > MaxActiveQuest) return;
        quest.SetQuestState(QuestState.Active);
        ActiveQuests.Add(quest);
        ReceiveData?.Invoke(true);
        if (quest)
            SetQuestUIText?.Invoke(quest.GetName());
        else
            SetQuestUIText?.Invoke("");
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

    public List<Quest> GetActiveQuest()
    {
        return ActiveQuests;
    }

    private void OnDestroy()
    {
        PuzzleObjective.SendPuzzleCompletion -= CheckQuestState;
        SurviveObjective.SendHordeCompletion -= CheckQuestState;
    }
}