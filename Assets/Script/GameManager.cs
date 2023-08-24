using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class GameManager : SinglentonMonobehavior
    {
        [Header("References")]
        [SerializeField] private GameObject menuPausePrefab;
        [SerializeField] private GameObject menuQuickMenuLearnPrefab;
        [SerializeField] private GameObject menuGameOverPrefab;
        public LearnManager learnManager;

        [Header("settings")]
        public StatsScriptableObject playerStats;
        public bool isPaused { get; private set; } = false;

        #region Controller Game
        public void NextLevel(Player player)
        {
            // TODO next level and up player
        }

        public void ReloadScene() 
        {
            SceneManager.LoadScene(0);
        }
        #endregion

        #region Controller Menus
        public void TogglePause()
        {
            isPaused = !isPaused;
        }

        private void Pause()
        {
            if (globalControls.Game.Pause.WasPressedThisFrame())
            {
                TogglePause();
                menuPausePrefab.SetActive(isPaused);
            }
        }
        
        public void GameOver()
        {
            menuGameOverPrefab.SetActive(true);
        }

        public void QuickMenuLearn()
        {
            if (globalControls.Game.QuickMenuLearn.WasPressedThisFrame())
            {
                TogglePause();
                menuQuickMenuLearnPrefab.SetActive(isPaused);
            }
        }
        #endregion

        #region Unity Events
        private void Update()
        {
            Pause();
            QuickMenuLearn();
        }

        protected override void Awake()
        {
            base.Awake();
            _instance = this;
        }
        #endregion
    }


    public class SinglentonMonobehavior : MonoBehaviour
    {
        protected static GameManager _instance;
        public GlobalControls globalControls { get; private set; }

        public static GameManager Instance
        {
            get
            {
                if (_instance is null)
                    Debug.Log("GameManager is null");

                return _instance;
            }
        }

        private void OnEnable()
        {
            globalControls.Enable();
        }

        private void OnDisable()
        {
            globalControls.Disable();
        }

        protected virtual void Awake()
        {
            globalControls = new GlobalControls();
        }
    }
}
