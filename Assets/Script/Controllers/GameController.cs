using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using FMODUnity;

public enum GameState
{
    MAIN_MENU, GAMEPLAY, PAUSE
}

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public GameState currentState;

    public void ChangeGameState(GameState newState)
    {
        currentState = newState;
    }

    public void ClosedAplication()
    {

    }
}
