using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    private void Destroy()
    {
        Destroy(transform.parent.gameObject);
    }
}
