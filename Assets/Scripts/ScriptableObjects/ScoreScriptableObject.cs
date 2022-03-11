using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScoreScriptableObject", menuName = "ScoreScriptableObject", order = 0)]
public class ScoreScriptableObject : UnityEngine.ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField] private int initScore = default;
    [NonSerialized] public int Score;

    public int HighScore;

    public void OnAfterDeserialize()
    {
        InitValues();
    }

    public void InitValues()
    {
        Score = initScore;
    }
    public void OnBeforeSerialize() { /* do nothing */ }

    public void AddScore(int newScore)
    {
        Score += newScore;
        if (Score > HighScore) HighScore = Score;
    }
}
