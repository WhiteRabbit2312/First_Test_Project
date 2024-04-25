using UnityEngine;

namespace TraineeGame
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject _mainPanel;
        [SerializeField] private GameObject _gameOverPanel;

        private void Awake()
        {
            GameManager.onGameplay += CloseMenuTab;
            GameManager.onEndGame += EnablegameOverPanel;
        }

        public void StartButton()
        {
            GameManager.Gameplay();
        }

        private void CloseMenuTab()
        {
            _mainPanel.SetActive(false);
        }

        private void EnablegameOverPanel()
        {
            _gameOverPanel.SetActive(true);
        }

        public void ExitButton()
        {
            Application.Quit();
        }

        public void RespawnGameButton()
        {
            GameManager.Gameplay();
        }

        public void ReturnToMenuButton()
        {
            GameManager.PreGame();
            _gameOverPanel.SetActive(false);
            _mainPanel.SetActive(true);
        }

        private void OnDestroy()
        {
            GameManager.onGameplay -= CloseMenuTab;
            GameManager.onEndGame -= EnablegameOverPanel;
        }
    }
}