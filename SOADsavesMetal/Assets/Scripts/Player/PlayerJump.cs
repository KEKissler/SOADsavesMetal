using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public Player ps;

    public void HandleJump()
    {
        if (!ps.crouched && (ps.currentBandMember == "John" || !ps.isSuperActive) &&
            (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
                    && ps.remainingJumps > 0)
        {
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
                    ps.auso.Stop();
                    ps.PlayAudioEvent(ps.shavoDash);
                }
                else if (ps.currentBandMember == "Daron")
                {
                    StartCoroutine("Teleport");
                    ps.auso.Stop();
                    ps.PlayAudioEvent(ps.daronTeleport);
                }
                else if (ps.currentBandMember == "Serj")
                {
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
    }

    public IEnumerator Dash()
    {
        float dashPower = 18f;
        yield return new WaitForSeconds(0.06f);
        ps.blockHorizontalMovement = true;
        if (!ps.attacking) ps.playerUpperAnim.Play(ps.GetAnimName("Dash"));
        ps.playerLowerAnim.Play(ps.GetAnimName("Dash"));
        ps.rb.velocity = new Vector2(ps.rb.velocity.x * 0.3f, 0.1f);
        yield return new WaitForSeconds(0.12f);
        var playerRotation = gameObject.transform.rotation;
        ps.rb.velocity = new Vector2((playerRotation.y == 0 ? 1 : -1) * 1.5f * dashPower, 0.43f * dashPower);
        yield return new WaitForSeconds(0.25f);
        ps.rb.velocity *= 0.33f;
        ps.blockHorizontalMovement = false;

    }

    public IEnumerator Teleport()
    {
        ps.blockHorizontalMovement = true;
        if (!ps.attacking) ps.playerUpperAnim.Play(ps.GetAnimName("Teleport"));
        ps.playerLowerAnim.Play(ps.GetAnimName("Teleport"));
        yield return new WaitForSeconds(0.04f);
        float dashPower = 15f;
        var playerRotation = gameObject.transform.rotation;
        ps.rb.velocity = new Vector2((playerRotation.y == 0 ? 1 : -1) * 1.5f * dashPower, 0f);

        ps.rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        ps.upperBodyHitbox.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.25f);

        ps.upperBodyHitbox.GetComponent<BoxCollider2D>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        ps.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        transform.position -= new Vector3(0f, 0.05f, 0f);
        ps.rb.velocity = new Vector2(ps.rb.velocity.x * -0.15f, -2.5f);
        ps.blockHorizontalMovement = false;
    }

    public IEnumerator Hover()
    {
        ps.playerUpperAnim.Play("SerjWings");
        ps.playerLowerAnim.Play("SerjIdleLegs");
        float timer = 0f;
        while (timer < 2f)
        {
            ps.rb.velocity = new Vector2(0, 0.3f);
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
