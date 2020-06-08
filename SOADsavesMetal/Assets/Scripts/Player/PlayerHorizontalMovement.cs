using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHorizontalMovement : MonoBehaviour
{
    public Player ps;
    
    private bool tryMove;
    private float input;
    // private const float CROUCH_SPEED_MODIFIER = 0.2f;

    private void Start()
    {
        tryMove = false;
    }

    public void HandleHorizontalMovement()
    {
        if (ps.blockHorizontalMovement ||
            ((ps.currentBandMember == "Daron" || ps.currentBandMember == "Serj") && ps.isSuperActive))
        { 
            return;
        }

        // Debug.Log("old speed " + oldSpeed);
        input = Input.GetAxisRaw(ps.GetHori());
        tryMove = true;
    }

    private void Move()
    {
        if (input != 0f)
        {
            float oldSpeed = ps.rb.velocity.x; // Grounded can only occur against flat surfaces below player, speed should only be in x dir
            // Handle changing direction
            if (input > 0)
            {
                // Face right if needed
                if (gameObject.transform.rotation.y != 0)
                {
                    gameObject.transform.Rotate(Vector3.up, 180.0f);
                }
            }
            else
            {
                // Face left if needed
                if (gameObject.transform.rotation.y == 0)
                {
                    gameObject.transform.Rotate(Vector3.up, 180.0f);
                }
            }

            if (ps.crouched) return;

            // If currently executing super, don't move (unless the player is John, the drummer)
            if (ps.currentBandMember != "John" && ps.isSuperActive) return;

            ps.moving = true;
            if (ps.attacking) ps.movedWhileAttacking = true;
            float accel, decel, maxSpeed;

            // Set parameters for movement
            if (!ps.inAir)
            {
                // Ground movement
                ps.PlayAnims("Walk");
                accel = ps.groundAccel;
                decel = ps.groundDecel;
                maxSpeed = ps.maxGroundSpeed;
            }
            else
            {
                // Air movement
                accel = ps.airAccel;
                decel = ps.airDecel;
                maxSpeed = ps.maxAirSpeed;
            }

            // Moving in direction of current velocity or when player was not moving before
            if ((Mathf.Sign(oldSpeed - ps.desiredVelocity) == Mathf.Sign(input)) || Mathf.Approximately(oldSpeed - ps.desiredVelocity, 0))
            {
                // If applying max accel would not put speed above target limit
                if (Mathf.Abs(oldSpeed + (input * accel * Time.deltaTime)) < maxSpeed)
                {
                    ps.rb.velocity = new Vector3(oldSpeed + (input * accel * Time.deltaTime), ps.rb.velocity.y, 0);
                }
                // Would go beyond limit
                else
                {
                    // Set velocity to either the targetWalkSpeed or leave it untouched if player was already traveling faster
                    if (Mathf.Abs(ps.rb.velocity.x) < maxSpeed)
                    {
                        // Debug.Log("Set to target walk speed!");
                        ps.rb.velocity = new Vector2(Mathf.Sign(oldSpeed) * maxSpeed, ps.rb.velocity.y);
                    }
                }
            }
            // Fighting velocity
            else
            {
                //no check needed, losing speed and minimum is zero.
                ps.rb.velocity = new Vector2(oldSpeed + (input * decel * Time.deltaTime), ps.rb.velocity.y);
            }

        }
        else if (!ps.crouched && !ps.inAir && !ps.Dead)
        {
            // moving = false;
            if (!ps.attacking && !ps.Dead)
            {
                ps.playerUpperAnim.Play(ps.GetAnimName("Idle"));
                ps.playerLowerAnim.Play(ps.GetAnimName("IdleLegs"));
            }
            else if (ps.attacking && ps.movedWhileAttacking)
            {
                ps.playerLowerAnim.Play(ps.GetAnimName("LandLegs"));
            }
        }

    }

    private void FixedUpdate()
    {
        if(tryMove)
        {
            Move();
            tryMove = false;
        }
    }

    public IEnumerator blockMovement(float duration)
    {
        ps.blockHorizontalMovement = true;
        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        ps.blockHorizontalMovement = false;
    }
}
