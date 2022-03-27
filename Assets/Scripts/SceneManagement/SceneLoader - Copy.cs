/*
* Copyright (c) Kp4ws
*
*/

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BDM.SceneManagement
{
	public class SceneLoader2 : MonoBehaviour
	{
		private Coroutine sceneRoutine;

		public void LoadScene(int sceneIndex)
		{
			InitiateLoad(sceneIndex);
		}
		
		private void InitiateLoad(int sceneIndex)
        {
			if(sceneRoutine != null)
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
	}

}