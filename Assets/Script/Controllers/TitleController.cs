using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    public Button btnContinue;

    private void Start()
    {
        btnContinue.interactable = SaveGame.Instance.CheckHasSave();
    }

    public void Play()
    {

    }

    public void Exit()
    {
        Application.Quit();
    }
}
