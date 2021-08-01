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

    private Player _Player;
    // Start is called before the first frame update
    void Start()
    {
        _Player = FindObjectOfType(typeof(Player)) as Player;
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
            _Player.isTutoActive = true;
            if (Input.GetKeyDown(KeyCode.Return))
            {
                MainTex.text = TutorialTex[IdTex].text;
                if (IdTex == 2)
                {
                    _Player.isTutoActive = false;
                    _Player.players[0].rigB.gameObject.layer = 14;
                    _Player.players[1].rigB.gameObject.layer = 14;
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
            _Player.isTutoActive = true;
            if (Input.GetKeyDown(KeyCode.Return))
            {
                MainTex.text = TutorialTex[IdTex].text;
                if (IdTex == 5)
                {
                    _Player.isTutoActive = false;
                    _Player.players[0].rigB.gameObject.layer = 14;
                    _Player.players[1].rigB.gameObject.layer = 14;
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
            _Player.isTutoActive = true;
            if (Input.GetKeyDown(KeyCode.Return))
            {
                MainTex.text = TutorialTex[IdTex].text;
                if (IdTex == 7)
                {
                    _Player.isTutoActive = false;
                    _Player.players[0].rigB.gameObject.layer = 14;
                    _Player.players[1].rigB.gameObject.layer = 14;
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
            _Player.isTutoActive = true;
            if (Input.GetKeyDown(KeyCode.Return))
            {
                MainTex.text = TutorialTex[IdTex].text;
                if (IdTex == 10)
                {
                    _Player.isTutoActive = false;
                    _Player.players[0].rigB.gameObject.layer = 14;
                    _Player.players[1].rigB.gameObject.layer = 14;
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

