using TMPro;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public GameObject PanelText;
    public TextMeshProUGUI MainTex;
    public TextMeshProUGUI[] TutorialTex;
    public int IdTex;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        IntroTuto();
    }

    void IntroTuto()
    {
        if (PanelText.activeInHierarchy != false)
        {
            MainTex.text = TutorialTex[IdTex].text;

            if (Input.GetKeyDown(KeyCode.Return))
            {
                MainTex.text = TutorialTex[IdTex].text;
                if (IdTex == 2)
                {
                    PanelText.SetActive(false);
                    return;
                }                
                IdTex++;
                return;
            }
            return;
            
        }
    }
}

