using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class QuestUIManager : MonoBehaviour
{
    [SerializeField] private Color standardColor;
    [SerializeField] private Color strongerColor;

    [SerializeField] private Text questText;
    [SerializeField] private QuestManager questManager;

    [SerializeField] private GameObject objectiveHud = null;

    private bool isHighlightingQuest = false;
    private bool isAlphaZero = true;

    private void Start()
    {
        QuestManager.SetQuestUIText += SetQuest;
    }

    void SetQuest(string questName)
    {
        Debug.Log("Hola " + questName);
        questText.text = questName;
        if (questName != "")
            HighlightCurrentQuest();
        else
            HideQuest();
    }

    void HideQuest()
    {
        StartCoroutine(HideUI());
    }

    IEnumerator HideUI()
    {
        if (isAlphaZero) yield break;

        CanvasGroup alphaModifier;
        alphaModifier = objectiveHud.GetComponent<CanvasGroup>();
        if (!alphaModifier) yield break;

        while (alphaModifier.alpha > 0)
        {
            alphaModifier.alpha -= Time.deltaTime*3;
            yield return null;
        }

        isAlphaZero = true;
    }

    public void HighlightCurrentQuest()
    {
        if (questManager.GetActiveQuest().Count > 0)
            StartCoroutine(HighlightQuest());
    }

    private IEnumerator HighlightQuest()
    {
        objectiveHud.SetActive(true);
        CanvasGroup canvasGroup;
        if (!isHighlightingQuest && !isAlphaZero)
        {
            isHighlightingQuest = true;
            questText.color = strongerColor;
            yield return new WaitForSeconds(1.0f);
            float timer = 0;
            while (questText.color != standardColor)
            {
                questText.color = Color.Lerp(strongerColor, standardColor, timer);
                timer += Time.deltaTime;
                yield return null;
            }

            isHighlightingQuest = false;
        }
        else if (!isHighlightingQuest && isAlphaZero)
        {
            canvasGroup = objectiveHud.GetComponent<CanvasGroup>();
            if (canvasGroup)
            {
                isHighlightingQuest = true;
                questText.color = strongerColor;
                yield return new WaitForSeconds(1.0f);
                float timer = 0;
                while (questText.color != standardColor)
                {
                    questText.color = Color.Lerp(strongerColor, standardColor, timer);
                    canvasGroup.alpha += Time.deltaTime * 3f;
                    timer += Time.deltaTime;
                    yield return null;
                }

                isHighlightingQuest = false;
                isAlphaZero = false;
            }
        }
    }

    private void OnDestroy()
    {
        QuestManager.SetQuestUIText -= SetQuest;
    }
}