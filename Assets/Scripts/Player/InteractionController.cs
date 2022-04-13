using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InteractionController : MonoBehaviour
{
    [Header("Settings")] 
    [SerializeField] private KeyCode interactionKey;
    [SerializeField] private float interactionRange;
    [SerializeField] private LayerMask interactionMask;
    
    [Header("Misc")]
    [SerializeField] private GameObject camera;
    [SerializeField] private Image originalCrosshair;
    [SerializeField] private Image interactionCrosshair;

    [SerializeField] GameObject interactButton;

    private PlayerController player;
    public static UnityAction<ItemBase> InteractedObject;
    
    void Start()
    {
        player = GetComponent<PlayerController>();
    }
    
    public void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, interactionRange, interactionMask))
        {
            interactButton.SetActive(true);
            if (Input.GetKeyDown(interactionKey))
            {
                if (hit.collider.gameObject)
                {
                    if (hit.collider.gameObject.GetComponent(typeof(InteractionInterface)) is InteractionInterface interactionObject)
                    {
                        interactionObject.HandleInteraction();
                        if(player)
                            interactionObject.HandleInteraction(player);
                        ItemBase item = hit.collider.GetComponent<ItemBase>();
                        if(item)
                            InteractedObject?.Invoke(item);

                        Note n = hit.collider.GetComponent<Note>();
                        if (n) {
                            if (n.openFirstTime) {
                                FindObjectOfType<Inventory>().HideShowInventory();
                                n.UseItem();
                            }
                        }
                    }
                }
            }
        }
        else
            interactButton.SetActive(false);
    }
}