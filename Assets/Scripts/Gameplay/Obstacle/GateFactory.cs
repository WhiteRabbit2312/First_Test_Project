using UnityEngine;

namespace TraineeGame
{
    public class GateFactory : IObtacleFactory // GateFactory
    {
        private ObstacleMovement _prefab;

        public GateFactory(ObstacleMovement prefab)
        {
            _prefab = prefab;
        }

        public ObstacleMovement CreateObstacle() //CreateObstacle
        {
            ObstacleMovement gate = GameObject.Instantiate(_prefab);
            return gate;
        }
    }
}
