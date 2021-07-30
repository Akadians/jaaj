using System.Collections.Generic;
using System.Collections;
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
    public Image godSendBar;
    public Sprite[] powerSprites;
    [SerializeField]private GameObject gameoverPanel;
    public float timeToDisablePanel = 3f;
    [SerializeField]private GameObject attentionPanel;
    void Start()
    {
        HavePower(false);
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

    public void UpdateGodSendBar(int current, int max)
    {
        godSendBar.fillAmount = current / max;
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
        if(newSkill != SkillType.NONE)
        {
            HavePower(true);
            int idSkill = (int)newSkill -1;
            powerIcon.sprite = powerSprites[idSkill];
        }
    }

    public void HavePower(bool have)
    {
        //PowerIconObject.SetActive(have);
    }

    public void OpenAttentionPanel()
    {
        StopCoroutine(CloseAttectionPanel());
        StartCoroutine(CloseAttectionPanel());
    }

    IEnumerator CloseAttectionPanel()
    {
        attentionPanel.SetActive(true);
        yield return new WaitForSeconds(timeToDisablePanel);
        attentionPanel.SetActive(false);
    }

    public void OpenGameoverPanel()
    {

    }
}
