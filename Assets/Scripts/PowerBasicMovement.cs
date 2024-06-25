using UnityEngine;

public class PowerBasicMovement : MonoBehaviour
{
    [SerializeField] float horizontalVelocity = 8f;
    [SerializeField] float lifeSpan = 3f;
    float timeSinceStart = 0f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * horizontalVelocity * Time.deltaTime);

        timeSinceStart += Time.deltaTime;

        if (timeSinceStart > lifeSpan)
            Destroy(gameObject);
    }
}