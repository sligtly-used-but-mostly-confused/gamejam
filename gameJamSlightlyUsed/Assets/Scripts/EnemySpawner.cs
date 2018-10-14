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
    [SerializeField]
    private float _enemySwarmSpawnRate = .25f;
    [SerializeField]
    private float _timeBetweenSwarmModes = 20f;
    [SerializeField]
    private float _swarmModeDuration = 5f;


    public float chanceToSpawnBraker = .2f;

    public static bool InSwarmMode = false;
  
    void Awake () {
        StartCoroutine(SwarmModeLoop());
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemyLoop());
    }

    private IEnumerator SwarmModeLoop()
    {
        yield return new WaitForSeconds(_timeBetweenSwarmModes);
        InSwarmMode = true;
        AlarmSound.hordeOn = true;
        AlarmSound.hordeOn = true;
        yield return new WaitForSeconds(_swarmModeDuration);
        InSwarmMode = false;
        AlarmSound.hordeOn = false;
        AlarmSound.hordeOn = false;
        yield return SwarmModeLoop();
    }

    private IEnumerator SpawnEnemyLoop()
    {
        SpawnEnemy();
        if(InSwarmMode)
        {
            yield return new WaitForSeconds(_enemySwarmSpawnRate);
        }
        else
        {
            yield return new WaitForSeconds(_enemySpawnRate);
        }
        
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
