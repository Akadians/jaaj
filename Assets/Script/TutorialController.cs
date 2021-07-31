using TMPro;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public GameObject PanelText;
    public TextMeshProUGUI MainTex;
    public TextMeshProUGUI[] TutorialTex;
    public int IdTex;    

    [SerializeField]
    private GameObject col01;
    [SerializeField]
    private GameObject col02;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        TutoControl();
    }

    void TutoControl ()
    {
        MainTex.text = TutorialTex[IdTex].text;

        if (IdTex < 3)
        {
            IntroTuto();
            return;
        }
        if(IdTex >= 3 && IdTex < 7)
        {
            JumpTuto();
        }
    }
    void IntroTuto()
    {
        if (PanelText.activeInHierarchy != false)
        {          

            if (Input.GetKeyDown(KeyCode.Return))
            {
                MainTex.text = TutorialTex[IdTex].text;
                if (IdTex == 2)
                {
                    PanelText.SetActive(false);
                    IdTex++;
                    return;
                }                
                IdTex++;
                return;
            }
            return;            
        }
    }

    void JumpTuto()
    {
        if (PanelText.activeInHierarchy != false)
        {

            if (Input.GetKeyDown(KeyCode.Return))
            {
                MainTex.text = TutorialTex[IdTex].text;
                if (IdTex == 6)
                {
                    PanelText.SetActive(false);
                    IdTex++;
                    return;
                }
                IdTex++;
                return;
            }
            return;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.layer == 14 && IdTex < 4)
        {          
            PanelText.SetActive(true);            
            Destroy(col01);
            return;
        }

        if (collision.gameObject.layer == 14 && IdTex > 4)
        {
            PanelText.SetActive(true);
            Destroy(col02);
            return;
        }
    }
}

