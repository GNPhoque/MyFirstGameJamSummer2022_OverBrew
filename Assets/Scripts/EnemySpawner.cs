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

    #region Unity Lifecycle

    void Awake()
    {
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

            //Debug.Log(hit.collider.gameObject.name);

            if (hit.collider == null)
            {
                SpawnEnemy();
            }
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
