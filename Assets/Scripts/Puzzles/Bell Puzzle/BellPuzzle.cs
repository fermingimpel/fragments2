using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BellPuzzle : MonoBehaviour {

    [SerializeField] List<Bell> bells;
    [SerializeField] int maxRoundsOfSounds;
    int actualRoundOfBells = 0;
    int actualBellIDNeededToContinueThePuzzleXD = 1;
    bool puzzleCompleted = false;

    [SerializeField] Transform rewardSpawnPosition;
    [SerializeField] Transform reward;
    [SerializeField] Animation pictureAnimation;

    private ObjectiveBase objective;

    void Awake() {
        Bell.ShootedBell += CheckBellID;
    }

    private void Start()
    {
        objective = GetComponent<ObjectiveBase>();
    }

    void OnDisable() {
        Bell.ShootedBell -= CheckBellID;
    }

    void OnDestroy() {
        Bell.ShootedBell -= CheckBellID;
    }

    void CheckBellID(Bell bell) {
        if (puzzleCompleted)
            return;

        if(bell.GetBellHitId(actualRoundOfBells) != actualBellIDNeededToContinueThePuzzleXD) {
            actualRoundOfBells = 0;
            actualBellIDNeededToContinueThePuzzleXD = 1;
            return;
        }

        actualBellIDNeededToContinueThePuzzleXD++;
        if(actualBellIDNeededToContinueThePuzzleXD > bells.Count) {
            actualBellIDNeededToContinueThePuzzleXD = 1;
            actualRoundOfBells++;
            if(actualRoundOfBells >= maxRoundsOfSounds) 
                PuzzleCompleted();
        }
    }

    void PuzzleCompleted() {
        puzzleCompleted = true;
        reward.position = rewardSpawnPosition.position;
        pictureAnimation.Play();
        if(objective)
            objective.CompleteObjective();
    }

}
