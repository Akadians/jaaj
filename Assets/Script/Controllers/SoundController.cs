using UnityEngine;

public class SoundController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    // TitleButton Sounds Method //
    public void ConfigButtonSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/sfx/ui/sfx_ui_menu_config");
    }
    public void CredButtonSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/sfx/ui/sfx_ui_menu_creditos");
    }
    public void ExitButtonSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/sfx/ui/sfx_ui_menu_exit");
    }
    public void ContinueButtonSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/sfx/ui/sfx_ui_menu_continue");
    }
    public void PlayButtonSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/sfx/ui/sfx_ui_menu_play");
    }
    public void BackButton()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/sfx/ui/sfx_ui_menu_volume_menos");
    }

    // Players Sounds Method //
    public void PlayerJump()
    {
        //FMODUnity.RuntimeManager.PlayOneShot("");
    }
    public void PlayerDeath()
    {
        //FMODUnity.RuntimeManager.PlayOneShot("");
    }
    public void SkillPlayer()
    {
        //FMODUnity.RuntimeManager.PlayOneShot("");
    }

}
