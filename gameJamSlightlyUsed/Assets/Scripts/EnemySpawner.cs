using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField]
    private GameObject _enemyPrefab;

	void Start () {

        StartCoroutine(SpawnEnemyLoop());

	}

    private IEnumerator SpawnEnemyLoop()
    {
        SpawnEnemy();
        yield return new WaitForSeconds(1);
        yield return SpawnEnemyLoop();
    }

    private void SpawnEnemy()
    {
        var enemy = Instantiate(_enemyPrefab);
        enemy.transform.position = transform.position;
    }
}
