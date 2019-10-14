using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Attack/ElectricShockAttack_wave")]
public class ElectricShock_Wave : TsovinarAttack
{
    private const string ON_STATE = "wire_on";
    private const string OFF_STATE = "wire_off";

    public GameObject wirePrefab;
    public float CycleTime;
    [Range(0, 5)]
    public int start;
    public bool isMovingRight;
    public int waveWidth;
        
    private int first, last;

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

    private void Advance()
    {
        first = PositiveMod((first + (isMovingRight ? 1 : -1)), 6);
        last = PositiveMod((last + (isMovingRight ? 1 : -1)), 6);
    }

    private int PositiveMod(int value, int n)
    {
        //C# why ;-;
        var result = value % n;
        if(result < 0)
        {
            return result + n;
        }
        return result;
    }

    protected override IEnumerator Execute(float duration)
    {
        yield return new WaitForEndOfFrame();
        first = start;
        last = start;
        //go back 2 for last
        isMovingRight = !isMovingRight;
        for(int i = 0; i < waveWidth; ++i)
        {
            Advance();
        }
        first = start;
        isMovingRight = !isMovingRight;

        for (int i = 0; i < waveWidth; ++i)
        {
            wireMap[first].Play(ON_STATE);
            yield return new WaitForSeconds(CycleTime);
            Advance();
        }

        Debug.Log("Setup");
        while (true)
        {
            wireMap[first].Play(ON_STATE);
            wireMap[last].Play(OFF_STATE);
            yield return new WaitForSeconds(CycleTime);
            Advance();
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

