using System.Collections.Generic;
using UnityEngine;

public class BossPhaseInitializer : MonoBehaviour
{
    public Transform BossPosition;
    public Player Player;
    public List<BossPhase> PhasesToInitialize;

    void Start()
    {
        foreach (var phase in PhasesToInitialize)
        {
            phase.Player = Player;
            phase.BossPosition = BossPosition;
        }
    }
}
