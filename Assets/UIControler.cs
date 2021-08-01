using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

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
    public Image[] bars;
    public Image heroHUD;
    public Sprite[] heroesImage;
    public Sprite[] powerSprites;
    [SerializeField]private GameObject gameoverPanel;
    [SerializeField]private GameObject pausePanel;
    public float timeToDisablePanel = 3f;
    [SerializeField]private GameObject attentionPanel;
    public TextMeshProUGUI txtAttention;
    void Start()
    {
        HavePower(false);
    }

    private void Update() {

        if(Input.GetKeyDown(KeyCode.Escape) && !pausePanel.activeSelf)
        {
            OpenPausePanel();
        }        
    }

    public void ChangeHUD(int id)
    {
        foreach(Animator a in hearts.heartsAnimator)
        {
           a.runtimeAnimatorController = heartsAnimators[id];
        }
        heroHUD.sprite = heroesImage[id];
        foreach(Image i in bars)
        {
            i.gameObject.SetActive(false);
        }
        bars[id].gameObject.SetActive(true);
    }

    public void UpdateHUD(int currentHp)
    {
        DisableHearts();
        for(int i = 0; i < currentHp; i++)
        {
            hearts.heartsGameObject[i].SetActive(true);
        }
    }

    public void UpdateGodSendBar(float current, float max)
    {
        bars[1].fillAmount = current / max;
    }

    public void UpdateNiuBar(float current, float max)
    {
        bars[0].fillAmount = current / max;
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
        PowerIconObject.SetActive(have);
    }

    public void OpenAttentionPanel(string text)
    {
        txtAttention.text = text;
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
        gameoverPanel.SetActive(true);
    }

    public void ReloadScene()
    {
        GameController.Instance.ReloadScene();
    }

    public void ChangeScene(string name)
    {
        GameController.Instance.ChangeScene(name);
    }

    public void OpenPausePanel()
    {
        pausePanel.SetActive(!pausePanel.activeSelf);
        if(pausePanel.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    public void Exit()
    {
        Application.Quit();
    }
}
