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
        Time.timeScale = 1;
        StartCoroutine(NextSceneFade());
    }

    public void LoadSaveScene()
    {
        Time.timeScale = 1;
        StartCoroutine(LoadSaveSceneFade());
    }

    public void ChangeScene(string name)
    {
        Time.timeScale = 1;
        StartCoroutine(ChangeSceneFade(name));
    }

    public void ReloadScene()
    {
        Time.timeScale = 1;
        StartCoroutine(ReloadSceneFade());
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

    IEnumerator ReloadSceneFade()
    {
        isFadeCompleted = false;
        anim.SetTrigger("fade");
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => isFadeCompleted);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        anim.SetTrigger("fade");
    }

    IEnumerator ChangeSceneFade(string name)
    {
        isFadeCompleted = false;
        anim.SetTrigger("fade");
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => isFadeCompleted);
        SceneManager.LoadScene(name);
        anim.SetTrigger("fade");
        if(name != "title")
        {
            SaveGame.Instance.Save();
        }
    }

    public void OnFadeCompleted()
    {
        isFadeCompleted = true;
    }
}
