using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelTrigger : MonoBehaviour
{
    private UIControler _UIController;
    public string nextSceneName;
    public int playersCount;
    private bool isSecondInteraction;

    private void Start() {
        _UIController = FindObjectOfType(typeof(UIControler)) as UIControler;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            playersCount++;
            if(playersCount == 2)
            {
                _UIController.ChangeScene(nextSceneName);
            }
            else if(!isSecondInteraction)
            {
                isSecondInteraction = true;
                _UIController.OpenAttentionPanel("Vocês precisam passar juntas!");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            playersCount--;
        }
    }
}
