using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    public Button btnContinue;

    private void Start()
    {
        //checar se existe um save e desabilitar ou habilitar a interacao    
    }

    public void Play()
    {
        
    }

    public void Exit()
    {
        Application.Quit();
    }
}
