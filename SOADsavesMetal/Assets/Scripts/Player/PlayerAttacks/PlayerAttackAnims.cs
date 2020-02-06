using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackAnims : MonoBehaviour
{
    public Player ps;

    public IEnumerator shortRangeAttackAnims()
    {
        ps.attacking = true;
        if (ps.crouched)
        {
            //playerSprite.sprite.pivot.Set
            ps.playerUpperAnim.pivotPosition.Set(0.49f, 0.83f, 0.0f);
        }
        ps.playerUpperAnim.Play(ps.GetAnimName("Short"));
        if (ps.currentBandMember != "Daron" && !ps.inAir && !ps.Dead)
        {
            ps.playerLowerAnim.Play(ps.GetAnimName("ShortLegs"));
        }
        if (ps.currentBandMember == "John")
        {
            ps.shortRange.Play("SoundWave");
            ps.auso.PlayOneShot(ps.GetRandomSoundEffect(ps.JohnShortRange));
            yield return new WaitForSeconds(0.55f);
            ps.shortRange.Play("BaseSound");
        }
        else if (ps.currentBandMember == "Shavo")
        {
            ps.playerLowerAnim.pivotPosition.Set(0.0f, -0.83f, 0.0f);
            ps.auso.PlayOneShot(ps.GetRandomSoundEffect(ps.ShavoShortRange));
            yield return new WaitForSeconds(0.5f);
        }
        else if (ps.currentBandMember == "Daron")
        {
            ps.auso.PlayOneShot(ps.DaronShortRange);
            yield return new WaitForSeconds(0.38f);
        }
        else if (ps.currentBandMember == "Serj")
        {
            yield return new WaitForSeconds(0.55f);
        }
        ps.attacking = false;
        ps.movedWhileAttacking = false;
    }
    public IEnumerator longRangeAttackAnims()
    {
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
            ps.auso.PlayOneShot(ps.ShavoLongRange);
            yield return new WaitForSeconds(0.4f);
        }
        else if (ps.currentBandMember == "John")
        {
            ps.auso.PlayOneShot(ps.GetRandomSoundEffect(ps.JohnLongRange));
        }
        else if (ps.currentBandMember == "Daron")
        {
            ps.playerUpperAnim.Play("DaronLong");
            if (!ps.moving && !ps.crouched && !ps.inAir)
            {
                ps.playerLowerAnim.Play("DaronAttackLegs");
            }
            ps.auso.PlayOneShot(ps.GetRandomSoundEffect(ps.DaronLongRangeThrow));
            yield return new WaitForSeconds(0.6f);
        }
        else if (ps.currentBandMember == "Serj")
        {
            ps.playerUpperAnim.Play("SerjLong");
            if (!ps.moving && !ps.crouched && !ps.inAir)
            {
                ps.playerLowerAnim.Play("SerjLongLegs");
            }
            yield return new WaitForSeconds(0.45f);
        }
        yield return new WaitForSeconds(0.07f);
        ps.attacking = false;
    }
    public IEnumerator superAttackAnims()
    {
        ps.attacking = true;
        ps.isSuperActive = true;
        if (ps.currentBandMember == "Shavo")
        {
            float initialAnimSpeed;  // Change the animation timing to account for jumping, i.e. wait a bit longer if you just started a jump
            if (ps.inAir) initialAnimSpeed = 0.01f;
            else initialAnimSpeed = 0.8f;
            ps.playerUpperAnim.speed = initialAnimSpeed;
            ps.playerLowerAnim.speed = initialAnimSpeed;
            ps.playerUpperAnim.Play("ShavoSuper");
            ps.playerLowerAnim.Play("ShavoSuper");
            float timeWaited = 0f;
            while (ps.inAir)
            {
                yield return new WaitForSeconds(0.03f);
                timeWaited += 0.03f;
            }
            if (timeWaited < 0.06f) yield return new WaitForSeconds(0.06f - timeWaited);
            else
            {
                ps.playerUpperAnim.speed = 5f;
                ps.playerLowerAnim.speed = 5f;
                yield return new WaitForSeconds(0.01f);
            }
            ps.auso.PlayOneShot(ps.ShavoSuper);   // may need tweaking
            ps.playerUpperAnim.speed = 0.8f;
            ps.playerLowerAnim.speed = 0.8f;
            yield return new WaitForSeconds(0.95f);
            ps.playerUpperAnim.speed = 1f;
            ps.playerLowerAnim.speed = 1f;
        }
        else if (ps.currentBandMember == "John")
        {
            ps.auso.PlayOneShot(ps.JohnSuper);
            yield return new WaitForSeconds(3.0f);
        }
        else if (ps.currentBandMember == "Daron")
        {
            yield return null;
            ps.auso.PlayOneShot(ps.GetRandomSoundEffect(ps.DaronSuper));
            ps.playerLowerAnim.Play("DaronIdleLegs");
            ps.playerUpperAnim.Play("DaronSuper");
            if (!ps.moving && !ps.crouched && !ps.inAir)
            {
            }
            yield return new WaitForSeconds(2.25f);

        }
        else if (ps.currentBandMember == "Serj")
        {
            ps.playerLowerAnim.Play("SerjLongLegs");
            ps.playerUpperAnim.Play("SerjSuper");
            yield return new WaitForSeconds(1.27f);
        }
        ps.attacking = false;
        ps.isSuperActive = false;
    }
}
