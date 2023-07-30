using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class GameManager : SinglentonMonobehavior
    {
        public StatsScriptableObject playerStats;

        private void Awake()
        {
            _instance = this;
        }
    }


    public class SinglentonMonobehavior : MonoBehaviour
    {
        protected static GameManager _instance;
        public static GameManager Instance
        {
            get
            {
                if (_instance is null)
                    Debug.Log("GameManager is null");

                return _instance;
            }
        }
    }
}
