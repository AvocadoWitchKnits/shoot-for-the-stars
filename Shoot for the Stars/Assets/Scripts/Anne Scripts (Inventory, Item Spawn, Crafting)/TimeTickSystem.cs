using System;
using UnityEngine;

public class TimeTickSystem : MonoBehaviour
{
    private const float TICK_TIMER_MAX = 1f;

    [SerializeField] private Spawner spawner;
    [SerializeField] private int spawnEveryTicks = 5;

    private int tick;
    private float tickTimer;

    private void Awake()
    {
        tick = 0;
    }

    private void Update()
    {
        tickTimer += Time.deltaTime;
        if (tickTimer >= TICK_TIMER_MAX)
        {
            tickTimer -= TICK_TIMER_MAX;
            tick++;

            if (tick % spawnEveryTicks == 0)
            {
                spawner.SpawnItem();
            }
            //Debug.Log("Tick " + tick);
        }
    }
} 
