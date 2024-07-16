using UnityEngine;

public class PowerBasicMovement : MonoBehaviour
{
    [SerializeField] float horizontalVelocity = 8f;
    [SerializeField] float lifeSpan = 3f;
    float timeSinceStart = 0f;

    public bool isRight = true;

    // Update is called once per frame
    void Update()
    {
        transform.Translate((isRight ? transform.forward : -transform.forward) * horizontalVelocity * Time.deltaTime);

        timeSinceStart += Time.deltaTime;

        if (timeSinceStart > lifeSpan)
            Destroy(gameObject);
    }
}