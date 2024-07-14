using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEditor.UI;
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
    Animator _animator;
    Rigidbody _rigidbody;

    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _isGrounded = Physics.Raycast(transform.position, transform.up * -1, out RaycastHit hitInfo, groundCheckDistance, groundLayer);

        if (debug) Debug.DrawLine(transform.position, transform.position + transform.up * -1 * groundCheckDistance, Color.yellow);

        Vector3 moviment = Vector3.right * HorizontalMoviment();

        _rigidbody.MovePosition(transform.position + moviment * Time.deltaTime);

        _rigidbody.AddForce(Vector3.up * VerticalMoviment(), ForceMode.Impulse);
    }

    float HorizontalMoviment()
    {
        float horizontalMoviment = 0f;

        if (Input.GetKey(KeyCode.D))
            horizontalMoviment += horizontalVelocity;
        if (Input.GetKey(KeyCode.A))
            horizontalMoviment -= horizontalVelocity * backwardsDecay;

        _animator.SetInteger("Movement", Mathf.RoundToInt(horizontalMoviment));

        return horizontalMoviment;
    }

    float VerticalMoviment()
    {
        float verticalMoviment = 0f;

        if (Input.GetKeyDown(KeyCode.W) && _isGrounded)
        {
            _animator.SetTrigger("Jump");
            verticalMoviment = verticalForce;
        }

        return verticalMoviment;
    }

    public void Disable()
    {
        _animator.SetInteger("Movement", 0);
        this.enabled = false;
    }
}