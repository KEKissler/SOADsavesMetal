using System.Collections;
using UnityEngine;
[CreateAssetMenu(menuName = "New Attack/ElectricShockAttack")]
public class ElectricShock : BossAttacks
{
    public Transform wire;

    protected override void OnEnd()
    {
        Debug.Log("Shock off");
    }

    protected override void OnStart()
    {
        Debug.Log("Shock on");
    }

    protected override IEnumerator Update(float duration)
    {
        Debug.Log("Shocking!");
        yield return new WaitForSeconds(duration);
    }
}
