using UnityEngine;

public class GroundTile : MonoBehaviour
{
    public float tileWidth = 30.09375f;
    public SpikeGenerator generator;

    private void Update()
    {
        transform.Translate(Vector2.left * generator.currentSpeed * Time.deltaTime);

        if (transform.position.x < -tileWidth)
        {
            transform.position += new Vector3(tileWidth * 2f, 0f, 0f);
        }
    }
}
