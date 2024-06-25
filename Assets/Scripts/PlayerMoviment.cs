using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{
    [Header("Horizontal Moviment")]
    [SerializeField] float horizontalVelocity = 10f;
    [SerializeField] float backwardsDecay = 0.8f;

    [Header("Jump")]
    [SerializeField] float verticalForce = 1f;
    [SerializeField] float groundCheckDistance = 1.3f;
    [SerializeField] LayerMask groundLayer;

    [Header("Debug")]
    [SerializeField] bool debug = false;

    bool _isGrounded = true;

    // Update is called once per frame
    void Update()
    {
        _isGrounded = Physics.Raycast(transform.position, transform.up * -1, out RaycastHit hitInfo, groundCheckDistance, groundLayer);

        if (debug) Debug.DrawLine(transform.position, transform.position + transform.up * -1 * groundCheckDistance, Color.yellow);

        Vector3 moviment = Vector3.right * HorizontalMoviment();

        transform.Translate(moviment * Time.deltaTime);

        GetComponent<Rigidbody>().AddForce(Vector3.up * VerticalMoviment(), ForceMode.Impulse);
    }

    float HorizontalMoviment()
    {
        float horizontalMoviment = 0f;

        if (Input.GetKey(KeyCode.D))
            horizontalMoviment += horizontalVelocity;
        if (Input.GetKey(KeyCode.A))
            horizontalMoviment -= horizontalVelocity * backwardsDecay;

        return horizontalMoviment;
    }

    float VerticalMoviment()
    {
        float verticalMoviment = 0f;

        if (Input.GetKeyDown(KeyCode.W) && _isGrounded)
            verticalMoviment = verticalForce;

        return verticalMoviment;
    }
}
