using System;
using UnityEngine;
using UnityEngine.UI;

namespace matchPuzzle.component
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField]
        Image bar;

        public void SetProgress(float value)
        {
            value = Math.Max(0, value);
            value = Math.Min(1, value);
            bar.fillAmount = value;
        }
    }
}