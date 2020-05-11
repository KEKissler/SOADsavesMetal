using UnityEngine;

public class ArmEnabler : MonoBehaviour
{
    public GameObject arm;

    void enableArm()
    {
        arm.SetActive(true);
    }
}
