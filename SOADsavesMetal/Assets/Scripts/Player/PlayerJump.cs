using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public Player ps;
    private bool tryJump;
    public GameObject parryBubble;
    public float bubbleMaxSize;
    void Start()
    {
        tryJump = false;
    }

    public void HandleJump()
    {
        if ((ps.currentBandMember == "John" || !ps.isSuperActive) &&
            (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(ps.GetUp()))
                    && ps.remainingJumps > 0)
        {
            tryJump = true;
        }
    }

    private void Jump()
    {
        if (ps.crouched)
        {
            ps.crouched = false;
            ps.playerUpperBody.transform.localPosition = new Vector3(0, 0);
            ps.upperBodyHitbox.SetActive(true);
        }
        ps.PlayAnims("Jump");
        ps.remainingJumps -= 1;

        ps.PlayAudioEvent(ps.playerJump);

        if (ps.inAir)
        {
            if (ps.currentBandMember == "John")
            {// && playerLowerAnim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "JohnJumpLegs")
                ps.PlayAnims("Jump");
                ps.rb.velocity = new Vector2(ps.rb.velocity.x, ps.jumpHeight);
                ps.PlayAudioEvent(ps.johnJump); // problem: basic jump plays at the same time. can't auso.Stop() because if the super is active, it cancels that too....
            }
            else if (ps.currentBandMember == "Shavo")
            {// && playerLowerAnim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "ShavoJumpLegs")
             // PlayAnims("Dash");
                StartCoroutine(Dash());
                ps.PlayAudioEvent(ps.shavoDash);
            }
            else if (ps.currentBandMember == "Daron" && !ps.daronListeningForParry)
            {
                StartCoroutine("Parry");
                ps.PlayAudioEvent(ps.daronTeleport);
            }
            else if (ps.currentBandMember == "Serj" && !ps.serjFlightActive)
            {
                ps.PlayAudioEvent(ps.serjFlyStart);
                StartCoroutine(Hover());
            }
        }
        else
        {
            ps.inAir = true;
            ps.rb.velocity = new Vector2(ps.rb.velocity.x, ps.jumpHeight);
        }
        //playerUpperAnim.Play("JohnJump2");
    }

    void FixedUpdate()
    {
        if(tryJump)
        {
            Jump();
            tryJump = false;
        }
    }

    public IEnumerator Dash()
    {
        float dashPower = 16f;
        ps.blockHorizontalMovement = true;
        yield return null;
        if (!ps.attacking) ps.playerUpperAnim.Play(ps.GetAnimName("Dash"));
        ps.playerLowerAnim.Play(ps.GetAnimName("Dash"));
        ps.rb.velocity = new Vector2(ps.rb.velocity.x * 0.3f, 5f);
        yield return new WaitForSeconds(0.12f);
        var playerRotation = gameObject.transform.rotation;
        ps.rb.velocity = new Vector2((playerRotation.y == 0 ? 1 : -1) * 1.5f * dashPower, 1.1f * dashPower);
        yield return new WaitForSeconds(0.17f);
        ps.rb.velocity *= 0.33f;
        ps.blockHorizontalMovement = false;

    }

    public IEnumerator Parry()
    {
        ps.blockNormalJumpAnims = true;
        StartCoroutine(BubbleExpand());
        ps.curInvulnerableTime = 0.33f > ps.curInvulnerableTime ? 0.33f : ps.curInvulnerableTime;
        ps.playerUpperAnim.Play(ps.GetAnimName("Parry"));

        float timer = 0f;

        ps.daronListeningForParry = true;
        while (timer < 0.26f)
        {
            timer += Time.deltaTime;
            if (!ps.daronListeningForParry)
            {
                ps.rb.velocity = new Vector2(ps.rb.velocity.x, ps.jumpHeight * 0.75f);
                ps.curInvulnerableTime += 0.2f;
                break;
            }
            yield return null;
        }
        yield return null;
        StartCoroutine(BubbleContract());
        yield return new WaitForSeconds(0.2f);

        ps.daronListeningForParry = false;
        ps.blockNormalJumpAnims = false;
    }

    public IEnumerator Hover()
    {
        // ps.playerLowerAnim.Play("SerjLegs");
        ps.serjFlightActive = true;
        float oldGravity = ps.rb.gravityScale;
        ps.rb.gravityScale = 0f;
        ps.rb.velocity = new Vector2(0, 0);
        ps.rb.drag = 10f;
        float timer = 0f;
        while (timer < 2f)
        {
            if (!ps.inAir || ps.isSuperActive)
            {
                ps.serjFlightActive = false;
                break;
            }
            timer += Time.deltaTime;
            yield return null;
        }
        ps.rb.gravityScale = oldGravity;
        ps.rb.drag = 0f;
        yield return null;
        ps.serjFlightActive = false;
        ps.PlayAudioEvent(ps.serjFlyEnd);
    }

    private IEnumerator BubbleExpand()
    {
        parryBubble.SetActive(true);
        yield return null;

        float timer = 0f, duration = 0.07f;
        while (timer < duration)
        {
            if (!ps.daronListeningForParry) break;

            float sz = Mathf.Lerp(0.01f, bubbleMaxSize, timer / duration);
            parryBubble.transform.localScale = new Vector2(sz, sz);
            timer += Time.deltaTime;
            yield return null;
        }
        parryBubble.transform.localScale = new Vector2(bubbleMaxSize, bubbleMaxSize);
        yield return null;
    }

    private IEnumerator BubbleContract()
    {
        yield return null;
        float timer = 0f, duration = 0.09f;
        while (timer < duration)
        {
            float sz = Mathf.Lerp(bubbleMaxSize, 0.01f, timer / duration);
            parryBubble.transform.localScale = new Vector2(sz, sz);
            timer += Time.deltaTime;
            yield return null;
        }

        parryBubble.SetActive(false);
        yield return null;
    }
}
