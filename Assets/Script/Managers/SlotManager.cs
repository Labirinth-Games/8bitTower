using HUD;
using Learn;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Manager
{
    public class SlotManager : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private List<LearnBase> learns = new List<LearnBase>();
        [SerializeField] private GameObject slotPrefab;
        [SerializeField] private GameObject slotZoneSpawn;
        
        private List<SlotHud> _learnInstances = new List<SlotHud>();

        public void Init()
        {
            foreach (var learn in learns)
            {
                var instance = Instantiate(slotPrefab);
                instance.transform.SetParent(slotZoneSpawn.transform);

                var slot = instance.GetComponent<SlotHud>();
                slot.SetLearn(Instantiate(learn));

                _learnInstances.Add(slot);
            }
        }

        public SlotHud GetLearn(int index)
        {
            return _learnInstances[index];
        }

        public int Size()
        {
            return _learnInstances.Count;
        }
    }
}
