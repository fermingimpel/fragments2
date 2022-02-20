using System;
using UnityEngine;

[Serializable]
public class KillObjective : ObjectiveBase
{
    public override void CheckObjectiveCompletion(QuestCheckList listToCompare)
    {
        CurrentStep = listToCompare.KillCount;
        base.CheckObjectiveCompletion(listToCompare);
        if (CurrentStep >= stepsToComplete)
        {
            isCompleted = true;
        }
    }
}