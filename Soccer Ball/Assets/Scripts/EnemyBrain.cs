using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class EnemyBrain : ScriptableObject
{
    public List<EnemyAiVer2> CurrentlyPursuingPlayer;
    public int MaxPursuers;

    public void CheckiFCanPersue(EnemyAiVer2 ai)
    {
        if(CurrentlyPursuingPlayer.Count <MaxPursuers)
        {
            CurrentlyPursuingPlayer.Add(ai);
        }
    }
}
