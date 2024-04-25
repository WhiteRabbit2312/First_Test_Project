using UnityEngine;

namespace TraineeGame
{
    public class ObstacleMovement : MonoBehaviour
    {
        private Vector3 _dirVector = new Vector3(0f, 0f, -1.4f);
        private ISpeedPlayer _speed;
        private bool _canMove = true;

        private void Awake()
        {
            GameManager.onEndGame += StopMove;
            GameManager.onGameplay += StartMove;
        }

        public void ApplySpeed(ISpeedPlayer speedPlayer)
        {
            _speed = speedPlayer;
        }

        void Update()
        {
            if(_canMove)
                MoveObstacle();
        }

        private void StopMove() => _canMove = false;
        private void StartMove() => _canMove = true;


        private void MoveObstacle()
        {
            transform.Translate(_dirVector.normalized * _speed.Speed * Time.deltaTime);
        }

        private void OnDestroy()
        {
            GameManager.onEndGame -= StopMove;
            GameManager.onGameplay -= StartMove;
        }
    }
}
