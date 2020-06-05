using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CreditSkip : MonoBehaviour
{
    public string levelName;
    private AsyncOperation synclevel;

    public GameObject hint;

    void Start()
    {
        StartCoroutine("loadLevel");
    }

    IEnumerator loadLevel()
    {
        synclevel = SceneManager.LoadSceneAsync(levelName);
        synclevel.allowSceneActivation = false;
        yield return synclevel;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Skipping credits");
            synclevel.allowSceneActivation = true;
        }

        if (Input.anyKeyDown)
        {
            if (!hint.active)
            {
                hint.SetActive(true);
                Invoke("FadeHint", 3f);
            }

        }
    }


    private void FadeHint()
    {
        hint.SetActive(false);
    }


}
