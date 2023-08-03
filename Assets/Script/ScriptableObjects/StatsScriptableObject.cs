using Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "ScriptableObjects/Stats", order = 1)]
public class StatsScriptableObject : ScriptableObject
{
    [Header("Stats")]
    public float intelligence = 1;
    public float strength = 1;
    public float dexterity = 1;
    public float constitution = 1;
    public float charisma = 1;
    public float perception = 1; // size used to detection if player its close.
    public float protection = 1;
    public float luck = 1;

    [Header("Dices")]
    public DiceType damagerDice;
}
