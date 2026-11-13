using System;
using UnityEngine;

public class TimeTickSystem : MonoBehaviour
{
    private const float TICK_TIMER_MAX = 1f;

    [SerializeField] private Spawner[] spawners;
    //[SerializeField] private int spawnEveryTicks = 5;

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

            foreach (Spawner spawner in spawners)
            {
                if (spawner != null)
                {
                    spawner.CheckSpawn(tick);
                }
            }
            //Debug.Log("Tick " + tick);
        }
    }
} 
