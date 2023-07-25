using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helpers
{
    public class DiceHelper : MonoBehaviour
    {
        public int Roll(DiceType diceType)
        {
            return Random.Range(1, (int)diceType);
        }
    }


    public enum DiceType
    {
        D4 = 4,
        D6 = 6,
        D8 = 8,
        D12 = 12,
        D20 = 20
    }
}
