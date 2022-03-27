/*
* Copyright (c) Kp4ws
*
*/

using UnityEngine;
using TMPro;
using BDM.EventManagement;
using BDM.SceneManagement;
using BDM.Stats;
using System.Collections;

namespace BDM.UI {
	public class GameoverMenu : MonoBehaviour, IMenu
	{
		[SerializeField] TextMeshProUGUI scoreValue;
		[SerializeField] TextMeshProUGUI highScoreValue;
        [SerializeField] GameObject gameoverMenu;
        private float gameoverDelay = 0.2f;

        private EventBus bus;


        private void Awake()
        {
            bus = EventBus.Instance;
        }

        private void OnEnable()
        {
            bus.Subscribe(EventChannel.GameOver, HandleGameover);
        }

        private void OnDisable()
        {
            bus.Unsubscribe(EventChannel.GameOver, HandleGameover);
        }

        public void CloseMenu()
        {
            gameoverMenu.SetActive(false);
        }

        public void OpenMenu()
        {
            gameoverMenu.SetActive(true);
        }

        public void PlayAgain()
        {
            FindObjectOfType<SceneLoader>().LoadScene(1);
        }

        public void QuitToMenu()
        {
            FindObjectOfType<SceneLoader>().LoadScene(0);
        }

        private void HandleGameover(object e)
        {
            UpdateScoreGUI();
            StartCoroutine(InitiateMenu());
        }

        private IEnumerator InitiateMenu()
        {
            yield return new WaitForSeconds(gameoverDelay);
            OpenMenu();
        }

        //TODO in future iteration, find better way to do this
        private void UpdateScoreGUI()
        {
            scoreValue.text = FindObjectOfType<ScoreDisplay>().GetScore().ToString();
			highScoreValue.text = PlayerPrefsController.GetHighScore().ToString();
        }
	}
}