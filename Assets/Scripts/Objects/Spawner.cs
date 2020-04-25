using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] SpawnPoints;
    public GameObject[] Prefabs;
    public float Delay;
    public float SpawnTime = 2f;

    private Coroutine _spawnRoutine;

    private void OnEnable()
    {
        _spawnRoutine = StartCoroutine(SpawnRoutine);
    }

    private void OnDisable()
    {
        if (_spawnRoutine != null)
        {
            StopCoroutine(_spawnRoutine);
            _spawnRoutine = null;
        }
    }

    private IEnumerator SpawnRoutine
    {
        get
        {
            yield return new WaitForSeconds(Delay);
            while (true)
            {
                int randomPoint = new System.Random().Next(0, SpawnPoints.Length);
                int randomPrefab = new System.Random().Next(0, Prefabs.Length);
                Instantiate(Prefabs[randomPrefab], SpawnPoints[randomPoint].position, transform.rotation);
                yield return new WaitForSeconds(SpawnTime);
            }
        }
    }
}
