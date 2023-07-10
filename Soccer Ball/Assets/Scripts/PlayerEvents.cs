using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerEvents : ScriptableObject
{
    public Action<float> onChargeValue;
    public Action<float> onChargeFirection;
}
