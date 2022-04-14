using UnityEngine;

namespace BDM.UI
{
    public class CreditsMenu : MonoBehaviour, IMenu
    {
        [SerializeField] private GameObject creditsMenu;

        public void CloseMenu()
        {
            creditsMenu.SetActive(false);
        }

        public void OpenMenu()
        {
            creditsMenu.SetActive(true);
        }

        public void OpenURL(string url)
        {
            Application.OpenURL(url);
        }
    }
}

