using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "New Attack/ElectricShockAttack_oddEven")]
public class ElectricShock_OddEven : TsovinarAttack
{
    private const string ON_STATE = "wire_warning";
    private const string OFF_STATE = "wire_off";

    public GameObject wirePrefab;
    public float CycleTime;

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

    private bool isOdd;
    

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
        wire1.Play(OFF_STATE);
        wire2.Play(OFF_STATE);
        wire3.Play(OFF_STATE);
        wire4.Play(OFF_STATE);
        wire5.Play(OFF_STATE);
        wire6.Play(OFF_STATE);
    }

    protected override IEnumerator Execute(float duration)
    {
        yield return new WaitForEndOfFrame();
        Debug.Log("Shocking!");
        while (true)
        {
            isOdd = !isOdd;
            if (isOdd)
            {
                wire1.Play(ON_STATE);
                wire3.Play(ON_STATE);
                wire5.Play(ON_STATE);
                wire2.Play(OFF_STATE);
                wire4.Play(OFF_STATE);
                wire6.Play(OFF_STATE);
            }
            else
            {
                wire2.Play(ON_STATE);
                wire4.Play(ON_STATE);
                wire6.Play(ON_STATE);
                wire1.Play(OFF_STATE);
                wire3.Play(OFF_STATE);
                wire5.Play(OFF_STATE);
            }
            yield return new WaitForSeconds(CycleTime);
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
