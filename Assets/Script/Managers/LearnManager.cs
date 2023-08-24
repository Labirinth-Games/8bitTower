using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Learn;

namespace Manager
{
    public class LearnManager : MonoBehaviour
    {
        public List<LearnBase> learns = new List<LearnBase>();
        public List<LearnBase> myLearns = new List<LearnBase>();
        public LearnBase attackSecond { get; private set; }
        public LearnBase attackThirth { get; private set; }

        public void SetSecondAttack(LearnBase attack)
        {
            attackSecond = attack;
        }
        
        public void SetThirthAttack(LearnBase attack)
        {
            attackThirth = attack;
        }

        public void GetALearn()
        {
            myLearns.Add(learns[Random.Range(0, learns.Count)]);
        }
    }
}
