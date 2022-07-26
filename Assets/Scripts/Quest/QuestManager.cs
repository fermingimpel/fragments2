using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Debug = UnityEngine.Debug;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private List<Quest> Quests = new List<Quest>();
    [/*CustomUtils.ReadOnly, */SerializeField] private List<Quest> ActiveQuests = new List<Quest>();
    [SerializeField] private int MaxActiveQuest;

    public static UnityAction<bool> ReceiveData;
    public static UnityAction<string> SetQuestUIText;

    private static QuestManager instance;
    private bool isQuestHidden = false;
    private float timer;
    private float timeToHideQuestUI = .5f;

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

    private void Update()
    {
        if (ActiveQuests.Count <= 0)
        {
            if (!isQuestHidden)
            {
                if (timer >= timeToHideQuestUI)
                {
                    SetQuestUIText?.Invoke("");
                    isQuestHidden = true;
                }
                timer += Time.deltaTime;
            }
        }
        else
        {
            timer = 0;
        }
    }

    public void UpdateQuests(QuestCheckList checkList)
    {
        bool completedQuest = false;
        string questToActivate = "";
        for (int i = 0; i < ActiveQuests.Count; i++)
        {
            if (!ActiveQuests[i]) return;

            ActiveQuests[i].UpdateQuest(checkList);

            if (ActiveQuests[i].GetQuestState() == QuestState.Completed)
            {
                completedQuest = true;
                questToActivate = ActiveQuests[i].GetQuestToActivateName();
                if (ActiveQuests[i].GetItemToSpawn())
                    Instantiate(ActiveQuests[i].GetItemToSpawn(), ActiveQuests[i].GetItemSpawnPosition().position,
                        Quaternion.identity);
                if (ActiveQuests[i].GetIsRepeatable())
                    ActiveQuests[i].SetQuestState(QuestState.Inactive);
                ActiveQuests.RemoveAt(i);
                i--;
            }
        }

        if (completedQuest)
        {
            if (GetQuestByName(questToActivate))
                ActivateQuest(GetQuestByName(questToActivate));
            else
                SetQuestUIText?.Invoke("");  
        }

        if (ActiveQuests.Count <= 0)
        {
            ReceiveData?.Invoke(false);
        }
    }

    private void CheckQuestState()
    {
        string questToActivate = "";
        bool completed = false;
        for (int i = 0; i < ActiveQuests.Count; i++)
        {
            if (!ActiveQuests[i]) return;

            if (ActiveQuests[i].GetQuestState() == QuestState.Completed)
            {
                completed = true;
                questToActivate = ActiveQuests[i].GetQuestToActivateName();
                if (ActiveQuests[i].GetItemToSpawn())
                {
                    GameObject item = Instantiate(ActiveQuests[i].GetItemToSpawn(),
                        ActiveQuests[i].GetItemSpawnPosition().localPosition, Quaternion.Euler(0, 90, 0),
                        ActiveQuests[i].GetItemSpawnPosition());
                    item.transform.localPosition = Vector3.zero;
                }

                if (ActiveQuests[i].GetIsRepeatable())
                    ActiveQuests[i].SetQuestState(QuestState.Inactive);
                ActiveQuests.RemoveAt(i);
                i--;
            }
        }

        if (completed)
        {
            if (GetQuestByName(questToActivate))
                ActivateQuest(GetQuestByName(questToActivate));
            else if (GetActiveQuest().Count <= 0)
                SetQuestUIText?.Invoke("");
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

    public Quest GetQuestByName(string questName)
    {
        foreach (Quest quest in Quests.Where(quest => quest))
        {
            if (quest.GetName() == questName)
                return quest;
        }

        return null;
    }

    public void ActivateQuest(Quest quest)
    {
        if (!quest || quest.GetQuestState() != QuestState.Inactive || ActiveQuests.Count >= MaxActiveQuest) return;
        isQuestHidden = false;
        quest.SetQuestState(QuestState.Active);
        ActiveQuests.Add(quest);
        ReceiveData?.Invoke(true);
        if (quest)
            SetQuestUIText?.Invoke(quest.GetName());
        else
            SetQuestUIText?.Invoke("");
    }

    public void CompleteActiveQuest()
    {
        string questToActivate = "";
        for (int i = 0; i < ActiveQuests.Count; i++)
        {
            if (ActiveQuests[i])
            {
                ActiveQuests[i].CompleteQuest();    
                if (ActiveQuests[i].GetQuestState() == QuestState.Completed)
                {
                    questToActivate = ActiveQuests[i].GetQuestToActivateName();
                    if (questToActivate == "" && !ActiveQuests[i].GetIsRepeatable())
                        questToActivate = "Explore";
                    if (ActiveQuests[i].GetItemToSpawn())
                    {
                        GameObject item = Instantiate(ActiveQuests[i].GetItemToSpawn(),
                            ActiveQuests[i].GetItemSpawnPosition().localPosition, Quaternion.Euler(0, 90, 0),
                            ActiveQuests[i].GetItemSpawnPosition());
                        item.transform.localPosition = Vector3.zero;
                    }

                    if (ActiveQuests[i].GetIsRepeatable())
                    {
                        Debug.Log("Quest " + ActiveQuests[i].GetName() + "is repeatable");
                        ActiveQuests[i].SetQuestState(QuestState.Inactive);
                    }
                    ActiveQuests.RemoveAt(i);
                    i--;
                }
            }
        }
        Debug.Log("Completed quest, activating: " + questToActivate);
        if (GetQuestByName(questToActivate))
            ActivateQuest(GetQuestByName(questToActivate));
        // else
        //     SetQuestUIText?.Invoke("");
    }

    public void ClearActiveQuests()
    {
        ActiveQuests.Clear();
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