using UnityEngine;

[System.Serializable]
public class SandarametAttackData 
{
    public Transform attackParent;
    public GameObject player;
    public GameObject sandaramet;
    public Transform frontHand;
    public Transform backHand;
    public Transform topHand;
    public Transform faceHand;
    public Transform bottomFrontHand;
    public Transform bottomBackHand;
    public Collider2D screenTop;
    public Collider2D screenBottom;
}
