using System;
using UnityEngine;
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
    
    public void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, interactionRange, interactionMask))
        {
            if (Input.GetKeyDown(interactionKey))
            {
                if (hit.collider.gameObject)
                {
                    if (hit.collider.gameObject.GetComponent(typeof(InteractionInterface)) is InteractionInterface interactionObject)
                    {
                        interactionObject.HandleInteraction();
                    }
                }
            }
        }
    }
}