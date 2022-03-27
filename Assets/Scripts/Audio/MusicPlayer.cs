/*
* Copyright (c) Kp4ws
*
*/

using UnityEngine;
using BDM.Stats;

namespace BDM.Audio
{
	public class MusicPlayer : MonoBehaviour
	{
		private static AudioSource audioSource;

		private void Awake()
		{
			audioSource = GetComponent<AudioSource>();
			audioSource.volume = PlayerPrefsController.GetMasterVolume();
		}

		public static void PauseMusic()
		{
			audioSource.Pause();
		}

		public static void ResumeMusic()
		{
			audioSource.UnPause();
		}
	}
}