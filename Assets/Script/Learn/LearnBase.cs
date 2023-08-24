using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Learn
{
    public class LearnBase : MonoBehaviour
    {
        [Header("Informations")]
        public string displayName;
        public string description;
        public string damageDescription;

        [Header("Stats")]
        public LearnLevelEnum level = LearnLevelEnum.Commom;
        public LearnTypeEnum type = LearnTypeEnum.Attack;

        public virtual void SetDirection(float dir) { }
    }

    public enum LearnLevelEnum
    {
        Commom,
        Rare,
        Epic
    }

    public enum LearnTypeEnum
    {
        Attack,
        Skill
    }
}
