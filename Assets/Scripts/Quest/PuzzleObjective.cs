using UnityEngine;
using UnityEngine.Events;

public class PuzzleObjective : ObjectiveBase
{
    public static UnityAction SendPuzzleCompletion;
    public override void CompleteObjective()
    {
        base.CompleteObjective();
        isCompleted = true;
        SendPuzzleCompletion?.Invoke();
    }
}