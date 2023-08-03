using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class ColorUtils : MonoBehaviour
    {
        public static Color HexToColor(string hex)
        {
            Color color = Color.white;

            if (ColorUtility.TryParseHtmlString(hex, out color))
            {
                return color;
            }
            return Color.white;
        }
    }
}
