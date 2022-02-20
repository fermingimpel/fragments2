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

    [CustomUtils.ReadOnly, SerializeField] private string id = "";
    [CustomUtils.ReadOnly, SerializeField] private QuestState state;
    [SerializeField, SerializeReference] private List<ObjectiveBase> objectives = new List<ObjectiveBase>();
    [SerializeField] private bool isRepeatable = true;

    private string createdId = "";

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
        if (objectives.Any(objective => !objective.isCompleted))
            return;

        CompleteQuest();
    }

    protected void CompleteQuest()
    {
        state = QuestState.Completed;
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
}