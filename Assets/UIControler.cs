using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIControler : MonoBehaviour
{
    public GameObject PowerIconObject;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PowerIcon()
    {
          
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
