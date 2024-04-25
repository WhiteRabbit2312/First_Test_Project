using System;

namespace TraineeGame
{
    public static class GameManager 
    {
        //TODO: make a static class and add unsubscription 
        public static event Action onPreGame;
        public static event Action onGameplay;
        public static event Action onEndGame;
        public static void PreGame()
        {
            onPreGame?.Invoke();
        }

        public static void Gameplay()
        {
            onGameplay?.Invoke();
        }

        public static void GameOver()
        {
            onEndGame?.Invoke();
        }
    }
}
