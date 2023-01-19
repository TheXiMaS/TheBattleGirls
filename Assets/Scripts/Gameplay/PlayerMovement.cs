using Newtonsoft.Json.Bson;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float _speed = 12f;
    private float _jumpPower = 18f;
    private bool _isFacingRight = true;
    private bool _isRagdolled = false;
    private bool _autoJumpIsAllowed = false;

    private float _moveHorizontal;

    private KeyCode _ragdollKey = KeyCode.Q;
    private KeyCode _strafeKey = KeyCode.LeftShift;

    private Rigidbody2D _rb;
    [SerializeField] private Transform feetPos;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject ragdollColliders;


    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _moveHorizontal = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(_ragdollKey)) Ragdoll();
        
        if (_isRagdolled == false) Flip(IsStrafing());

        if (_isRagdolled == false) JumpLogic();
    }

    private void FixedUpdate()
    {
        if (_isRagdolled == false) MovementLogic();
    }

    private void MovementLogic()
    {
        _rb.velocity = new Vector2(_moveHorizontal * _speed, _rb.velocity.y);
    }

    private void JumpLogic()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpPower);
        }

        if (_autoJumpIsAllowed)
        {
            if (Input.GetButton("Jump") && IsGrounded())
            {
                _rb.velocity = new Vector2(_rb.velocity.x, _jumpPower);
            }
        }

        if (Input.GetButtonUp("Jump") && _rb.velocity.y > 0f)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * 0.5f);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(feetPos.position, 0.2f, groundLayer);
    }

    private void Flip(bool isStrafing)
    {
        if (_isFacingRight == true && _moveHorizontal < 0f || _isFacingRight == false && _moveHorizontal > 0f)
        {
            _isFacingRight = !_isFacingRight;
            if (isStrafing == false) transform.Rotate(0, 180f, 0);
        }
    }

    private bool IsStrafing()
    {
        return Input.GetKey(_strafeKey);
    }

    private void Ragdoll()
    {
        _isRagdolled = !_isRagdolled;
        _rb.freezeRotation = !_isRagdolled;

        if (ragdollColliders != null)
        {
            GetComponent<Collider2D>().isTrigger = _isRagdolled;
            ragdollColliders.SetActive(_isRagdolled);
        }

        if (_isRagdolled == false)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpPower - 6f);
            transform.rotation = Quaternion.identity;
        }
    }

    public void TakeRecoil(float power)
    {
        if (power > 0)
        {
            _rb.AddForce(transform.right * -power);
            _rb.AddForce(transform.up * power / 2);
        }
    }
}
