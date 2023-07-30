using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class SpriteUtils : MonoBehaviour
    {
        public static void Flip(float dirX, Transform transform)
        {
            Vector3 scale = transform.localScale;
            float signScaleCurrent = Mathf.Sign(scale.x);

            if (dirX == signScaleCurrent)
                return;

            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}
