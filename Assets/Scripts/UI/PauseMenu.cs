using UnityEngine;
using BDM.EventManagement;
using BDM.SceneManagement;

namespace BDM.UI
{
    public class PauseMenu : MonoBehaviour, IMenu
    {
        [SerializeField] private GameObject pauseMenu;
        EventBus bus;

        private void Awake()
        {
            bus = EventBus.Instance;
        }

        public void CloseMenu()
        {
            pauseMenu.SetActive(false);
            bus.Publish(EventChannel.PauseToggle, this, "Game Unpaused"); //Create different event for this?
            Time.timeScale = 1;
        }

        public void OpenMenu()
        {
            pauseMenu.SetActive(true);
            bus.Publish(EventChannel.PauseToggle, this, "Game Paused"); //Create different event for this?
            Time.timeScale = 0;
        }

        //TODO in future iteration, figure out a better way to do this
        public void QuitToMenu()
        {
            Time.timeScale = 1;
            FindObjectOfType<SceneLoader>().LoadScene(0);
        }
    }

}