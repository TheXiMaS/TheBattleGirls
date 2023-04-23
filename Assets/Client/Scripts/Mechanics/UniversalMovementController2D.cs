using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class UniversalMovementController2D : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private bool movement = true;
    [SerializeField] private bool horizontalMovement = true;
    [SerializeField] private bool verticalMovement;
    [SerializeField][Min(0)] private float horizontalSpeed;
    [SerializeField][Min(0)] private float verticalSpeed;
    [SerializeField] private bool canCrouch;
    
    [Header("Jump")]
    [SerializeField] private bool canJump = true;
    [SerializeField][Min(0)] private float jumpForce;
    [SerializeField] private bool doubleJump;
    
    [Header("Fly (Does not work!)")]
    [SerializeField] private bool canFly;
    [SerializeField][Min(0)] private float flyingSpeed;
    [SerializeField][Min(0)] private float maxHoveringTime;

    [Header("Additional")]
    [SerializeField] private Transform feet;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private string[] ignorableTags;
    [SerializeField] private MovementStyle movementStyle = MovementStyle.Physical;
    
    [Header("Input Settings")]
    [SerializeField] private bool customButtons;
    [SerializeField] private KeyCode leftButton;
    [SerializeField] private KeyCode rightButton;
    [SerializeField] private KeyCode upButton;
    [SerializeField] private KeyCode downButton;
    [SerializeField] private KeyCode jumpButton;
    
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

    private void Start()
    {
        Init();
    }
    
    private void Update()
    {
        if (canJump)
        {
            if (customButtons == false)
            {
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
                        _rigidbody2D.velocity.x, _rigidbody2D.velocity.y * 0.5f);

                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (movement)
        {
            if (customButtons == false)
            {
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
            }
            else
            {
                if (horizontalMovement)
                    _horizontalInput = KeyboardInput.GetCustomRawHorizontal(
                        leftButton, rightButton);
            
                if (verticalMovement)
                    _verticalInput = KeyboardInput.GetCustomRawVertical(
                        upButton, downButton);
            }
            
            if (_animator != null) _animator.SetFloat(Speed, Mathf.Abs(_horizontalInput));

            if (_horizontalInput > 0)
            {
                MoveRight();
            }
            else if (_horizontalInput < 0)
            {
                MoveLeft();
            }
            else
            {
                _rigidbody2D.velocity = new Vector2(
                    0, _rigidbody2D.velocity.y);
            }

            if (_verticalInput > 0)
            {
                MoveUp();
            }
            else if (_verticalInput < 0)
            {
                MoveDown();
            }
        }
    }

    private void MoveLeft() 
    {
        if (_horizontalInput == 0)
        {
            _rigidbody2D.velocity = new Vector2(
                0 * horizontalSpeed, _rigidbody2D.velocity.y);
        }
        else if (_horizontalInput < 0)
        {
            _rigidbody2D.velocity = new Vector2(
                _horizontalInput * horizontalSpeed, _rigidbody2D.velocity.y);
        }

        Flip(SpriteFacing.Left);
    }

    private void MoveRight() 
    {
        if (_horizontalInput == 0)
        {
            _rigidbody2D.velocity = new Vector2(
                0 * horizontalSpeed, _rigidbody2D.velocity.y);
        }
        else if (_horizontalInput > 0)
        {
            _rigidbody2D.velocity = new Vector2(
                _horizontalInput * horizontalSpeed, _rigidbody2D.velocity.y);
        }

        Flip(SpriteFacing.Right);
    }

    private void MoveUp() 
    {
        if (_verticalInput == 0)
        {
            _rigidbody2D.velocity = new Vector2(
                _rigidbody2D.velocity.x, 0 * verticalSpeed);
        }
        else if (_verticalInput > 0)
        {
            _rigidbody2D.velocity = new Vector2(
                _rigidbody2D.velocity.x, _verticalInput * verticalSpeed);
        }
    }

    private void MoveDown() 
    {
        if (_verticalInput == 0)
        {
            _rigidbody2D.velocity = new Vector2(
                _rigidbody2D.velocity.x, 0 * verticalSpeed);
        }
        else if (_verticalInput < 0)
        {
            _rigidbody2D.velocity = new Vector2(
                _rigidbody2D.velocity.x, _verticalInput * verticalSpeed);
        }
    }

    private void Jump()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpForce);
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

    private bool IsGrounded()
    {
        if (feet != null)
        {
            return Physics2D.OverlapCircle(
                feet.position, OverlapRadius, groundLayers);
        }
        return false;
    }

    private void Init()
    {
        _collider2D = GetComponent<Collider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void SetIgnorableLayers()
    {
        foreach (var c in Resources.FindObjectsOfTypeAll<Item>())
        {
            Collider2D collider2 = c.GetComponent<Collider2D>();
            if (collider2 != null)
                if (collider2.isTrigger == false)
                    Physics2D.IgnoreCollision(_collider2D, collider2);
        }
    }
    
    private enum SpriteFacing
    {
        Left,
        Right
    }

    private enum HorizontalMovementDirection
    {
        Left,
        Right
    }

    private enum VerticalMovementDirection
    {
        Up,
        Down
    }

    private enum MovementState
    {
        Grounded,
        Moving,
        Jumping,
        Falling,
        Hovering
    }

    private enum MovementStyle
    {
        Physical,
        Linear
    }
}
