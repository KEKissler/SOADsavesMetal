using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Attack/White Noise Attack")]
public class WhiteNoiseAttack : TsovinarAttack
{
    private const string STATIC_ON = "static_on";
    private const string STATIC_OFF = "static_off";
    private const string ANTENNA_UNFOLD = "antenna_unfold";
    private const string ANTENNA_BLINK = "antenna_blinkrapid";
    private const string ANTENNA_FOLD = "antenna_fold";

    public Material whiteNoise;
    public GameObject screens;
    public GameObject antenna;
    public float CycleTime;

    private Transform antennaLocation;

    private Transform attackParent;
    private GameObject screen1;
    private GameObject screen2;
    private GameObject screen3;
    private GameObject screen4;
    private GameObject screen5;

    private Material screen1DefaultMat;
    private Material screen2DefaultMat;
    private Material screen3DefaultMat;
    private Material screen4DefaultMat;
    private Material screen5DefaultMat;

    private Animator telescopingAntenna;

    private bool antennaActive;

    public override void Initialize(TsovinarAttackData data)
    {
        screen1DefaultMat = screen1.GetComponent<MeshRenderer>().material;
        screen2DefaultMat = screen2.GetComponent<MeshRenderer>().material;
        screen3DefaultMat = screen3.GetComponent<MeshRenderer>().material;
        screen4DefaultMat = screen4.GetComponent<MeshRenderer>().material;
        screen5DefaultMat = screen5.GetComponent<MeshRenderer>().material;
        antennaLocation = data.telescopingAntenna;
        attackParent = data.attackParent;
    }

    private void checkAntenna()
    {

    }

    protected override IEnumerator Execute(float duration)
    {
        yield return new WaitForEndOfFrame();
        Debug.Log("John Cena");
        while (true)
        {
            checkAntenna();
            if (!antennaActive)
            {
                telescopingAntenna.Play(ANTENNA_FOLD);
                screen1.GetComponent<MeshRenderer>().material = screen1DefaultMat;
                screen2.GetComponent<MeshRenderer>().material = screen2DefaultMat;
                screen3.GetComponent<MeshRenderer>().material = screen3DefaultMat;
                screen4.GetComponent<MeshRenderer>().material = screen4DefaultMat;
                screen5.GetComponent<MeshRenderer>().material = screen5DefaultMat;
            }
            else
            {
                telescopingAntenna.Play(ANTENNA_BLINK);
                screen1.GetComponent<MeshRenderer>().material = whiteNoise;
                screen2.GetComponent<MeshRenderer>().material = whiteNoise;
                screen3.GetComponent<MeshRenderer>().material = whiteNoise;
                screen4.GetComponent<MeshRenderer>().material = whiteNoise;
                screen5.GetComponent<MeshRenderer>().material = whiteNoise;

            }
            yield return new WaitForSeconds(CycleTime);
        }
    }

    protected override void OnEnd()
    {
        screen1.GetComponent<MeshRenderer>().material = screen1DefaultMat;
        screen2.GetComponent<MeshRenderer>().material = screen2DefaultMat;
        screen3.GetComponent<MeshRenderer>().material = screen3DefaultMat;
        screen4.GetComponent<MeshRenderer>().material = screen4DefaultMat;
        screen5.GetComponent<MeshRenderer>().material = screen5DefaultMat;
    }

    protected override void OnStart()
    {
        Debug.Log("Antenna Up");
        telescopingAntenna = Instantiate(antenna, antennaLocation.position, Quaternion.identity, attackParent).GetComponent<Animator>();

        antennaActive = true;
        telescopingAntenna.Play(ANTENNA_UNFOLD);
    }

    
}
