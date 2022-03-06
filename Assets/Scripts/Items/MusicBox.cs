using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MusicBox : MonoBehaviour, InteractionInterface
{
    [Header("Settings")]
    [SerializeField] private ItemBase neededItem;
    [SerializeField] private AudioClip song;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private string hintText;

    [Header("GameObjects")] 
    [SerializeField] private GameObject missingPart;

    private bool isBroken = true;
    private Animator anim;

    public static UnityAction<string> SendHintText;
    
    void Start()
    {
        missingPart.SetActive(false);
        audioSource.clip = song;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (audioSource.isPlaying) return;

        if(anim)
            anim.SetBool("IsActive", false);
            
    }

    public void HandleInteraction()
    {
        
    }

    public void HandleInteraction(PlayerController player)
    {
        if (!isBroken)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
                if(anim)
                    anim.SetBool("IsActive", true);
            }
        }
        else if (player.GetEquippedItem().itemInfo.item.name == neededItem.itemInfo.item.name)
        {
            Debug.Log("TengoItem");
            missingPart.SetActive(true);
            isBroken = false;
        }
        else
        {
            Debug.Log("No tengo item");
            SendHintText?.Invoke(hintText);
        }
    }
}
