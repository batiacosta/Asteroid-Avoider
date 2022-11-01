using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] asteroidsPrefabs;
    [SerializeField] private float secondsBetweenAsteroid = 1.5f;
    [SerializeField] private Vector2 forceRange;

    private float _timer = 0;
    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <=0)
        {
            SpawnAsteroid();
            _timer += secondsBetweenAsteroid;
        }
    }

    private void SpawnAsteroid()
    {
        int side = Random.Range(0, 4);
        Vector2 spawnPoint = Vector2.zero;
        Vector2 direction = Vector2.zero;

        switch (side)
        {
            case 0: //  Left side
                spawnPoint.x = 0;
                spawnPoint.y = Random.value;
                direction = new Vector2(1f, Random.Range(-1, 1));
                break;
            case 1: //   Right side
                spawnPoint.x = 1;
                spawnPoint.y = Random.value;
                direction = new Vector2(-1f, Random.Range(-1, 1));
                break;
            case 2: //  Bottom side
                spawnPoint.y = 0;
                spawnPoint.x = Random.value;
                direction = new Vector2(Random.Range(-1, 1), 1f);
                break;
            case 3: //  Top side
                spawnPoint.y = 1;
                spawnPoint.x = Random.value;
                direction = new Vector2(Random.Range(-1, 1), -1f);
                break;
        }

        Vector3 worldSpawnPoint = _mainCamera.ViewportToWorldPoint(spawnPoint);
        worldSpawnPoint.z = 0;

        GameObject asteroidsPrefab = asteroidsPrefabs[Random.Range(0, asteroidsPrefabs.Length)];
        Quaternion asteroidInitialOrientation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
        GameObject asteroidInstance = Instantiate(asteroidsPrefab, worldSpawnPoint, asteroidInitialOrientation);
        
        Rigidbody asteroidRigidBody = asteroidInstance.GetComponent<Rigidbody>();
        asteroidRigidBody.velocity = direction.normalized * Random.Range(forceRange.x, forceRange.y);
    }
}
