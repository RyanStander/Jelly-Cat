using System;
using Events;
using TMPro;
using UnityEngine;
using EventType = Events.EventType;

namespace UI
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        private float duration;

        private void FixedUpdate()
        {
            DisplayTime();
        }

        //Updates and shows the time remaining
        private void DisplayTime()
        {
            duration += Time.deltaTime;

            var minutes = Mathf.FloorToInt(duration / 60).ToString();
            var seconds = Mathf.FloorToInt(duration % 60).ToString();

            //Make sure the seconds are displayed properly as 2 digits, it will always be 1 or 2 digits so no further checks required
            if (seconds.Length == 1)
                seconds = "0" + seconds;

            //Make sure the minutes are displayed properly as 2 digits, it will always be 1 or 2 digits so no further checks required
            if (minutes.Length == 1)
                minutes = "0" + minutes;

            text.text = minutes + ":" + seconds;
        }
    }
}