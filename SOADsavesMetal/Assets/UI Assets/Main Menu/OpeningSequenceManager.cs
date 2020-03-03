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

    void Start()
    {
        //Cutscene.Play(); //Video play must have 'Wait for First Frame" unchecked
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
        Cutscene.loopPointReached += OnFinish;
    }

    private void OnFinish(VideoPlayer vp)
    {
        Debug.Log("Cutscene Ended!");
        //SceneManager.LoadScene(levelName);
        synclevel.allowSceneActivation = true;
    }


}
