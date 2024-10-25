using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    public float climbSpeed;
    public Rigidbody2D _rb;
    [SerializeField] private float _jumpForce;

    private Vector3 _velocity = Vector3.zero;
    private bool _isJumping;
    private bool _isGrounded;
    [HideInInspector]
    public bool isClimbing;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask collisionLayers;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public CapsuleCollider2D playerCollider;

    private float horizontalMovement;
    private float verticalMovement;

    public static PlayerMovements instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerMovements dans le scène");
            return;
        }

        instance = this;
    }

    public void Update()
    {
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _isJumping = true;
        }

        Flip(_rb.velocity.x);

        float characterVelocity = Mathf.Abs(_rb.velocity.x);
        animator.SetFloat("Speed", characterVelocity);
        animator.SetBool("isClimbing", isClimbing);
    }

    public void FixedUpdate()
    {
        horizontalMovement = Input.GetAxis("Horizontal") * _moveSpeed * Time.deltaTime;
        verticalMovement = Input.GetAxis("Vertical") * climbSpeed * Time.deltaTime;
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayers);
        MovePlayer(horizontalMovement, verticalMovement);
    }

    public void MovePlayer(float _horizontalMovement, float _verticalMovement)
    {
        if (!isClimbing)
        {
            Vector3 targetVelocity = new Vector2(_horizontalMovement, _rb.velocity.y);
            _rb.velocity = Vector3.SmoothDamp(_rb.velocity, targetVelocity, ref _velocity, 0.05f);

            if (_isJumping)
            {
                _rb.AddForce(new Vector2(0f, _jumpForce));
                _isJumping = false;
            }
        } else
        {
            Vector3 targetVelocity = new Vector2(0, _verticalMovement);
            _rb.velocity = Vector3.SmoothDamp(_rb.velocity, targetVelocity, ref _velocity, 0.05f);
        }
        
    }

    public void Flip(float _velocity)
    {
        if (_velocity > 0.1f)
        {
            spriteRenderer.flipX = false;
        } else if (_velocity < -0.1f)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
