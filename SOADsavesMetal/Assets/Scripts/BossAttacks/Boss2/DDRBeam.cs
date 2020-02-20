using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Attack/Tsovinar/DDRBeam")]
public class DDRBeam : TsovinarAttack
{
    [System.Serializable]
    public enum Direction
    {
        Right,
        Up,
        Left,
        Down
    }

    public GameObject ddrbeamPrefab;
    [SerializeField]
    private Direction direction;
    [SerializeField]
    private float velocity = -10;

    private readonly System.Random RNG = new System.Random();
    private Color color;
    private GameObject beam;
    private GameObject[] screens = new GameObject[4];
    private Transform arrow;

    public override void Initialize(TsovinarAttackData data)
    {
        screens = new GameObject[] { data.screen4, data.screen2, data.screen3, data.screen5 };
    }

    protected override void OnStart()
    {
        int dir = RNG.Next(4);
        beam = Instantiate(ddrbeamPrefab);
        arrow = beam.transform.GetChild(1);
        switch (dir)
        {
            case 0:
                direction = Direction.Right;
                arrow.Rotate(new Vector3(0, 0, 180));
                break;
            case 1:
                direction = Direction.Up;
                arrow.Rotate(new Vector3(0, 0, -90));
                break;
            case 2:
                direction = Direction.Left;
                velocity -= 4;
                break;
            case 3:
                direction = Direction.Down;
                arrow.Rotate(new Vector3(0, 0, 90));
                break;
            default:
                Debug.Log("DDRBeam selected a direction that doesn't exist.");
                break;
        }
        
        arrow.GetComponent<SpriteRenderer>().color = color = screens[dir].GetComponent<SpriteRenderer>().color;
        beam.GetComponent<DDRBeamManager>().direction = direction;
        beam.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity, 0);
    }

    protected override IEnumerator Execute(float duration)
    {
        yield return new WaitForSeconds(duration);
    }

    protected override void OnEnd()
    {
    }
}
