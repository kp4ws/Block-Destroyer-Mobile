using UnityEngine;
using BDM.EventManagement;
using BDM.SceneManagement;

namespace BDM.UI
{
    public class PauseMenu : MonoBehaviour, IMenu
    {
        [SerializeField] private GameObject pauseMenu;
        EventBus bus;

        private bool isPaused = false;

        private void Awake()
        {
            bus = EventBus.Instance;
        }

        public void CloseMenu()
        {
            Time.timeScale = 1;
            isPaused = !isPaused;
            pauseMenu.SetActive(false);
            bus.Publish(EventChannel.PauseToggle, this, isPaused); //Create different event for this?
        }

        public void OpenMenu()
        {
            Time.timeScale = 0;
            isPaused = !isPaused;
            pauseMenu.SetActive(true);
            bus.Publish(EventChannel.PauseToggle, this, isPaused); //Create different event for this?
        }

        //TODO in future iteration, figure out a better way to do this
        public void QuitToMenu()
        {
            Time.timeScale = 1;
            FindObjectOfType<SceneLoader>().LoadScene(0);
        }
    }

}