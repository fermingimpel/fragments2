using System;
using System.Collections.Generic;
using UnityEngine;

public enum QuestState
{
    Inactive, Active, Completed
}

[Serializable]
struct Objective
{ 
    public string description;
    private int currentStep;
    public int stepsToComplete;
    public bool isCompleted;
    
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

    public void CompleteQuest()
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
