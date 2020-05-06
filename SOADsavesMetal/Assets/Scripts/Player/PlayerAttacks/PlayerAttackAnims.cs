using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackAnims : MonoBehaviour
{
    public Player ps;

    public IEnumerator shortRangeAttackAnims()
    {
        ps.blockAttackProgress = true;
        ps.attacking = true;
        yield return null;
        if (ps.crouched)
        {
            //playerSprite.sprite.pivot.Set
            // ps.playerUpperAnim.pivotPosition.Set(0.49f, 0.83f, 0.0f);
        }
        ps.playerUpperAnim.Play(ps.GetAnimName("Short"));
        if ((ps.currentBandMember == "Shavo" || ps.currentBandMember == "Serj") && !ps.inAir && !ps.Dead)
        {
            ps.playerLowerAnim.Play(ps.GetAnimName("ShortLegs"));
        }
        if (ps.currentBandMember == "John")
        {
            ps.shortRange.Play("SoundWave");
            ps.PlayAudioEvent(ps.johnShortRange);
            yield return new WaitForSeconds(0.07f);
            ps.blockAttackProgress = false;
            yield return null;
            yield return new WaitForSeconds(0.28f);
            ps.shortRange.Play("BaseSound");
        }
        else if (ps.currentBandMember == "Shavo")
        {
            ps.playerLowerAnim.pivotPosition.Set(0.0f, -0.83f, 0.0f);
            yield return new WaitForSeconds(0.03f);
            ps.PlayAudioEvent(ps.shavoShortRange);
            yield return new WaitForSeconds(0.25f);
        }
        else if (ps.currentBandMember == "Daron")
        {
            ps.PlayAudioEvent(ps.daronShortRange);
            yield return new WaitForSeconds(0.45f);
        }
        else if (ps.currentBandMember == "Serj")
        {
            yield return new WaitForSeconds(0.39f);
            ps.PlayAudioEvent(ps.serjShortRange);
        }
        ps.PlayAnims("Idle");
        ps.blockAttackProgress = false;
        ps.attacking = false;
        ps.movedWhileAttacking = false;
        yield return null;
    }
    public IEnumerator longRangeAttackAnims()
    {
        ps.blockAttackProgress = true;
        ps.attacking = true;
        //GameObject projectile = Instantiate(stick, new Vector3(gameObject.transform.position.x + 0.5f, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation) as GameObject;
        //projectile.GetComponent<DrumStick>().SendMessage("Fire");
        if (ps.currentBandMember == "Shavo")
        {
            ps.playerUpperAnim.Play("ShavoLong");
            if (!ps.moving && !ps.crouched && !ps.inAir)
            {
                ps.playerLowerAnim.Play("ShavoLongLegs");
            }
            ps.PlayAudioEvent(ps.shavoLongRange);
            yield return new WaitForSeconds(0.27f);
        }
        else if (ps.currentBandMember == "John")
        {
            ps.PlayAudioEvent(ps.johnLongRange);
            yield return new WaitForSeconds(0.15f);
        }
        else if (ps.currentBandMember == "Daron")
        {
            ps.playerUpperAnim.Play("DaronLong");
            if (!ps.moving && !ps.crouched && !ps.inAir)
            {
                ps.playerLowerAnim.Play("DaronAttackLegs");
            }
            ps.PlayAudioEvent(ps.daronLongRange);
            yield return new WaitForSeconds(0.24f);
        }
        else if (ps.currentBandMember == "Serj")
        {
            Debug.Log("Serj");
            ps.playerUpperAnim.Play("SerjLong");
            if (!ps.moving && !ps.crouched && !ps.inAir)
            {
                ps.playerLowerAnim.Play("SerjLongLegs");
            }
            ps.PlayAudioEvent(ps.serjLongRange);
            yield return new WaitForSeconds(0.48f);
        }
        yield return new WaitForSeconds(0.07f);
        ps.blockAttackProgress = false;
        ps.attacking = false;
        yield return null;
    }
    public IEnumerator superAttackAnims()
    {
        // Ensure invincibility from the beginning of the attack to the end
        if (ps.currentBandMember != "John") ps.curInvulnerableTime = 9f;

        ps.blockAttackProgress = true;
        ps.attacking = true;
        ps.isSuperActive = true;
        if (ps.currentBandMember == "Shavo")
        {
            float initialAnimSpeed;  // Change the animation timing to account for jumping, i.e. wait a bit longer if you just started a jump
            if (ps.inAir) initialAnimSpeed = 0.01f;
            else initialAnimSpeed = 0.8f;
            ps.playerUpperAnim.speed = initialAnimSpeed;
            ps.playerLowerAnim.speed = initialAnimSpeed;
            ps.playerUpperAnim.Play("ShavoSuperIdle");
            ps.playerLowerAnim.Play("ShavoSuperIdle");
            float timeWaited = 0f;
            while (ps.inAir)
            {
                yield return null;
                timeWaited += Time.deltaTime;
            }
            if (timeWaited < 0.32f) yield return new WaitForSeconds(0.32f - timeWaited); // Wait for a certain time minimum regardless of being grounded or in air
            // One more wait
            while (ps.inAir) yield return null;
            ps.playerUpperAnim.Play("ShavoSuper");
            ps.playerLowerAnim.Play("ShavoSuper");
            yield return new WaitForSeconds(0.03f);

            // Start the attack
            ps.blockAttackProgress = false;
            ps.PlayAudioEvent(ps.shavoSuper);   // may need tweaking
            yield return null;
            ps.blockAttackProgress = true;
            ps.playerUpperAnim.speed = 0.77f;
            ps.playerLowerAnim.speed = 0.77f;
            yield return new WaitForSeconds(0.65f);
            ps.blockAttackProgress = false;
            yield return new WaitForSeconds(0.5f);
            ps.blockAttackProgress = true;
            ps.playerUpperAnim.speed = 1f;
            ps.playerLowerAnim.speed = 1f;
        }
        else if (ps.currentBandMember == "John")
        {
            ps.PlayAudioEvent(ps.johnSuper);
            yield return new WaitForSeconds(3.0f);
        }
        else if (ps.currentBandMember == "Daron")
        {
            yield return null;
            ps.PlayAudioEvent(ps.daronSuper);
            ps.playerLowerAnim.Play("DaronIdleLegs");
            ps.playerUpperAnim.Play("DaronSuper");
            if (!ps.moving && !ps.crouched && !ps.inAir)
            {
            }
            yield return new WaitForSeconds(2.25f);

        }
        else if (ps.currentBandMember == "Serj")
        {
            ps.PlayAudioEvent(ps.serjSuper);
            ps.playerLowerAnim.Play("SerjLongLegs");
            ps.playerUpperAnim.Play("SerjSuper");
            yield return new WaitForSeconds(2.57f);
        }
        // After attack, give one second of bonus invincibility
        if (ps.currentBandMember != "John") ps.curInvulnerableTime = 1f;
        ps.blockAttackProgress = false;
        ps.attacking = false;
        ps.isSuperActive = false;
        yield return null;
    }
}
