using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] _powerupPrefabs;

    [SerializeField]
    private float _enemySpawnDelay = 5.0f;
    [SerializeField]
    private float _powerupSpawnDelay = 5.0f;

    private bool _stopSpawning = false;
    private UI_Manager _uiManager;


    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Create a coroutine of type IEnumerator -- Yield Events
    // while loop
    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        while (_stopSpawning == false)
        {
            float randX = Random.Range(-9.5f, 9.6f);
            Vector3 postoSpawn = new Vector3(randX, 9f, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, postoSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_enemySpawnDelay);
        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        while (_stopSpawning == false)
        {
            float randX = Random.Range(-9.5f, 9.6f);
            Vector3 postoSpawn = new Vector3(randX, 9f, 0);
            Instantiate(_powerupPrefabs[Random.Range(0,3)], postoSpawn, Quaternion.identity);
            yield return new WaitForSeconds(_powerupSpawnDelay);
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }

}
