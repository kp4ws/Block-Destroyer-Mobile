using UnityEngine;
using TMPro;
using BDM.EventManagement;
using System;
using BDM.Stats;

namespace BDM.UI
{
    public class ScoreDisplay : MonoBehaviour
    {
        private int score;
        private TextMeshProUGUI scoreText;
        private EventBus bus;

        private void Awake()
        {
            bus = EventBus.Instance;
        }

        private void OnEnable()
        {
            bus.Subscribe(EventChannel.BlockDestroyed, UpdateScore);
        }

        private void OnDisable()
        {
            bus.Unsubscribe(EventChannel.BlockDestroyed, UpdateScore);
        }

        private void Start()
        {
            scoreText = GetComponent<TextMeshProUGUI>();
            UpdateScoreGUI();
        }

        private void UpdateScore(object e)
        {
            if (e == null)
                return;

            EventObject<int> _event = e as EventObject<int>;
            if (_event == null)
                return;

            score += _event.value;
            UpdateScoreGUI();

            if (score > PlayerPrefsController.GetHighScore())
            {
                PlayerPrefsController.SetHighScore(score);
            }
        }

        private void UpdateScoreGUI()
        {
            scoreText.text = String.Format("Score: {0}", score);
        }

        public int GetScore()
        {
            return score;
        }
    }
}