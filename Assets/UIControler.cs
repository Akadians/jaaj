using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public struct Heart
{
    public GameObject[] heartsGameObject;
    public Animator[] heartsAnimator;
}

public class UIControler : MonoBehaviour
{
    public GameObject PowerIconObject;
    public Heart hearts;
    public RuntimeAnimatorController[] heartsAnimators;
    public Image powerIcon;
    public Sprite[] powerSprites;
    public GameObject gameoverPanel;
    void Start()
    {
        //PowerIconObject.SetActive(false);
    }

    public void ChangeHUD(int id)
    {
        foreach(Animator a in hearts.heartsAnimator)
        {
           a.runtimeAnimatorController = heartsAnimators[id];
        }
    }

    public void UpdateHUD(int currentHp)
    {
        DisableHearts();
        for(int i = 0; i < currentHp; i++)
        {
            hearts.heartsGameObject[i].SetActive(true);
        }
    }

    void DisableHearts()
    {
        foreach(GameObject g in hearts.heartsGameObject)
        {
            g.SetActive(false);
        }
    }


    public void ChangePowerIcon(SkillType newSkill)
    {
        powerIcon.sprite = powerSprites[(int)newSkill];
    }

    public void HavePower(bool have)
    {
        if (have != false)
        {
            PowerIconObject.SetActive(true);
        }
        else
        {
            PowerIconObject.SetActive(false);
        }
    }
}
