using TMPro;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public GameObject PanelText;
    public GameObject Player;
    public TextMeshProUGUI MainTex;
    public TextMeshProUGUI[] TutorialTex;
    public int IdTex;

    [SerializeField]
    private GameObject col01;
    [SerializeField]
    private GameObject col02;
    [SerializeField]
    private GameObject col03;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        TutoControl();
    }

    void TutoControl()
    {
        MainTex.text = TutorialTex[IdTex].text;

        if (IdTex < 3)
        {
            IntroTuto();
            return;
        }
        if (IdTex >= 3 && IdTex < 6)
        {
            JumpTuto();
            return;
        }
        if (IdTex >= 6 && IdTex < 8)
        {
            CombatTuto();
            return;
        }
        if (IdTex >= 8 && IdTex < 11)
        {
            ActiveTuto();
            return;
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
                if (IdTex == 5)
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

    void CombatTuto()
    {
        if (PanelText.activeInHierarchy != false)
        {

            if (Input.GetKeyDown(KeyCode.Return))
            {
                MainTex.text = TutorialTex[IdTex].text;
                if (IdTex == 7)
                {
                    PanelText.SetActive(false);
                    IdTex++;
                    Destroy(col02);
                    return;
                }
                IdTex++;
                return;
            }
            return;
        }
    }

    void ActiveTuto()
    {
        if (PanelText.activeInHierarchy != false)
        {

            if (Input.GetKeyDown(KeyCode.Return))
            {
                MainTex.text = TutorialTex[IdTex].text;
                if (IdTex == 10)
                {
                    PanelText.SetActive(false);
                    Destroy(col03);
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

        if (collision.gameObject.layer == 14 && IdTex > 4 && IdTex < 8)
        {
            PanelText.SetActive(true);            
            return;
        }

        if (collision.gameObject.layer == 14 && IdTex > 7 && IdTex < 11)
        {
            PanelText.SetActive(true);            
            return;
        }
    }
}

