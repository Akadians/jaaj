using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    [SerializeField]
    private Button btnContinue;

    public Dropdown Quality;   


    private void Start()
    {
        CheckContinue();
    }

    public void Play()
    {
        GameController.Instance.NextScene();
    }

    private void CheckContinue()
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

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
