using Learn;
using Manager;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEditor;
using UnityEngine;

namespace HUD
{
    public class QuickMenuLearnHud : MonoBehaviour
    {
        [SerializeField] private SlotDescriptionHud descriptionPrefab;
        [SerializeField] private SlotManager slotManager;

        private int _activeIndex = 0;

        private void RenderSlotDescription()
        {
            if (!descriptionPrefab.gameObject.activeSelf)
                descriptionPrefab.gameObject.SetActive(true);

            if (descriptionPrefab.TryGetComponent(out SlotDescriptionHud slotDescriptionHud))
            {
                LearnBase learn = slotManager.GetLearn(_activeIndex).learn;

                slotDescriptionHud.SetTitle(learn.displayName);
                slotDescriptionHud.SetDamage(learn.damageDescription);
            }
        }

        private void SwitchItem()
        {
            if (!GameManager.Instance.globalControls.QuickMenuLearn.Move.WasPressedThisFrame()) return;

            var moveTo = GameManager.Instance.globalControls.QuickMenuLearn.Move.ReadValue<float>();
            int newPosition = (int)Mathf.Sign(moveTo);
            int oldPosition = _activeIndex;

            if (_activeIndex + newPosition == slotManager.Size())
            {
                _activeIndex = 0;
            }
            else if (_activeIndex + newPosition < 0)
            {
                _activeIndex = slotManager.Size() - 1;
            }
            else
            {
                _activeIndex += newPosition;
            }

            slotManager.GetLearn(oldPosition).SetDeselect();
            slotManager.GetLearn(_activeIndex).SetSelect();

            RenderSlotDescription();
        }

        private void Update()
        {
            SwitchItem();
        }

        private void Start()
        {
            slotManager.Init();
        }

        private void OnValidate()
        {
            if (TryGetComponent(out SlotManager slotManager))
                this.slotManager = slotManager;
        }
    }
}
