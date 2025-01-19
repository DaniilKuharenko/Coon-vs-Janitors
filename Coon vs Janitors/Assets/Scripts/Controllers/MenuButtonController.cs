using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Raccons_House_Games
{
    public class MenuButtonController : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _shopButton;
        
        private void Awake()
        {
            _playButton.onClick.AddListener(OnPlayButtonClicked);
            _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
            _shopButton.onClick.AddListener(OnShopButtonClicked);
        }

        private void OnPlayButtonClicked()
        {
            Debug.Log("Play button was clicked!");
            SceneManager.LoadScene("DefaultGame");
        }

        private void OnSettingsButtonClicked()
        {
            Debug.Log("Settings button was clicked!");
        }

        private void OnShopButtonClicked()
        {
            Debug.Log("Shop button was clicked!");
        }
    }
}
