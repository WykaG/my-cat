using UnityEngine;

public class FloatingItem : MonoBehaviour
{
    public float amplitude = 0.2f;   // Height
    public float frequency = 2f;     // Velocity

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position; // Save initial position
    }

    void Update()
    {
        // Movement on Y
        float newY = startPos.y + Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}