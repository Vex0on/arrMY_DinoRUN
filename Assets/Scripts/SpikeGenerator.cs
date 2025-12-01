using UnityEngine;

public class SpikeGenerator : MonoBehaviour
{
    public GameObject bottomSpike;
    public GameObject topSpike;

    [Header("Top spike settings")]
    public float topSpikeY = -3f;
    public float bottomSpikeY = -4f;
    public float chanceForTopSpawn = 0.3f;

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
        bool spawnTop = Random.value < chanceForTopSpawn;

        GameObject prefab = spawnTop ? topSpike : bottomSpike;
        
        Vector3 pos = transform.position;
        pos.y = spawnTop ? topSpikeY : bottomSpikeY;

        GameObject SpikeIns = Instantiate(prefab, pos, Quaternion.identity);
        SpikeIns.GetComponent<SpikeScript>().spikeGenerator = this;
    }
}
