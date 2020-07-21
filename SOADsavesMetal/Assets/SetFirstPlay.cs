using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFirstPlay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StaticData.firstPlay = false;
        SaveSystem.SaveGame();
    }
}
