using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    [SerializeField]
    private Button btnContinue;

    private void Start()
    {
        CheckContinue();
    }

    public void Play()
    {
        GameController.Instance.NextScene();
    }

    private void CheckContinue ()
    {
        btnContinue.interactable = SaveGame.Instance.CheckHasSave();
    }
    public void Continue()
    {
        GameController.Instance.LoadSaveScene();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
