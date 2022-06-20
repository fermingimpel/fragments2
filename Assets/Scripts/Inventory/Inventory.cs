using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour, IPointerClickHandler {
    [Header("UI")]
    [SerializeField] private GameObject canvas;
    [Header("Inventory")]
    [SerializeField] private GameObject inventorySlot;
    [SerializeField] private GridLayoutGroup grid;
    [SerializeField] private int xSize;
    [SerializeField] private int ySize;
    [SerializeField] private float dropOffset;
    [Header("Items")]
    [SerializeField] private List<ItemBase> items = new List<ItemBase>();
    [SerializeField] private UnityEngine.UI.Image description;

    private List<InventorySlot> slots = new List<InventorySlot>();

    private int maxInventorySize = 0;

    private bool isInventoryShown = false;
    private bool isShowingItem = false;
    public static UnityAction<bool> Pause;
    private void Awake() {
        InventorySlot.ShowDescription += ShowDescription;
        InventorySlot.HideDescription += HideDescription;
        InventorySlot.DropItem += DropItem;
        ItemBase.PickUp += AddItem;
        NoteUIManager.HideInventory += HideInventory;
    }

    private void Start() {
        grid.constraintCount = xSize;

        for (int y = 0; y < ySize; y++) {
            for (int x = 0; x < xSize; x++) {
                GameObject slot = Instantiate(inventorySlot, grid.gameObject.transform);
                InventorySlot slotScript = slot.GetComponent<InventorySlot>();
                if (slotScript)
                    slots.Add(slotScript);
            }
        }
        canvas.SetActive(isInventoryShown);
        maxInventorySize = xSize * ySize;

        gameObject.SetActive(false);
    }

    void Update() {
        // if (!canvas.activeSelf)
        //     return;

        if (PauseController.instance.IsPaused && !isInventoryShown)
            return;

        if (isShowingItem)
            return;

        if (Input.GetKeyDown(KeyCode.Tab) && !isInventoryShown)
            HideShowInventory();
        else if (Input.GetKeyDown(KeyCode.Q) && isInventoryShown)
            HideShowInventory();
    }


    private void AddItem(ItemBase item) {
        if (items.Count >= maxInventorySize)
            return;

        items.Add(item);
        for (int i = 0; i < slots.Count; i++) {
            if (!slots[i].GetItem()) {
                slots[i].SetItem(items[i].gameObject);
                item.gameObject.SetActive(false);
                break;
            }
        }
        Debug.Log("Handle interaction");

    }

    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Left) {
            foreach (InventorySlot slot in slots) {
                if (slot.gameObject != eventData.pointerCurrentRaycast.gameObject) {
                    slot.HideContextualMenu();
                }
                else if (slot.GetItem()) {
                    slot.ShowContextualMenu();
                }

            }
        }
        else {
            foreach (InventorySlot slot in slots) {
                slot.HideContextualMenu();
            }
        }
    }

    private void DropItem(ItemBase item) {
        if (items.Contains(item)) {
            item.gameObject.transform.position = gameObject.transform.position + (transform.forward * dropOffset);
            item.gameObject.SetActive(true);
            items.Remove(item);
        }
    }

    private void ShowDescription(Sprite newDescription) {
        description.sprite = newDescription;
        description.rectTransform.sizeDelta = newDescription.rect.size;
        description.color = Color.white;
    }

    private void HideDescription() {
        description.color = new Color(1, 1, 1, 0);
    }

    public void HideShowInventory() {
        isInventoryShown = !isInventoryShown;
        if (isInventoryShown) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Pause?.Invoke(true);
        }
        else {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Pause?.Invoke(false);
        }
        canvas.SetActive(isInventoryShown);
    }

    private void HideInventory() {
        isInventoryShown = !isInventoryShown;
        isShowingItem = !isShowingItem;
        canvas.SetActive(isInventoryShown);
    }

    public void OnClickBack() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Pause?.Invoke(false);
        isInventoryShown = false;
        canvas.SetActive(isInventoryShown);
    }

    private void OnDestroy() {
        InventorySlot.ShowDescription -= ShowDescription;
        InventorySlot.HideDescription -= HideDescription;
        InventorySlot.DropItem -= DropItem;
        ItemBase.PickUp -= AddItem;
        NoteUIManager.HideInventory -= HideInventory;
    }

}

