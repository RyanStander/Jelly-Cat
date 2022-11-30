using System;
using Events;
using TMPro;
using UnityEngine;
using EventType = Events.EventType;

namespace UI
{
    public class TimerCountdown : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        private float duration;
        private bool startedTimer;

        private void FixedUpdate()
        {
            DisplayTime();
        }

        //Updates and shows the time remaining
        private void DisplayTime()
        {
            duration -= Time.deltaTime;

            var minutes = Mathf.FloorToInt(duration / 60);
            var seconds = Mathf.FloorToInt(duration % 60);
            text.text = minutes + ":" + seconds;

            CheckIfTimeExpired();
        }

        private void CheckIfTimeExpired()
        {
            if (duration<=0&&startedTimer)
                EventManager.currentManager.AddEvent(new TimerExpired());
        }

        #region OnEvents

        private void OnEnable()
        {
            EventManager.currentManager.Subscribe(EventType.SetTime,OnSetTime);
        }

        private void OnDisable()
        {
            EventManager.currentManager.Unsubscribe(EventType.SetTime,OnSetTime);
        }

        private void OnSetTime(EventData eventData)
        {
            if (eventData is SetTime setTime)
            {
                duration = setTime.Time;
                startedTimer = true;
            }
            else
            {
                Debug.LogWarning("EventType SetTime does not match eventData of type SetTime");
            }
        }

        #endregion
        

    }
}