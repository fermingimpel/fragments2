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

    private void Start()
    {
        QuestManager.SetQuestUIText += SetQuest;
    }

    void SetQuest(string questName)
    {
        questText.text = questName;
        StartCoroutine(ChangeQuest());
    }

    private IEnumerator ChangeQuest()
    {
        questText.color = strongerColor;
        yield return new WaitForSeconds(1.0f);
        float timer = 0;
        while (questText.color != standardColor)
        {
            questText.color = Color.Lerp(strongerColor, standardColor, timer * 1f);
            timer += Time.deltaTime;
            yield return null;
        }
    }

    private void OnDestroy()
    {
        QuestManager.SetQuestUIText -= SetQuest;
    }
}
