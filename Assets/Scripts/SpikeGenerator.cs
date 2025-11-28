using UnityEngine;

public class SpikeGenerator : MonoBehaviour
{
    public GameObject spike;

    [Header("Speed settings")]

    public float MinSpeed = 5f;
    public float MaxSpeed = 18f;
    public float currentSpeed;
    public float SpeedMultiplier = 0.15f;

    [Header("Spacing")]
    public float MinGap = 0.8f;
    public float MaxGap = 2.2f;

    private float nextSpawnTime;

    void Awake()
    {
        currentSpeed = MinSpeed;
        ScheduleNextSpike();
    }

    void Update()
    {
        if (currentSpeed < MaxSpeed)
            currentSpeed += SpeedMultiplier * Time.deltaTime;

        if (Time.time >= nextSpawnTime)
        {
            SpawnSpike();
            ScheduleNextSpike();
        }
    }

    private void ScheduleNextSpike()
    {
        nextSpawnTime = Time.time + Random.Range(MinGap, MaxGap);
    }

    private void SpawnSpike()
    {
        GameObject SpikeIns = Instantiate(spike, transform.position, transform.rotation);
        SpikeIns.GetComponent<SpikeScript>().spikeGenerator = this;
    }
}
