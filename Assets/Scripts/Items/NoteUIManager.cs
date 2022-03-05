using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NoteUIManager : MonoBehaviour
{
    [SerializeField] private GameObject noteCanvas;
    [SerializeField] private Image page;
    [SerializeField] private TMPro.TextMeshProUGUI pageNumber;
    [SerializeField] private List<Sprite> pages = new List<Sprite>();

    private int currentPage = 0;

    private Sprite currentPageImage = null;

    public static UnityAction HideInventory;

    private void Start()
    {
        noteCanvas.SetActive(false);
        SetPageText();
    }

    private void Update()
    {
        if (!noteCanvas.activeSelf) return;
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            OnClickPrevious();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            OnClickNext();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnClickClose();
        }
    }

    private void OnEnable()
    {
        noteCanvas.SetActive(false);
        SetPageText();
    }

    public void Activate()
    {
        noteCanvas.SetActive(true);
        currentPageImage = pages[currentPage];
        page.sprite = currentPageImage;
        page.SetNativeSize();
        HideInventory?.Invoke();
    }

    public void OnClickNext()
    {
        if (currentPage < pages.Count-1)
            currentPage++;
        else
            currentPage = 0;

        SetPageText();
        currentPageImage = pages[currentPage];
        page.sprite = currentPageImage;
        page.SetNativeSize();
    }
    public void OnClickPrevious()
    {
        if (currentPage > 0)
            currentPage--;
        else
            currentPage = pages.Count-1;
        
        SetPageText();
        currentPageImage = pages[currentPage];
        page.sprite = currentPageImage;
        page.SetNativeSize();
    }
    public void OnClickClose()
    {
        noteCanvas.SetActive(false);
        HideInventory?.Invoke();
    }

    private void SetPageText()
    {
        int newPage = currentPage+1;
        
        pageNumber.text = newPage.ToString() + " / " + pages.Count;
    }
    
}