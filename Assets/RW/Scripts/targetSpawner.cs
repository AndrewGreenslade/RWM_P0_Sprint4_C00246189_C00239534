using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetSpawner : MonoBehaviour
{
    public GameObject targetObj;
    public float TargetSpawnRate = 5;
    public bool gameStarted = false;

    float currentTime;

    public int currentTargetAmount = 0;

    public Vector2 minSpawnPos = new Vector2(-8.5f, 5.5f);
    public Vector2 maxSpawnPos = new Vector2(8.5f, 0.4f);

    private void FixedUpdate()
    {
        if (gameStarted)
        {
            currentTime += Time.deltaTime;

            if (currentTime > TargetSpawnRate)
            {
                Debug.Log("SpawningTarget");
                Vector3 randomPos = new Vector3(Random.Range(minSpawnPos.x, maxSpawnPos.x), Random.Range(minSpawnPos.y, maxSpawnPos.y), -0.05f);
                spawnTarget(randomPos);
                restartTimer();
            }
        }
    }

    private void spawnTarget(Vector3 t_pos)
    {
        Instantiate(targetObj, t_pos, Quaternion.Euler(-90,0,0));
    }

    public void restartTimer()
    {
        currentTargetAmount++;
        currentTime = 0;
    }
}