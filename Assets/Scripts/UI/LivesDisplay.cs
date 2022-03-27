using UnityEngine;
using BDM.EventManagement;
using BDM.Stats;
using TMPro;
using System;

namespace BDM.UI
{
    public class LivesDisplay : MonoBehaviour
    {
		[SerializeField] private int baseLives = 3;

		private int lives;
		
		private EventBus bus;
		private TextMeshProUGUI livesText;


		private void Awake()
		{
			bus = EventBus.Instance;
		}

        private void Start()
        {
			lives = baseLives - PlayerPrefsController.GetDifficulty();
			livesText = GetComponent<TextMeshProUGUI>();
			UpdateLivesGUI();
		}

		private void OnEnable()
		{
			bus.Subscribe(EventChannel.TakeLife, UpdateLives);
		}

		private void OnDisable()
		{
			bus.Unsubscribe(EventChannel.TakeLife, UpdateLives);
		}

		private void UpdateLives(object e)
		{
			lives--;

			if(lives < 0)
            {
				bus.Publish(EventChannel.GameOver, this, "Game over");
				return;
            }

			UpdateLivesGUI();
		}

		private void UpdateLivesGUI()
        {
			livesText.text = String.Format("x{0}", lives);
		}
	}
}
