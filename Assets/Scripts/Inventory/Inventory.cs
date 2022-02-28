using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Inventory : MonoBehaviour, IPointerClickHandler
{
    [Header("Inventory")] 
    [SerializeField] private GameObject inventorySlot;
    [SerializeField] private GridLayoutGroup grid;
    [SerializeField] private int xSize;
    [SerializeField] private int ySize;
    [Header("Items")]
    [CustomUtils.ReadOnly, SerializeField] private List<ItemBase> items = new List<ItemBase>();
    [SerializeField] private UnityEngine.UI.Image description;
    
    private List<InventorySlot> slots = new List<InventorySlot>();
    
    private void Start()
    {
        InventorySlot.ShowDescription += ShowDescription;
        InventorySlot.HideDescription += HideDescription;
        InventorySlot.DropItem += DropItem;
        
        grid.constraintCount = xSize;

        for (int y = 0; y < ySize; y++)
        {
            for (int x = 0; x < xSize; x++)
            {
                GameObject slot = Instantiate(inventorySlot, grid.gameObject.transform);
                InventorySlot slotScript = slot.GetComponent<InventorySlot>();
                if(slotScript)
                    slots.Add(slotScript);
            }
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button==PointerEventData.InputButton.Right)
        {
            foreach(InventorySlot slot in slots)
            {
                if (slot.gameObject != eventData.pointerCurrentRaycast.gameObject)
                {
                    slot.HideContextualMenu();
                }
                else if(slot.GetItem())
                {
                    slot.ShowContextualMenu();
                }
                    
            }
        }
        else
        {
            foreach(InventorySlot slot in slots)
            {
                slot.HideContextualMenu();
            }
        }
    }
    
    void DropItem(ItemBase item)
    {
        if (items.Contains(item))
        {
            //Spawn item
            items.Remove(item);
        }
    }

    void ShowDescription(UnityEngine.UI.Image newDescription)
    {
        description = newDescription;
        description.rectTransform.sizeDelta = newDescription.rectTransform.sizeDelta;
        description.color = Color.white;
    }

    void HideDescription()
    {
        description.color = new Color(1,1,1,0);
    }

    private void OnDestroy()
    {
        InventorySlot.ShowDescription -= ShowDescription;
        InventorySlot.HideDescription -= HideDescription;
        InventorySlot.DropItem -= DropItem;

    }

}

