using Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "ScriptableObjects/Stats", order = 1)]
public class StatsScriptableObject : ScriptableObject
{
    [Header("Stats")]
    public float intelligence;
    public float strength;
    public float dexterity;
    public float constitution;
    public float charisma;
    public float perception; // size used to detection if player its close.
    public float protection;
    public DiceType damagerDice;
}
