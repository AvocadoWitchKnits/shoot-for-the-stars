using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner: MonoBehaviour
{
    [SerializeField] private GameObject droppedItemPrefab;
    [SerializeField] private Item itemToSpawn;

    [Header("Spawn Timing")]
    [SerializeField] private int minTicks = 5;
    [SerializeField] private int maxTicks = 10;
    [SerializeField] private int startingOffset = 0;

    [Header("Spawn Limits")]
    [SerializeField] private int maximumSpawnCount = 3;

    [Header("Spawn Position")]
    [SerializeField] private float spawnRadius = 2f;

    private int spawnedCount;
    private int nextSpawnTick;

    private void Awake()
    {
        spawnedCount = 0;
        nextSpawnTick =  startingOffset + Random.Range(minTicks, maxTicks + 1);
    }
    public void CheckSpawn(int currentTick)
    {
        if (spawnedCount >= maximumSpawnCount)
            return;

        if (currentTick < nextSpawnTick)
            return;

        SpawnItem();

        nextSpawnTick =
            currentTick +
            Random.Range(
                minTicks,
                maxTicks + 1
            );
    }

    private void SpawnItem()
    {
        Vector2 randomOffset =
            Random.insideUnitCircle * spawnRadius;

        Vector3 spawnPosition =
            transform.position +
            new Vector3(
                randomOffset.x,
                0f,
                randomOffset.y
            );

        GameObject spawnedObject = Instantiate(
            droppedItemPrefab,
            spawnPosition,
            Quaternion.identity
        );

        DroppedItem droppedItem =
            spawnedObject.GetComponent<DroppedItem>();

        if (droppedItem != null)
        {
            droppedItem.item = itemToSpawn;
        }

        spawnedCount++;

        Debug.Log(
            itemToSpawn.name +
            " spawned. Count: " +
            spawnedCount
        );
    }
}





// This method is called by the Input System when look input changes.
