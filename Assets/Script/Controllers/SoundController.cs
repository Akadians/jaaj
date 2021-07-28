using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class SoundController : MonoBehaviour
{
    [FMODUnity.EventRef]
    FMOD.Studio.EventInstance playerState;
    //public string sfx_player_jump = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerJump()
    {
        //FMODUnity.RuntimeManager.PlayOneShot(sfx_player_jump, transform.position);
    }
    public void PlayerDeath()
    {

    }
}
