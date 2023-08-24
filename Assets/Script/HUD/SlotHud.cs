using DG.Tweening;
using Learn;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HUD
{
    public class SlotHud : MonoBehaviour
    {
        [SerializeField] private GameObject preview;
        public bool isSelected {  get; private set; }

        public LearnBase learn { get; private set; }

        public void SetSelect()
        {
            this.isSelected = true;
        } 
        
        public void SetDeselect()
        {
            this.isSelected = false;
        }

        public void SetLearn(LearnBase newLearn)
        {
            learn = newLearn;

            if(preview.TryGetComponent(out Image image))
            {
                image.sprite = newLearn.GetComponent<SpriteRenderer>()?.sprite;
            }

        }

        private void Update()
        {
            if(TryGetComponent(out Image image))
            {
                if (isSelected)
                    image.color = Color.blue;                
                else
                    image.color = Color.white;
            }
        }

    }
}
