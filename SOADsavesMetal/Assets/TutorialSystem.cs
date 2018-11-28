using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialSystem : MonoBehaviour {

    public GameObject enterText;
    public Text tutorialText;

	// Use this for initialization
	void Start () {
        StartCoroutine("TutorialStart");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator TutorialStart()
    {
        tutorialText.text = "Welcome player! Before you begin your rad journey to defeat the four goddesses of the underworld, let's get you familiarized with the game's controls.";
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        yield return new WaitForSeconds(0.1f);
        enterText.SetActive(false);
        tutorialText.text = "Movement: Move your character using the left and right arrow keys.";
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow));
        tutorialText.text = "";
        yield return new WaitForSeconds(2.0f);
        tutorialText.text = "Movement: Crouching can sometimes be used to dodge attacks.Crouch by using the down key!";
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.DownArrow));
        tutorialText.text = "";
        yield return new WaitForSeconds(2.0f);
        tutorialText.text = "Movement: Jumping plays a vital role in this game.Press Spacebar to jump!";
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        tutorialText.text = "";
        yield return new WaitForSeconds(1.5f);

    }
}
