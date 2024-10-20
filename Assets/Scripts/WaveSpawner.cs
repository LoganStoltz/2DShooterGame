using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour{

    public int waveCount = 0;

    [SerializeField] public float spawnRate = 1.0f;
    public float timeBetweenWaves = 10.0f;
    public int enemyCount;
    [SerializeField] private GameObject[] enemyPreFabs;
    [SerializeField] private GameObject shootingEnemyClone;
    [SerializeField] bool waveIsDone = true;

    void Update() {
        if (waveIsDone == true) {
            StartCoroutine(waveSpawner());
        }
    }

    IEnumerator waveSpawner() {
        waveIsDone = false;
        if(!GameController.manager.GetPlayerDeathState())
        {
            for (int i = 0; i < enemyCount; i++) {
                if(waveCount % 5 == 0)
                {
                    Instantiate(shootingEnemyClone, new Vector3 (transform.position.x, transform.position.y, 0f), Quaternion.identity);
                    yield return new WaitForSeconds(spawnRate);
                }
                else
                {
                    int rand = Random.Range(0, enemyPreFabs.Length);
                    GameObject enemyClone = enemyPreFabs[rand];
                    Instantiate(enemyClone, new Vector3 (transform.position.x, transform.position.y, 0f), Quaternion.identity); // had to create a vecto to keep the enemies at the correct z level
                    yield return new WaitForSeconds(spawnRate);
                }
            }
        }

        spawnRate -= 0.05f;
        enemyCount += 3;
        waveCount += 1;

        yield return new WaitForSeconds(timeBetweenWaves);

        waveIsDone = true;
    }
}
