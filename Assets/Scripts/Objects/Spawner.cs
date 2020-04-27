﻿using System.Collections;
using UnityEngine;


namespace Objects
{
    public class Spawner : MonoBehaviour
    {
        public Transform[] SpawnPoints;
        public GameObject[] Prefabs;
        public GameObject BossPrefab;

        public float Delay = 1;
        public float SpawnTime = 5f;

        private int _wavesCount = 1;
        private int _enemysCount = 2;
        private float _spawnTimer;
        private Coroutine _spawnRoutine;

        private void OnEnable()
        {
            _spawnTimer = SpawnTime;
        }

        private void Update()
        {
            _spawnTimer -= Time.deltaTime;
            if (_spawnTimer <= 0)
            {
                _spawnRoutine = StartCoroutine(SpawnRoutine(_enemysCount));
                _wavesCount += 1;
                _enemysCount += 1;
                SpawnTime += 10;
                _spawnTimer = SpawnTime;
            }
        }

        private void OnDisable()
        {
            if (_spawnRoutine != null)
            {
                StopCoroutine(_spawnRoutine);
                _spawnRoutine = null;
            }
        }

        private IEnumerator SpawnRoutine(int enemyCount)
        {
            for (int i = 0; i < enemyCount; i++)
            {
                int randomPoint = new System.Random().Next(0, SpawnPoints.Length);
                int randomPrefab = new System.Random().Next(0, Prefabs.Length);
                Instantiate(Prefabs[randomPrefab], SpawnPoints[randomPoint].position, transform.rotation);
                if (_wavesCount % 10 == 0)
                {
                    Instantiate(BossPrefab, SpawnPoints[randomPoint].position, transform.rotation);
                    yield return new WaitForSeconds(Delay);
                }
                yield return new WaitForSeconds(Delay);
            }
        }
    }
}

