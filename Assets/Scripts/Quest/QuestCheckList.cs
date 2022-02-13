using System;
using UnityEngine;

[Serializable]
public struct QuestCheckList
{
    public int KillCount;
    public int DistanceTraveled;
    public InteractionInterface InteractedObject;
    public GameObject ObjectToCompare;
    public GameObject ObjectCreated;
    public GameObject CollidedObject;

    public void Reset()
    {
        KillCount = 0;
        DistanceTraveled = 0;
        InteractedObject = null;
        ObjectToCompare = null;
        ObjectCreated = null;
        CollidedObject = null;
    }
    
}
