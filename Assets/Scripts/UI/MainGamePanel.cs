using UnityEngine;
using UnityEngine.UI;
using System;

namespace TraineeGame
{
    public class MainGamePanel : MonoBehaviour
    {
        [SerializeField] private GameObject _mainPanel;
        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private GameObject _leaderboard;
        [SerializeField] private Button startButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private Button leaderbordButton;

        public static Action OnLeaderbordOpen;


        private void Awake()
        {
            GameManager.onGameplay += CloseMenuTab;
            startButton.onClick.AddListener(StartGame);
            exitButton.onClick.AddListener(ExitButton);
            leaderbordButton.onClick.AddListener(LeaderboardButton);
        }

        public void StartGame()
        {
            GameManager.Gameplay();
        }

        public void LeaderboardButton()
        {
            OnLeaderbordOpen?.Invoke();
            _leaderboard.SetActive(true);
        }

        public void CloseLeaderBoard()
        {
            _leaderboard.SetActive(false);
        }

        private void CloseMenuTab()
        {
            _mainPanel.SetActive(false);
        }

        public void ExitButton()
        {
            Application.Quit();
        }

        private void OnDestroy()
        {
            GameManager.onGameplay -= CloseMenuTab;
        }
    }
}
