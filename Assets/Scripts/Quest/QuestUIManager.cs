using System;
using System.Collections;
using System.Net.Configuration;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class QuestUIManager : MonoBehaviour
{
    [SerializeField] private Color standardColor;
    [SerializeField] private Color strongerColor;

    [SerializeField] private Text questText;
    [SerializeField] private QuestManager questManager;

    private bool isHighlightingQuest = false;
    
    private void Start()
    {
        QuestManager.SetQuestUIText += SetQuest;
        SimplePlayerTest.ShowObjective += HighlightCurrentQuest;
    }

    void SetQuest(string questName)
    {
        questText.text = questName;
        StartCoroutine(HighlightQuest());
    }

    public void HighlightCurrentQuest()
    {
        StartCoroutine(HighlightQuest());
    }

    private IEnumerator HighlightQuest()
    {
        if (!isHighlightingQuest)
        {
            isHighlightingQuest = true;
            questText.color = strongerColor;
            yield return new WaitForSeconds(1.0f);
            float timer = 0;
            while (questText.color != standardColor)
            {
                questText.color = Color.Lerp(strongerColor, standardColor, timer * 1f);
                timer += Time.deltaTime;
                yield return null;
            }
            isHighlightingQuest = false;
        }
    }

    private void OnDestroy()
    {
        QuestManager.SetQuestUIText -= SetQuest;
        SimplePlayerTest.ShowObjective -= HighlightCurrentQuest;

    }
}
