using UnityEngine;

public class TimeTickSystem : MonoBehaviour
{
    private const float TICK_TIMER_MAX = 1f;

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
            Debug.Log("Tick " + tick);
        }
    }
} 
