using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class OpeningSequenceManager : MonoBehaviour
{

    public VideoPlayer Cutscene;
    public string levelName;
    private AsyncOperation synclevel;

    public GameObject hint;

    void Start()
    {
        StartCoroutine("loadLevel");
        //Cutscene.SetDirectAudioVolume(0, 0.5f);

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
            Debug.Log("Skipping cutscene");
            OnFinish(Cutscene);
        }

        if (Input.anyKeyDown) {
            if (!hint.active) {
                hint.SetActive(true);
                Invoke("FadeHint", 3f);
            }
            
        }

        Cutscene.loopPointReached += OnFinish;
    }

    private void OnFinish(VideoPlayer vp)
    {
        //Debug.Log("End of cutscene");
        //SceneManager.LoadScene(levelName);
        synclevel.allowSceneActivation = true;
    }

    private void FadeHint() {
        hint.SetActive(false);
    }


}
