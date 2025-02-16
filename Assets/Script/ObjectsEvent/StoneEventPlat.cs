﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneEventPlat : MonoBehaviour
{
    private Animator treeAnimator;
    public string triggerName;
    public SoundController StoneSound;

    private void Start()
    {
        treeAnimator = GetComponent<Animator>();
    }

    public void Interaction()
    {
        treeAnimator.SetTrigger(triggerName);
        StoneSound.StoneEvent();
    }
}
