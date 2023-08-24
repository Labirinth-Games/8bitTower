using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using DG.Tweening;

namespace Helpers
{
    public class AttackHoldHelpers : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float fireRate = .2f;
        [SerializeField] private GameObject barAttackPrefab;
        [SerializeField] private float holdMedium = 1f;
        [SerializeField] private float holdLong = 2f;

        [Header("Callback")]
        public UnityEvent OnShortPress;
        public UnityEvent OnMediumPress;
        public UnityEvent OnLongPress;
        public UnityEvent OnAttackFire;

        private float _startTimer;

        private float _fireRateCooldown;
        private bool _isLoaddingAttack = false;
        private bool _isHold = false;

        public void Hold(GlobalControls globalControls)
        {
            if (globalControls.Player.Attack.ReadValue<float>() == 1 && _isHold == false)
            {
                _startTimer = Time.time;
                _isLoaddingAttack = true;
                _isHold = true;
            }

            if (globalControls.Player.Attack.ReadValue<float>() == 0 && _isHold)
            {
                _isLoaddingAttack = false;
                _isHold = false;

                OnAttackFire?.Invoke();

                if(Time.time > _fireRateCooldown)
                {
                    _fireRateCooldown = Time.time + fireRate;

                    if (_startTimer + holdLong < Time.time)
                    {
                        OnLongPress?.Invoke();
                    }
                    else if (_startTimer + holdMedium < Time.time)
                    {
                        OnMediumPress?.Invoke();
                    }
                    else
                    {
                        OnShortPress?.Invoke();
                    }
                }
            }
        }

        private void IncrementBarRender()
        {
            if (_startTimer + .5f > Time.time) return; // only show bar when hold for along time 

            var scale = barAttackPrefab.transform.localScale;
            scale.x += .003f;
            
            if(scale.x < 2.2)
            {
                barAttackPrefab.transform.localScale = scale;

                if (_startTimer + holdLong < Time.time)
                    barAttackPrefab.GetComponent<SpriteRenderer>().color = Utils.ColorUtils.HexToColor("#957acc");
                else if (_startTimer + holdMedium < Time.time)
                    barAttackPrefab.GetComponent<SpriteRenderer>().color = Utils.ColorUtils.HexToColor("#e7b21c");
                else
                    barAttackPrefab.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }

        private void ResetBarRender()
        {
            var scale = barAttackPrefab.transform.localScale;
            scale.x = 0;

            barAttackPrefab.transform.localScale = scale;
        }

        private void Update()
        {
            if (_isLoaddingAttack) IncrementBarRender();
            else ResetBarRender();
        }
    }
}