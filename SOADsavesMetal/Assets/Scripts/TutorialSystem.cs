using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialSystem : MonoBehaviour {

    public GameObject enterText;
    public Text tutorialText;
    public Player player;
    public tutorialMarker marker;
    public tutorialDummy dummy;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine("TutorialStart");
	}

    public IEnumerator TutorialStart()
    {
        marker.gameObject.SetActive(false);
        tutorialText.text = "Welcome, player! Let's get you familiarized with the controls before you begin your rad journey to defeat the four Goddesses of the Underworld.";
        enterText.SetActive(true);
        yield return new WaitUntil(() => Input.GetButtonDown("Submit"));
        enterText.SetActive(false);
        yield return new WaitForSeconds(.1f);

        tutorialText.text = "Movement: Move your character using the left and right arrow keys.";
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow));
        enterText.SetActive(true);
        yield return new WaitUntil(() => Input.GetButtonDown("Submit"));
        enterText.SetActive(false);
        yield return new WaitForSeconds(.1f);

        tutorialText.text = "Movement: Crouch by using the down key!";
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.DownArrow) && !player.inAir);
        enterText.SetActive(true);
        yield return new WaitUntil(() => Input.GetButtonDown("Submit"));
        enterText.SetActive(false);
        yield return new WaitForSeconds(.1f);

        tutorialText.text = "Movement: Press Spacebar to jump!"; // change first part, make less meta
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        enterText.SetActive(true);
        yield return new WaitUntil(() => Input.GetButtonDown("Submit"));
        enterText.SetActive(false);
        yield return new WaitForSeconds(.1f);

        tutorialText.text = "Unique Aerial Movement: Each character has a form of aerial movement that is unique to them."; // consider removing second sentence and move "System of a Down's drummer, John" down to the next one
        enterText.SetActive(true);
        yield return new WaitUntil(() => Input.GetButtonDown("Submit"));
        enterText.SetActive(false);
        yield return new WaitForSeconds(.1f);

        tutorialText.text = "You are currently playing as System of a Down's drummer, John. John has a double jump. Press Spacebar twice to perform a double jump. Try reaching the marker in the air.";
        marker.gameObject.SetActive(true);
        yield return new WaitUntil(() => marker.touched);
        marker.gameObject.SetActive(false);
        tutorialText.text = "Great job!";
        enterText.SetActive(true);
        yield return new WaitUntil(() => Input.GetButtonDown("Submit"));
        enterText.SetActive(false);
        yield return new WaitForSeconds(.1f);

        tutorialText.text = "Attacks: There are three types of attacks you can perform: Short range, long range, and a super attack.";
        enterText.SetActive(true);
        yield return new WaitUntil(() => Input.GetButtonDown("Submit"));
        enterText.SetActive(false);
        yield return new WaitForSeconds(.1f);

        tutorialText.text = "Short Range: Short range attacks are powerful, but span a limited distance. Try using John's short range attack by pressing the \"Z\" key on the dummy.";
        dummy.shortCheck = true;
        yield return new WaitUntil(() => dummy.shortHit);
        tutorialText.text = "Awesome!";
        enterText.SetActive(true);
        yield return new WaitUntil(() => Input.GetButtonDown("Submit"));
        enterText.SetActive(false);
        yield return new WaitForSeconds(.1f);
        dummy.shortCheck = false;

        tutorialText.text = "Long Range: Long range attacks span a longer distance, but are less powerful. Try using John's long range attack by pressing the \"X\" key on the dummy.";
        dummy.longCheck = true;
        yield return new WaitUntil(() => dummy.longHit);
        tutorialText.text = "Nice shot!";
        enterText.SetActive(true);
        yield return new WaitUntil(() => Input.GetButtonDown("Submit"));
        enterText.SetActive(false);
        yield return new WaitForSeconds(.1f);
        dummy.longCheck = false;

        tutorialText.text = "The Super: Every band member has their own special attack called a Super. Supers are very powerful and unique to each character."; // consider "unique to each character" instead of "vary..."
        enterText.SetActive(true);
        yield return new WaitUntil(() => Input.GetButtonDown("Submit"));
        enterText.SetActive(false);
        yield return new WaitForSeconds(.1f);


        tutorialText.text = "Super attacks can only be used once your Super Meter is completely full. The more damage you inflict, the faster your meter fills up.";
        enterText.SetActive(true);
        yield return new WaitUntil(() => Input.GetButtonDown("Submit"));
        enterText.SetActive(false);
        yield return new WaitForSeconds(.1f);

        tutorialText.text = "Fill up John's Super Meter by attacking the dummy. Once the meter is full, unleash John's Super attack by pressing the \"C\" key.";
        dummy.shortCheck = true;
        dummy.longCheck = true;
        dummy.superCheck = true;
        yield return new WaitUntil(() => dummy.superHit);
        tutorialText.text = "Metal!";
        enterText.SetActive(true);
        yield return new WaitUntil(() => Input.GetButtonDown("Submit"));
        enterText.SetActive(false);
        yield return new WaitForSeconds(.1f);

        tutorialText.text = "Congratulations! You have completed the tutorial and are ready to face the Goddesses of the Underworld!";
        enterText.SetActive(true);
        yield return new WaitUntil(() => Input.GetButtonDown("Submit"));
        enterText.SetActive(false);
        yield return new WaitForSeconds(.1f);

        tutorialText.text = "Good luck and remember, the very essence of heavy metal is relying on you!";
        enterText.SetActive(true);
        yield return new WaitUntil(() => Input.GetButtonDown("Submit"));
        enterText.SetActive(false);
        yield return new WaitForSeconds(.1f);

        tutorialText.text = "End.";
    }
}
