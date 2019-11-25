using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "New Attack/ElectricShockAttack_random")]
public class ElectircShock_Random : TsovinarAttack
{
    private const string ON_STATE = "wire_warning";
    private const string OFF_STATE = "wire_off";

    public GameObject wirePrefab;
    public float CycleTime;

    [Range(1, 6)]
    public int activeWireCount;

    private bool[] wireOnOff = new bool[5];

    private Transform wire1Location;
    private Transform wire2Location;
    private Transform wire3Location;
    private Transform wire4Location;
    private Transform wire5Location;
    private Transform wire6Location;

    private Transform attackParent;
    private Animator wire1;
    private Animator wire2;
    private Animator wire3;
    private Animator wire4;
    private Animator wire5;
    private Animator wire6;

    private readonly Dictionary<int, Animator> wireMap = new Dictionary<int, Animator>();

    protected override void OnEnd()
    {
        wire1.Play(OFF_STATE);
        wire2.Play(OFF_STATE);
        wire3.Play(OFF_STATE);
        wire4.Play(OFF_STATE);
        wire5.Play(OFF_STATE);
        wire6.Play(OFF_STATE);
        Destroy(wire1.gameObject);
        Destroy(wire2.gameObject);
        Destroy(wire3.gameObject);
        Destroy(wire4.gameObject);
        Destroy(wire5.gameObject);
        Destroy(wire6.gameObject);
        wireMap.Clear();
        Debug.Log("Shock off");
    }

    protected override void OnStart()
    {
        Debug.Log("Shock on");
        wire1 = Instantiate(wirePrefab, wire1Location.position, Quaternion.identity, attackParent).GetComponent<Animator>();
        wire2 = Instantiate(wirePrefab, wire2Location.position, Quaternion.identity, attackParent).GetComponent<Animator>();
        wire3 = Instantiate(wirePrefab, wire3Location.position, Quaternion.identity, attackParent).GetComponent<Animator>();
        wire4 = Instantiate(wirePrefab, wire4Location.position, Quaternion.identity, attackParent).GetComponent<Animator>();
        wire5 = Instantiate(wirePrefab, wire5Location.position, Quaternion.identity, attackParent).GetComponent<Animator>();
        wire6 = Instantiate(wirePrefab, wire6Location.position, Quaternion.identity, attackParent).GetComponent<Animator>();
        wireMap.Add(0, wire1);
        wireMap.Add(1, wire2);
        wireMap.Add(2, wire3);
        wireMap.Add(3, wire4);
        wireMap.Add(4, wire5);
        wireMap.Add(5, wire6);

        wire1.Play(OFF_STATE);
        wire2.Play(OFF_STATE);
        wire3.Play(OFF_STATE);
        wire4.Play(OFF_STATE);
        wire5.Play(OFF_STATE);
        wire6.Play(OFF_STATE);
    }

    private void TurnOn()
    {
        int randNum;
        for (int i = 0; i < activeWireCount; ++i)
        {
            randNum = Random.Range(0, 5);
            if (wireOnOff.All(a => a))
            {
                break;
            }
            while (wireOnOff[randNum])
            {
                randNum = Random.Range(0, 5);
            }
            wireOnOff[randNum] = true;
        }

        for(int i = 0; i < wireOnOff.Length; ++i)
        {
            if (wireOnOff[i])
            {
                wireMap[i].Play(ON_STATE);      
            }
        }

   
    }

    protected override IEnumerator Execute(float duration)
    {
        yield return new WaitForEndOfFrame();

        Debug.Log("Setup");
        while (true)
        {
            TurnOn();
            yield return new WaitForSeconds(CycleTime);
            wire1.Play(OFF_STATE);
            wire2.Play(OFF_STATE);
            wire3.Play(OFF_STATE);
            wire4.Play(OFF_STATE);
            wire5.Play(OFF_STATE);
            wire6.Play(OFF_STATE);
            for (int i = 0; i < wireOnOff.Length; ++i)
            {
                wireOnOff[i] = false;
            }
        }
    }

    public override void Initialize(TsovinarAttackData data)
    {
        wire1Location = data.wire1;
        wire2Location = data.wire2;
        wire3Location = data.wire3;
        wire4Location = data.wire4;
        wire5Location = data.wire5;
        wire6Location = data.wire6;
        attackParent = data.attackParent;
    }
}
