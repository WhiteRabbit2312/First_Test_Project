using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace TraineeGame
{
    public class GameOverPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _mainPanel;//TODO
        [SerializeField] private GameObject _gameOverPanel;//TODO

        [SerializeField] private Button returnToMenuButton;
        [SerializeField] private Button rewardButton;

        public static event Action OnRespawnClicked;

        private void Awake()
        {
            returnToMenuButton.onClick.AddListener(ReturnToMenuButton);
            rewardButton.onClick.AddListener(RespawnButton);
            GameManager.onEndGame += EnablegameOverPanel;
        }

        public void ReturnToMenuButton()
        {
            _gameOverPanel.SetActive(false);
            _mainPanel.SetActive(true);
            rewardButton.gameObject.SetActive(true);
            GameManager.PreGame();

        }

        private void EnablegameOverPanel()
        {
            _gameOverPanel.SetActive(true);
        }

        public void RespawnButton()
        {
            OnRespawnClicked?.Invoke();
            rewardButton.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            GameManager.onEndGame -= EnablegameOverPanel;
        }
    }
}
