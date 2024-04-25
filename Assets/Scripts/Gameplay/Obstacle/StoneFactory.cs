using UnityEngine;

namespace TraineeGame
{
    public class StoneFactory: IObtacleFactory
    {
        private ObstacleMovement _prefab;
        public StoneFactory(ObstacleMovement obstacle)
        {
            _prefab = obstacle;
        }
       
        public ObstacleMovement CreateObstacle()
        {
            ObstacleMovement stone = GameObject.Instantiate(_prefab);
            return stone;
        }
    }
}

