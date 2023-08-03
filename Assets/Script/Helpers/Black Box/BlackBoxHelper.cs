using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

namespace Helpers
{
    public class BlackBoxHelper : MonoBehaviour
    {
        private List<BlackBoxCondition> _conditions;
        private GameObject _output;
        private Action _failureCallback;
        private float _difficulty;

        // esse metodo é chamado toda vez que um item de ativaçao é acionado
        // e recebe como parametro o status do item em especifico
        public void ValidateConditions(bool state, BlackBoxInput current)
        {
            if (_conditions.Count > 0)
            {
                bool condition = false;

                for (var i = 0; i < _conditions.Count; i++)
                {
                    if (_conditions[i].input.GetState() != _conditions[i].condition)
                    {
                        condition = false;

                        // when touch on input wrong roll the test to bad way
                        NegativeReinforcementTest();

                        break;
                    }
    
                    condition = true;
                }

                if (condition)
                {
                    _output.GetComponent<IBlackBoxOutput>().Unlock();
                    UnSubscriber();
                }
            }
        }

        private void NegativeReinforcementTest()
        {
            DiceType diceBase = DiceType.D6;

            float playerTest = DiceHelper.Roll(diceBase) + Manager.GameManager.Instance.playerStats.luck;
            float bboxTest = DiceHelper.Roll(diceBase) + _difficulty;

            if(bboxTest > playerTest)
            {
                _failureCallback();
            }
        }

        private void UnSubscriber()
        {
            foreach (var element in _conditions)
            {
                element.input.OnChangeInput.RemoveListener(ValidateConditions);
            }
        }

        public void Load(List<BlackBoxCondition> elements, GameObject output, float difficulty = 1, Action failureCallback = null)
        {
            foreach (var element in elements)
            {
                element.input.OnChangeInput.AddListener(ValidateConditions);
            }

            _conditions = elements;
            _output = output;
            _failureCallback = failureCallback;
            _difficulty = difficulty;
        }
    }
}
