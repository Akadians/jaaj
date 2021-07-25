using System.Collections;
using System.Collections.Generic;
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

    }

    public bool CheckHasSave()
    {
        return PlayerPrefs.GetInt("saveScene") != 0;
    }

    public void Load()
    {

    }
}
