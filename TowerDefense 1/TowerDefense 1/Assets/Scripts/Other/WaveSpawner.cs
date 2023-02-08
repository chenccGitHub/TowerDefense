using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class WaveSpawner : MonoBehaviour
{
    public float timeBetweenWawe = 2;
    private GameObject enemyPrefab;
    public int spawnerNum = 0;
    void Start()
    {
        enemyPrefab = Resources.Load<GameObject>("Prefab/Enemy");
        new Task(ISpawnerSquene());
    }
    private IEnumerator ISpawnerSquene()
    {
        while (true)
        {
            spawnerNum++;
            for (int i = 0; i < spawnerNum; i++)
            {
                GameObject.Instantiate(enemyPrefab);
                yield return new WaitForSeconds(1);
            }
            yield return new WaitForSeconds(timeBetweenWawe);
        }
    }
}
