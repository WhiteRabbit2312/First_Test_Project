using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TraineeGame
{
    public class SpawnObjects : MonoBehaviour, ISpeedPlayer
    {
        [SerializeField] private ObstacleMovement _prefabStone;
        [SerializeField] private ObstacleMovement _prefabGate;
        private const int PrefabCount = 20; 
        private const int MaxObstacleCount = 100; 
        private const int MaxSpawnCount = 3;

        private const float _leftSpawnPos = -1.4f;
        private const float _centerSpawnPos = 0f;
        private const float _rightSpawnPos = 1.4f;

        private List<ObstacleMovement> _pool = new List<ObstacleMovement>();
        
        private IObtacleFactory _stoneObstacle;
        private IObtacleFactory _gateObstacle;

        private int _score = 0;
        private float _speed = 15f;
        private bool _isGameplay = true;

        private Vector3 _spawnLeft;
        private Vector3 _spawnCenter;
        private Vector3 _spawnRight;


        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        private void Awake()
        {
            _stoneObstacle = new StoneFactory(_prefabStone);
            _gateObstacle = new GateFactory(_prefabGate);

            GameManager.onGameplay += CanPlay;
            GameManager.onEndGame += StopPlay;
            GameManager.onPreGame += ReturnToMenu;

            _spawnLeft = new Vector3(_leftSpawnPos, transform.position.y, transform.position.z);
            _spawnCenter = new Vector3(_centerSpawnPos, transform.position.y, transform.position.z);
            _spawnRight = new Vector3(_rightSpawnPos, transform.position.y, transform.position.z);
        }

        private void Start()
        {
            for (int i = 0; i < PrefabCount; ++i)
            {
                int obstacleType = UnityEngine.Random.Range(0, MaxObstacleCount);
                var _obstacle = GetObstacleType(obstacleType);
               
                _obstacle.gameObject.SetActive(false);
                _pool.Add(_obstacle);

            }
        }

        private ObstacleMovement GetObstacleType(int type)
        {
            switch (type % 2)
            {
                case 0:
                    var obst1 = _stoneObstacle.CreateObstacle();
                    obst1.ApplySpeed(this);
                    return obst1;

                case 1: 
                    var obst2 = _gateObstacle.CreateObstacle();
                    obst2.ApplySpeed(this);
                    return obst2;

                default: return null;
            }
        }

        void OnDestroy()
        {
            GameManager.onGameplay -= CanPlay;
            GameManager.onEndGame -= StopPlay;
            GameManager.onPreGame -= ReturnToMenu;
        }

        private void CanPlay() 
        {
            _isGameplay = true;
            StartCoroutine(SpawnObstacle()); 
        }


        private void StopPlay()
        {
            _isGameplay = false;
           
        }
            
        private void ReturnToMenu()
        {
            DisableObstacles();
        }

        List<Vector3> existingSpawnPoints = new List<Vector3>();
        private IEnumerator SpawnObstacle()
        {
            while (_isGameplay)
            {
                int obstacleCountInRow = Random.Range(1, 4);
                
                for (int i = 0; i < obstacleCountInRow; ++i)
                {
                    ObstacleMovement obstacle = GetObstacleFromPool();
                    if (obstacle != null)
                    {
                        obstacle.gameObject.SetActive(true);
                        do
                        {
                            obstacle.gameObject.transform.position = SpawnPoint();
                        } while (existingSpawnPoints.Contains(obstacle.gameObject.transform.position));
                        

                        existingSpawnPoints.Add(obstacle.gameObject.transform.position);
                        _score++;

                        if (_score % 20 == 0)
                        {
                            Speed += 0.3f;
                        }
                    }
                }
                existingSpawnPoints.Clear();
                yield return new WaitForSeconds(1.7f);
            }

        }

        private Vector3 SpawnPoint()
        {
            Vector3 obstaclePosition = default;

            int spawner = UnityEngine.Random.Range(0, MaxSpawnCount);
            switch (spawner)
            {
                case 0: obstaclePosition = _spawnLeft; break;
                case 1: obstaclePosition = _spawnCenter; break;
                case 2: obstaclePosition = _spawnRight; break;
            }

            return obstaclePosition;
        }

        private void DisableObstacles()
        {
            foreach(var item in _pool)
            {
                item.gameObject.SetActive(false);
            }
        }

        private ObstacleMovement GetObstacleFromPool()
        {
            for (int i = 0; i < _pool.Count; ++i)
            {
                if (!_pool[i].gameObject.activeInHierarchy)
                {
                    return _pool[i];
                }
            }

            return null;
        }
       
        
    }
}
