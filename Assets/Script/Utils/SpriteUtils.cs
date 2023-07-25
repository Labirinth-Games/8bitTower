using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class SpriteUtils : MonoBehaviour
    {
        private float _lastPositionFace = 1;

        public void Flip(float dirX, Vector3 scale)
        {
            if (dirX == _lastPositionFace)
                return;

            scale.x *= Mathf.Sign(dirX);

            transform.localScale = scale;
            _lastPositionFace = dirX;
        }
    }
}
