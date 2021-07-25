using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeController : MonoBehaviour
{
    private Animator anim;
    public bool isFadeCompleted;

    void Start()
    {
        anim = GetComponent<Animator>();    
    }

    public void NextScene()
    {
        StartCoroutine(NextSceneFade());
    }

    public void LoadSaveScene()
    {
        StartCoroutine(LoadSaveSceneFade());
    }

    IEnumerator NextSceneFade()
    {
        isFadeCompleted = false;
        anim.SetTrigger("fade");
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => isFadeCompleted);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        anim.SetTrigger("fade");
        yield return new WaitUntil(() => isFadeCompleted);
        SaveGame.Instance.Save();
    }

    IEnumerator LoadSaveSceneFade()
    {
        isFadeCompleted = false;
        anim.SetTrigger("fade");
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => isFadeCompleted);
        SceneManager.LoadScene(SaveGame.Instance.GetSaveSceneID());
        anim.SetTrigger("fade");
    }

    public void OnFadeCompleted()
    {
        isFadeCompleted = true;
    }
}
