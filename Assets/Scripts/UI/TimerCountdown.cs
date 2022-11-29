using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class TimerCountdown : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private float duration = 300;


        private void FixedUpdate()
        {
            DisplayTime();
        }

        private void DisplayTime()
        {
            duration -= Time.deltaTime;

            var minutes = Mathf.FloorToInt(duration / 60);
            var seconds = Mathf.FloorToInt(duration % 60);
            text.text = minutes + ":" + seconds;
        }
    }
}