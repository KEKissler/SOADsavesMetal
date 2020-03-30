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
    private GameObject beam;
    private GameObject[] screens;
    private Vector3[] bossSizes;
    private GameObject tsovinar;
    private Transform arrow;

    public override void Initialize(TsovinarAttackData data)
    {
        tsovinar = data.tsovinar;
        screens = new GameObject[] { data.screen4, data.screen2, data.screen3, data.screen5 };
        bossSizes = new Vector3[] { new Vector3(1.6f, 1.6f, 1.6f),
                                    new Vector3(0.8f, 0.8f, 0.8f),
                                    new Vector3(1.2f, 1.2f, 1.2f),
                                    new Vector3(2f, 2f, 2f),
                                    };
    }

    protected override void OnStart()
    {
        int dir = RNG.Next(4);
        float tempVel = velocity;
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
                tempVel -= 4;
                break;
            case 3:
                direction = Direction.Down;
                arrow.Rotate(new Vector3(0, 0, 90));
                break;
            default:
                Debug.Log("DDRBeam selected a direction that doesn't exist.");
                break;
        }
        
        arrow.GetComponent<SpriteRenderer>().color = screens[dir].GetComponent<SpriteRenderer>().color;
        tsovinar.transform.localScale = bossSizes[dir];
        tsovinar.transform.localRotation = Quaternion.identity;
        tsovinar.transform.localPosition = screens[dir].transform.position;
        beam.GetComponent<DDRBeamManager>().direction = direction;
        beam.GetComponent<DDRBeamManager>().velocity = tempVel;
    }

    void Update()
    {

    }

    protected override IEnumerator Execute(float duration)
    {
        yield return new WaitForSeconds(duration);
    }

    protected override void OnEnd()
    {
    }
}
