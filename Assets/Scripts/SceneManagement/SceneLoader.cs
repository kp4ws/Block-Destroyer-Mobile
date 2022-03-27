/*
* Copyright (c) Kp4ws
*
*/

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BDM.SceneManagement
{
	public class SceneLoader : MonoBehaviour
	{
		private Coroutine sceneRoutine;

		public void LoadScene(int sceneIndex)
		{
			InitiateLoad(sceneIndex);
		}

		private void InitiateLoad(int sceneIndex)
		{
			if (sceneRoutine != null)
			{
				StopCoroutine(sceneRoutine);
				sceneRoutine = null;
			}

			sceneRoutine = StartCoroutine(PerformLoad(sceneIndex));
		}

		private IEnumerator PerformLoad(int sceneIndex)
		{
			yield return SceneManager.LoadSceneAsync(sceneIndex);
		}

        public void QuitGame()
        {
            Application.Quit();
        }

        ////////////
        //OLD CODE//
        ///////////
        //private const string START_SCENE = "Start Menu";
        //private const string GAME_SCENE = "Game Scene";
        //private const string CREDITS_SCENE = "Credits Scene";

        //public void LoadMenuScene()
        //{
        //	SceneManager.LoadScene(START_SCENE);
        //}

        //public void LoadGameScene()
        //{
        //	SceneManager.LoadScene(GAME_SCENE);
        //}

        //public void LoadCredits()
        //{
        //	SceneManager.LoadScene(CREDITS_SCENE);
        //}
    }

}