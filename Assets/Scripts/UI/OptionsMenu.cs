/*
* Copyright (c) Kp4ws
*
*/

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BDM.Stats;

namespace BDM.UI
{
	public class OptionsMenu : MonoBehaviour, IMenu
	{
		[SerializeField] private string[] difficulties;
		[SerializeField] private Slider volumeSlider;
		[SerializeField] private TextMeshProUGUI difficultyText;
		[SerializeField] private GameObject optionsMenu;

		private int selectedDifficulty;

		private void Start()
		{
			volumeSlider.value = PlayerPrefsController.GetMasterVolume();
			selectedDifficulty = PlayerPrefsController.GetDifficulty();
			UpdateDifficultyGUI();
		}

		public void difficultyRight()
		{
			selectedDifficulty = selectedDifficulty < difficulties.Length - 1 ? ++selectedDifficulty : 0;
			UpdateDifficultyGUI();
		}

		public void difficultyLeft()
		{
			selectedDifficulty = selectedDifficulty > 0 ? --selectedDifficulty : difficulties.Length - 1;
			UpdateDifficultyGUI();
		}

		private void UpdateDifficultyGUI()
		{
			difficultyText.text = difficulties[selectedDifficulty];
		}

		private void SaveAndExit()
		{
			PlayerPrefsController.SetMasterVolume(volumeSlider.value);
			PlayerPrefsController.SetDifficulty(selectedDifficulty);
		}

        public void OpenMenu()
        {
			optionsMenu.SetActive(true);
        }

        public void CloseMenu() //Assuming options menu is only accessed from main menu. Otherwise need to add in additional check to return to game
        {
			SaveAndExit();
			optionsMenu.SetActive(false);
        }
    }

}