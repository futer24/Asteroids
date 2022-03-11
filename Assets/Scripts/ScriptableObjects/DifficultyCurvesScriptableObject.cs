using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DifficultyCurves Configuration", menuName = "ScriptableObject/DifficultyCurves Configuration")]
public class DifficultyCurvesScriptableObject : ScriptableObject
{
    public AnimationCurve SpeedCurve;
    public AnimationCurve SpawnRateCurve;
    public AnimationCurve SpawnCountCurve;
}
