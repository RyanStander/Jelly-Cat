using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SliderTextDisplay : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private TextMeshProUGUI text;

        private void FixedUpdate()
        {
            UpdateSliderText();
        }

        private void UpdateSliderText() => text.text = (int)(slider.value * 100) + "%";
    }
}