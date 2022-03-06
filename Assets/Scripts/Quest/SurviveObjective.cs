using UnityEngine;
using UnityEngine.Events;

public class SurviveObjective : ObjectiveBase
{
    public static UnityAction SendHordeCompletion;
    public override void CompleteObjective()
    {
        base.CompleteObjective();
        isCompleted = true;
        SendHordeCompletion?.Invoke();
    }
}