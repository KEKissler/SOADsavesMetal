using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "New Attack/Agas/LightCandle")]
public class LightCandle : AgasAttack
{
    public int Min_MaxShots;
    public int Max_MaxShots;
    public float Min_FirePeriod;
    public float Max_FirePeriod;
    public int Min_Speed;
    public int Max_Speed;

    private readonly CandleEmitter[] candles = new CandleEmitter[7];

    public override void Initialize(AgasAttackData data)
    {
        candles[0] = data.candle0;
        candles[1] = data.candle1;
        candles[2] = data.candle2;
        candles[3] = data.candle3;
        candles[4] = data.candle4;
        candles[5] = data.candle5;
        candles[6] = data.candle6;
    }

    protected override IEnumerator Execute(float duration)
    {
        yield return new WaitForEndOfFrame();
        var candle = GetRandomUnlitCandle();
        if(candle == null)
        {
            Debug.Log("Could not find a candle to light.");
            yield break;
        }
        candle.enableFire();
        candle.setMaxShots(Random.Range(Min_MaxShots, Max_MaxShots + 1));
        candle.setFirePeriod(Random.Range(Min_FirePeriod, Max_FirePeriod));
        candle.setSpeed(Random.Range(Min_Speed, Max_Speed + 1));
    }

    protected override void OnEnd()
    {
        //candle state persists beyond this attack, so its in the scene and is not destroyed or managed here
    }

    protected override void OnStart()
    {
        
    }

    private CandleEmitter GetRandomUnlitCandle()
    {
        if (candles.All(a => a.getCandleActive()))
        {
            return null;
        }
        var inactiveCandles = candles.Where(a => !a.getCandleActive()).ToList();
        var selectedCandle = Random.Range(0, inactiveCandles.Count);
        return inactiveCandles[selectedCandle];
    }
}
