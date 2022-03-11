using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameDataScriptableObject", menuName = "GameDataScriptableObject", order = 0)]
public class GameDataScriptableObject : UnityEngine.ScriptableObject, ISerializationCallbackReceiver
{

   // [Header("Scriptable Object References")]
    [SerializeField] private int currentLives = 3;
    [NonSerialized] public int Lives;
    [NonSerialized] public int Wave = 1;
    public int maxWaves = 12;

    public void OnAfterDeserialize()
    {
        InitValues();
    }

    public void InitValues()
    {
        Lives = currentLives;
    }

    public void OnBeforeSerialize()
    {
         
    }
}
