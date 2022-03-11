using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDataScriptableObject", menuName = "EnemyDataScriptableObject", order = 0)]
public class EnemyDataScriptableObject : ScriptableObject 
{
     public int size;
     public float speed;
     public float torqueRange;
     public int scorePointsForDestroy;
}
