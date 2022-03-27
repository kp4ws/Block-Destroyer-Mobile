/*
* Copyright (c) Kp4ws
*
*/

using UnityEngine;
using TMPro;
using BDM.EventManagement;

namespace BDM.UI
{
	public class NextWave : MonoBehaviour
	{
		[SerializeField] private GameObject countdownCanvas;
		[SerializeField] private TextMeshProUGUI countdownText;
		
		private bool readyForCount;
		public float countdownTimer;

		private EventBus bus;

        private void Awake()
        {
			bus = EventBus.Instance;
			countdownTimer = 3;
		}

        private void OnEnable()
		{
			bus.Subscribe(EventChannel.Countdown, PrepareCountdown);
		}

		private void OnDisable()
		{
			bus.Unsubscribe(EventChannel.Countdown, PrepareCountdown);
		}

		private void PrepareCountdown(object e)
        {
			readyForCount = true;
			countdownCanvas.SetActive(true);
			countdownTimer = 3;
		}

		private void Update()
        {
			if (!readyForCount)
				return;

			countdownTimer -= Time.deltaTime;
			countdownText.text = Mathf.Round(countdownTimer).ToString();

			if(countdownTimer < 1)
            {
				countdownCanvas.SetActive(false);
				readyForCount = false;
				bus.Publish(EventChannel.NewWave, this, "Ready for new wave");
			}
		}
	}
} 