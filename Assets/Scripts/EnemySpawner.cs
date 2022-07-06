using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    #region Exposed
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private EnemyInfo[] _enemyWave;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _spawnDelay;
    #endregion

    #region Unity Lifecycle

    void Awake()
    {
        
    }

    void Start()
    {
        _lastSpawnTime = Time.time;
    }

    void Update()
    {
        if (Time.time - _lastSpawnTime >= _spawnDelay && _spawnIndex < _enemyWave.Length) {
            GameObject enemy = Instantiate(_enemyPrefab, _spawnPoint);
            enemy.GetComponent<EnemyController>().SetParameters(_enemyWave[_spawnIndex]);
            _spawnIndex++;
            _lastSpawnTime = Time.time;
        }
    }

    void FixedUpdate()
    {
        
    }

    #endregion

    #region Main methods
    
    #endregion

    #region Private & Protected
    private float _lastSpawnTime;
    private int _spawnIndex;
    #endregion
}
