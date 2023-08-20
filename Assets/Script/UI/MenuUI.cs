using Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class MenuUI : MonoBehaviour
    {
        public bool isActive = false;

        public void SetConfigs(string title)
        {
            if(TryGetComponent(out TextMeshProUGUI label))
            {
                label.text = title;
            } 
        }

        private void FixedUpdate()
        {
            if (TryGetComponent(out TextMeshProUGUI label))
            {
                if(isActive)
                {
                    label.color = Color.red;
                }
                else
                {
                    label.color = Color.white;
                }
            }
        }
    }

}