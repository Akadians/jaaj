using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    public static SaveGame Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Save()
    {
        PlayerPrefs.SetInt("saveScene", SceneManager.GetActiveScene().buildIndex);
        print("Salvou: " + PlayerPrefs.GetInt("saveScene"));
    }

    public bool CheckHasSave()
    {
        print("Checou: " + PlayerPrefs.GetInt("saveScene"));
        return PlayerPrefs.GetInt("saveScene") != 0;
    }

    public int GetSaveSceneID()
    {
        return PlayerPrefs.GetInt("saveScene");
    }
}
