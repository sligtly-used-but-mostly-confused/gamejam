using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private float _enemySpawnRate = 1;

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
        var enemy = Instantiate(_enemyPrefab);
        enemy.transform.position = transform.position;
    }
}
