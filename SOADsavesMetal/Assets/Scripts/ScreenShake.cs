using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    private float shakeAmount = 0f;
    private Vector3 position;
    private System.Random random = new System.Random();

    private void Start()
    {
        position = transform.position;
    }

    private void Update()
    {
        float randomAngle = (float)random.NextDouble() * 360f;
        transform.position = position + new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle), 0);
        if(shakeAmount > 0)
        {
            shakeAmount -= 0.01f;
        }
    }

    public void shake()
    {
        shakeAmount = 0.5f;
    }
}
