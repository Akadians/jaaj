using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SkillType
{
    BULL_SKILL, CAT_SKILL, BAT_SKILL  
}

public class UIControler : MonoBehaviour
{
    public GameObject PowerIconObject;
    public Image powerIcon;
    public Sprite[] powerSprites;

    void Start()
    {
        //PowerIconObject.SetActive(false);
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
