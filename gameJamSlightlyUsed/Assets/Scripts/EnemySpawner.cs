using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _platformBrakingEnemyPrefab;
    [SerializeField]
    private float _enemySpawnRate = 1;

    public float chanceToSpawnBraker = .2f;

    void Start () {

        StartCoroutine(SpawnEnemyLoop());

	}

    private IEnumerator SpawnEnemyLoop()
    {
        SpawnEnemy();
        yield return new WaitForSeconds(_enemySpawnRate);
        yield return SpawnEnemyLoop();
    }

    private void SpawnEnemy()
    {
        GameObject enemy = null;
        if(Random.value > chanceToSpawnBraker)
        {
            enemy = Instantiate(_enemyPrefab);
        }
        else
        {
            enemy = Instantiate(_platformBrakingEnemyPrefab);
        }

        enemy.transform.position = transform.position;
    }
}
