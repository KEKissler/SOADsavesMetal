using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDRBeamManager : MonoBehaviour
{
    public DDRBeam.Direction direction;

    private void OnTriggerExit2D(Collider2D other)
    {
        GameObject player = other.gameObject;
        if (player.tag == "Player")
        {
            switch(direction)
            {
                case DDRBeam.Direction.Up:
                    if (!(player.GetComponent<Rigidbody2D>().velocity.y > 0))
                        other.gameObject.GetComponent<Player>().Health -= 1;
                    break;
                case DDRBeam.Direction.Right:
                    if(!(player.GetComponent<Rigidbody2D>().velocity.x > 0))
                        other.gameObject.GetComponent<Player>().Health -= 1;
                    break;
                case DDRBeam.Direction.Left:
                    if (!(player.GetComponent<Rigidbody2D>().velocity.x < 0))
                        other.gameObject.GetComponent<Player>().Health -= 1;
                    break;
                case DDRBeam.Direction.Down:
                    if (!(player.GetComponent<Rigidbody2D>().velocity.y < 0 || player.GetComponent<Player>().crouched))
                        other.gameObject.GetComponent<Player>().Health -= 1;
                    break;
            }
        }
    }
}
