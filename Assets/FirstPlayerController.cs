using UnityEngine;

public class FirstPlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private bool movement = true;
    [SerializeField] private bool horizontalMovement = true;
    [SerializeField] private bool verticalMovement;
    [SerializeField][Min(0)] private float movementSpeed = 14f;

    [Header("Jump")]
    [SerializeField] private bool jumping = true;
    [SerializeField][Min(0)] private float jumpForce = 14f;

    [Header("Additional")]
    [SerializeField] private Transform feet;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private string[] ignorableObjectsTags;
    [SerializeField] private MovementStyle movementStyle = MovementStyle.Physical;

    [Header("Sprite Settings")]
    [SerializeField] private SpriteFacing spriteFacingDirection;

    private Collider2D _collider2D;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;

    private float _horizontalInput;
    private float _verticalInput;

    private bool _jumpUp;
    private bool _jumpDown;
    private bool _jump;

    private static readonly int Speed = Animator.StringToHash("Speed");

    private const float OverlapRadius = .2f;

    private void Awake() 
    {
        Init();
    }

    private void Start()
    {
        if (jumpForce == 0) jumpForce = movementSpeed;
    }

    private void Update()
    {
        CanJump();
    }

    private void FixedUpdate()
    {
        CanMove();
    }

    private void CanMove()
    {
        if (movement == false) return;
        
        if (movementStyle == MovementStyle.Physical)
        {
            if (horizontalMovement)
                _horizontalInput = Input.GetAxis(Axis.Horizontal);
        
            if (verticalMovement)
                _verticalInput = Input.GetAxis(Axis.Vertical);
        }
        else if (movementStyle == MovementStyle.Linear)
        {
            if (horizontalMovement)
                _horizontalInput = Input.GetAxisRaw(Axis.Horizontal);
        
            if (verticalMovement)
                _verticalInput = Input.GetAxisRaw(Axis.Vertical);
        }
        
        if (_animator != null) _animator.SetFloat(Speed, Mathf.Abs(_horizontalInput));

        if (_horizontalInput > 0)
        {
            MoveHorizontal();

            Flip(SpriteFacing.Right);
        }
        else if (_horizontalInput < 0)
        {
            MoveHorizontal();
            
            Flip(SpriteFacing.Left);
        }
        else
        {
            _rigidbody2D.velocity = new Vector2(
                0, _rigidbody2D.velocity.y);
        }

        if (_verticalInput > 0)
        {
            MoveVertical();
        }
        else if (_verticalInput < 0)
        {
            MoveVertical();
        }
    }

    private void MoveHorizontal()
    {
        _rigidbody2D.velocity = new Vector2(
                _horizontalInput * movementSpeed, _rigidbody2D.velocity.y);
    }

    private void MoveVertical() 
    {
        _rigidbody2D.velocity = new Vector2(
            _rigidbody2D.velocity.x, _verticalInput * movementSpeed);
    }

    private void CanJump()
    {
        if (jumping == false) return;
        
        _jumpDown = Input.GetButtonDown(Button.Jump);
        _jump = Input.GetButton(Button.Jump);
        _jumpUp = Input.GetButtonUp(Button.Jump);

        if (_jumpDown && IsGrounded())
        {
            Jump();
        }

        if (_jumpUp && _rigidbody2D.velocity.y > 0f)
        {
            _rigidbody2D.velocity = new Vector2(
                _rigidbody2D.velocity.x, _rigidbody2D.velocity.y * .5f);
        }
    }

    private void Jump()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpForce);
    }

    private void Init()
    {
        _collider2D = GetComponent<Collider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void SetIgnorableObjects()
    {
        foreach (var item in Resources.FindObjectsOfTypeAll<Item>())
        {
            Collider2D ignorableCollider = item.GetComponent<Collider2D>();

            if (ignorableCollider == null) continue;

            if (ignorableCollider.isTrigger == false)
                Physics2D.IgnoreCollision(_collider2D, ignorableCollider);
        }
    }

    private bool IsGrounded()
    {
        if (feet == null) return false;

        return Physics2D.OverlapCircle(
            feet.position, OverlapRadius, groundLayer);
    }

    private void Flip(SpriteFacing direction)
    {
        if (direction == SpriteFacing.Left)
        {
            transform.localRotation = new Quaternion(0, 180, 0, 0);
        }
        else if (direction == SpriteFacing.Right)
        {
            transform.localRotation = new Quaternion(0, 0, 0, 0);
        }

        spriteFacingDirection = direction;
    }

    private enum SpriteFacing
    {
        Left,
        Right
    }

    private enum MovementStyle
    {
        Physical,
        Linear
    }
}