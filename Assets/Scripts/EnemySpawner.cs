using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    #region Exposed
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private EnemyInfo[] _enemyWave;
    [SerializeField] private Transform _spawnPoint;
    
    #endregion

    #region Singleton
    private static EnemySpawner _instance;
    public static EnemySpawner Instance {
        get {
            return _instance;
        }
    }
    #endregion

    #region Events
    public event Action OnVictory;
    #endregion

    #region Unity Lifecycle

    void Awake()
    {
        Time.timeScale = 1;
        if (_instance == null) _instance = this;
        else Destroy(gameObject);
        _enemyCollider = _enemyPrefab.GetComponent<BoxCollider2D>();
    }

    void Start()
    {
    }

    void Update()
    {
        if (_spawnIndex < _enemyWave.Length)
        {
            float raycastLength = _enemyWave[_spawnIndex]._enemySize == 0 ? _enemyCollider.size.x : _enemyWave[_spawnIndex]._enemySize;
            raycastLength = raycastLength * 0.5f + .1f;

            RaycastHit2D hit = Physics2D.Raycast(_spawnPoint.position, Vector2.left, raycastLength);

            Debug.DrawRay(_spawnPoint.position, Vector2.left * raycastLength, Color.red);

            if (hit.collider == null)
            {
                SpawnEnemy();
            }
        } else if(_spawnPoint.childCount == 0) {
            OnVictory.Invoke();
            Time.timeScale = 0;
        }
    }

    void FixedUpdate()
    {

    }


    private void SpawnEnemy()
    {
        GameObject enemy = Instantiate(_enemyPrefab, _spawnPoint);
        enemy.GetComponent<EnemyController>().SetParameters(_enemyWave[_spawnIndex]);
        _spawnIndex++;
    }
    #endregion

    #region Main methods

    #endregion

    #region Private & Protected
    private int _spawnIndex;
    private BoxCollider2D _enemyCollider;
    #endregion
}
