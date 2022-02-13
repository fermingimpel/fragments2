using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum QuestState
{
    Inactive, Active, Completed
}

[Serializable]
class Objective
{ 
    public string description;
    private int currentStep;
    public int stepsToComplete;
    public bool isCompleted;

    public QuestCheckList List;

    public void CheckObjectiveCompletion(QuestCheckList listToCompare)
    {
        if (listToCompare.KillCount>=List.KillCount && listToCompare.DistanceTraveled >= List.DistanceTraveled &&
            listToCompare.InteractedObject == List.InteractedObject && listToCompare.CollidedObject == List.CollidedObject &&
            listToCompare.ObjectCreated == List.ObjectCreated)
        {
            isCompleted = true;
        }
    }

}
[Serializable]
public class Quest : MonoBehaviour
{
    [Header("Quest Data")]
    [SerializeField] private string questName = "";
    [CustomUtils.ReadOnly, SerializeField] private string id = "";
    [CustomUtils.ReadOnly, SerializeField] private QuestState state;
    [SerializeField] private List<Objective> objectives = new List<Objective>();

    private string createdId = "";


    public void UpdateQuest(QuestCheckList checkList)
    {
        foreach (var objetive in objectives)
        {
            objetive.CheckObjectiveCompletion(checkList);
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
