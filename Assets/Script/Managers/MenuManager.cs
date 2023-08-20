using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UI;

namespace Manager
{
    public class MenuManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject itemMenuPrefab;

        [Header("Settings")]
        [SerializeField] private List<MenuInfo> menu = new List<MenuInfo>();

        private int _activeIndex = 0;

        private void Init()
        {
            for(var i = 0; i < menu.Count; i++)
            {
                var instance = Instantiate(itemMenuPrefab, transform);
                var menuUI = instance.GetComponent<MenuUI>();

                menuUI.SetConfigs(menu[i].title);
                menuUI.transform.position = new Vector2(menuUI.transform.position.x, menuUI.transform.position.y - 50 * i);

                if(i == _activeIndex) menuUI.isActive = true;

                menu[i].instance = instance;
            }
        }

        // this method do switch item selected on menu
        private void SwitchItem() 
        {
            if (!GameManager.Instance.globalControls.Menu.Move.WasPressedThisFrame()) return;

            var moveTo = GameManager.Instance.globalControls.Menu.Move.ReadValue<float>();
            int newPosition = (int)Mathf.Sign(moveTo);
            int oldPosition = _activeIndex;

            if (_activeIndex + newPosition == menu.Count)
            {
                _activeIndex = 0;
            }
            else if (_activeIndex + newPosition < 0)
            {
                _activeIndex = menu.Count - 1;
            }
            else
            {
                _activeIndex += newPosition;
            }

            menu[oldPosition].instance.GetComponent<MenuUI>().isActive = false;
            menu[_activeIndex].instance.GetComponent<MenuUI>().isActive = true;

        }

        private void SelectItem()
        {
            if (GameManager.Instance.globalControls.Menu.Select.triggered)
                menu[_activeIndex].action?.Invoke();
        }

        #region Unity Event
        private void Update()
        {
            SwitchItem();
            SelectItem();
        }

        private void Start()
        {
            Init();
        }
        #endregion
    }

    [System.Serializable]
    public class MenuInfo
    {
        public string title;
        public GameObject instance;
        public UnityEvent action;
        //public List<MenuInfo> subMenu;
    }
}
