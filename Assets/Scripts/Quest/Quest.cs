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
    public string description = "";
    private int currentStep=0;
    public int stepsToComplete=1;
    public bool isCompleted = false;

    public QuestCheckList list = new QuestCheckList();

    public void CheckObjectiveCompletion(QuestCheckList listToCompare)
    {
        if (listToCompare.KillCount>=list.KillCount && listToCompare.DistanceTraveled >= list.DistanceTraveled &&
            listToCompare.InteractedObject == list.InteractedObject && listToCompare.CollidedObject == list.CollidedObject &&
            listToCompare.ObjectCreated == list.ObjectCreated)
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
    [SerializeField] private List<Objective> objectives;

    private string createdId = "";


    public void UpdateQuest(QuestCheckList checkList)
    {
        for(int i = 0;i < objectives.Count; i++)
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
