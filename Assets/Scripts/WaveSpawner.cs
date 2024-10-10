using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour{
    int waveCount = 1;

    [SerializeField] public float spawnRate = 1.0f;
    public float timeBetweenWaves = 3.0f;

    public int enemyCount;

    [SerializeField] private GameObject[] enemyPreFabs;

    [SerializeField] bool waveIsDone = true;
    
    void Update() {
        if (waveIsDone == true) {
            StartCoroutine(waveSpawner());
        }
    }

    IEnumerator waveSpawner() {
        waveIsDone = false;

        for (int i = 0; i < enemyCount; i++) {
            int rand = Random.Range(0, enemyPreFabs.Length);
            GameObject enemyClone = enemyPreFabs[rand];
            Instantiate(enemyClone, new Vector3 (transform.position.x, transform.position.y, 0f), Quaternion.identity); // had to create a vecto to keep the enemies at the correct z level
            yield return new WaitForSeconds(spawnRate);
        }

        spawnRate -= 0.1f;
        enemyCount += 3;
        waveCount += 1;

        yield return new WaitForSeconds(timeBetweenWaves);

        waveIsDone = true;
    }
}
