/*
* Copyright (c) Kp4ws
*
*/

using UnityEngine;
namespace BDM.Stats
{
	public static class PlayerPrefsController
	{
		private const string MASTER_VOLUME_KEY = "master volume";
		private const string DIFFICULTY_KEY = "difficulty";
		private const string HIGHSCORE_KEY = "highscore";

		private const float MIN_VOLUME = 0f;
		private const float MAX_VOLUME = 1f;

		public static void SetMasterVolume(float volume)
		{
			if (volume >= MIN_VOLUME && volume <= MAX_VOLUME)
			{
				PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
			}
			else
			{
				Debug.LogError("Master of volume is out of range");
			}
		}

		public static void SetDifficulty(int difficulty)
		{
			PlayerPrefs.SetInt(DIFFICULTY_KEY, difficulty);
		}

		public static void SetHighScore(int newHighscore)
		{
			PlayerPrefs.SetInt(HIGHSCORE_KEY, newHighscore);
		}

		public static float GetMasterVolume()
		{
			return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY, 0.8f);
		}

		public static int GetDifficulty()
		{
			return PlayerPrefs.GetInt(DIFFICULTY_KEY, 1);
		}

		public static int GetHighScore()
		{
			return PlayerPrefs.GetInt(HIGHSCORE_KEY, 0);
		}
	}

}