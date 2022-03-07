using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum QuestState
{
    Inactive,
    Active,
    Completed
}


[Serializable]
public class Quest : MonoBehaviour
{
    [Header("Quest Data")] [SerializeField]
    private string questName = "";

    [/*CustomUtils.ReadOnly,*/ SerializeField] private string id = "";
    [/*CustomUtils.ReadOnly,*/ SerializeField] private QuestState state;
    [SerializeField, SerializeReference] private List<ObjectiveBase> objectives = new List<ObjectiveBase>();
    [SerializeField] private bool activateOnStart = false;
    [SerializeField] private bool isRepeatable = true;
    [SerializeField] private string questToActivate = "";
    [SerializeField] private GameObject itemToSpawn;
    [SerializeField] private Transform positionToSpawnItem;

    private string createdId = "";

    private void Awake()
    {
        PuzzleObjective.SendPuzzleCompletion += CheckQuestCompletion;
        SurviveObjective.SendHordeCompletion += CheckQuestCompletion;
    }

    public void ResetQuest()
    {
        state = QuestState.Inactive;
        foreach (var objective in objectives)
        {
            if(objective) objective.ResetObjective();
        }
    }
    
    public string GetName()
    {
        return questName;
    }

    public void UpdateQuest(QuestCheckList checkList)
    {
        for (int i = 0; i < objectives.Count; i++)
        {
            objectives[i].CheckObjectiveCompletion(checkList);
        }

        CheckQuestCompletion();
    }

    protected void CheckQuestCompletion()
    {
        foreach (var objective in objectives)
        {
            if (objective && objective.isCompleted)
                CompleteQuest();
        }
    }
    
    
    protected void CompleteQuest()
    {
        Debug.Log("complete quest " + GetName());
        if (!isRepeatable)
            state = QuestState.Completed;
        else
        {
            state = QuestState.Inactive;
            foreach (ObjectiveBase obj in objectives)
            {
                if (obj)
                {
                    obj.ResetObjective();
                }
            }
        }
    }

    public void SetQuestState(QuestState newState)
    {
        state = newState;
    }

    public QuestState GetQuestState()
    {
        return state;
    }

    public string GetId()
    {
        return id;
    }

    private void Reset()
    {
        if (createdId != "")
            return;
        id = CustomUtils.IdGenerator.GenerateId();
        createdId = id;
    }

    public string GetQuestToActivateName()
    {
        return questToActivate;
    }

    public bool GetActiveOnStart()
    {
        return activateOnStart;
    }

    private void OnDestroy()
    {
        PuzzleObjective.SendPuzzleCompletion -= CheckQuestCompletion;
        SurviveObjective.SendHordeCompletion -= CheckQuestCompletion;
    }

    public GameObject GetItemToSpawn()
    {
        return itemToSpawn;
    }

    public Transform GetItemSpawnPosition()
    {
        return positionToSpawnItem;
    }
}