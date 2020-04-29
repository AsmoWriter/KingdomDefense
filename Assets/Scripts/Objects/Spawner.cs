using System.Collections;
using UnityEngine;


namespace Objects
{
    public class Spawner : MonoBehaviour
    {
        public Transform[] SpawnPoints;
        public GameObject[] Prefabs;
        public GameObject BossPrefab;

        public float Delay = 1;
        public float BaseSpawnTime = 5f;

        private float _spawnTime;
        private int _wavesCount = 1;
        private int _enemysCount = 2;
        private float _spawnTimer;
        private Coroutine _spawnRoutine;

        private void OnEnable()
        {
            _spawnTime = BaseSpawnTime;
            _spawnTimer = _spawnTime;
        }

        private void Update()
        {
            _spawnTimer -= Time.deltaTime;
            if (_spawnTimer <= 0)
            {
                _spawnRoutine = StartCoroutine(SpawnRoutine(_enemysCount));
                _wavesCount += 1;
                _enemysCount += 1;
                _spawnTime += 5;
                _spawnTimer = _spawnTime;
            }
        }

        private void OnDisable()
        {
            if (_spawnRoutine != null)
            {
                StopCoroutine(_spawnRoutine);
                _spawnRoutine = null;
            }
            _spawnTimer = BaseSpawnTime;
            _wavesCount = 1;
            _enemysCount = 2;
    }

        private IEnumerator SpawnRoutine(int enemyCount)
        {
            for (int i = 0; i < enemyCount; i++)
            {
                int randomPrefab = new System.Random().Next(0, Prefabs.Length);
                yield return new WaitForSeconds(0.1f);
                int randomPoint = new System.Random().Next(0, SpawnPoints.Length);
                Instantiate(Prefabs[randomPrefab], SpawnPoints[randomPoint].position, transform.rotation);
                yield return new WaitForSeconds(Delay);
            }
            if (_wavesCount % 10 == 0)
            {
                int randomPoint = new System.Random().Next(0, SpawnPoints.Length);
                Instantiate(BossPrefab, SpawnPoints[randomPoint].position, transform.rotation);
                yield return new WaitForSeconds(Delay);
            }
        }
    }
}

