using System;
using UnityEditor;
using UnityEngine;

[Serializable]
public class ObjectiveBase : MonoBehaviour
{
    public string description = "";
    [CustomUtils.ReadOnly] protected int CurrentStep = 0;
    public int stepsToComplete = 1;
    public bool isCompleted = false;

    public virtual void CheckObjectiveCompletion(QuestCheckList listToCompare)
    {
    }

    public void ResetObjective()
    {
        CurrentStep = 0;
        isCompleted = false;
    }
}