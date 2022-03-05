using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Note : ItemBase
{
    public UnityEvent showNote;
    public override void HandleInteraction()
    {
        base.HandleInteraction();
    }

    public override void Use()
    {
        base.Use();
        showNote?.Invoke();
    }
}