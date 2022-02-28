using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, InventoryActions
{
    [SerializeField] private GameObject item;
    [SerializeField] private GameObject contextualMenu;
    [SerializeField] private Image itemImage;
    
    
    private ItemBase itemScript;
    [HideInInspector] public bool isContextualMenuActive = false;

    public static UnityAction<Image> ShowDescription;
    public static UnityAction HideDescription;
    public static UnityAction<ItemBase> DropItem;

    private void Start()
    {
        contextualMenu.SetActive(false);
    }

    public void SetItem(GameObject newItem)
    {
        if (item)
            return;

        item = newItem;
        if (item)
            itemScript = item.GetComponent<ItemBase>();

        itemImage = itemScript.itemInfo.item.inventoryImage;
        itemImage.color = Color.white;
    }

    public void ShowContextualMenu()
    {
        contextualMenu.SetActive(true);
        isContextualMenuActive = true;
    }

    public void HideContextualMenu()
    {
        contextualMenu.SetActive(false);
        isContextualMenuActive = false;
    }

    public ItemBase GetItem()
    {
        return itemScript;
    }

    public void Examine()
    {
        ShowDescription?.Invoke(itemScript.itemInfo.item.description);
    }

    public void Use()
    {
        itemScript.Use();
        HideDescription?.Invoke();
    }

    public void Drop()
    {
        HideDescription?.Invoke();
        DropItem?.Invoke(itemScript);
        item = null;
        itemScript = null;
        itemImage.color = new Color(1,1,1,0);
    }
}