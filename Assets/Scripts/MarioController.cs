using UnityEngine;
using System.Collections;

public class MarioController : MonoBehaviour {
    public int[] varios;

    [Range(0.0f, 10.0f)]
    public float moveSpeed = 3.0f;

    private bool _playerCanMove = true;

    private Rigidbody2D _rigidBody;
    private Animator _animator;
    private AudioSource _audio;

    private bool _facingRight = true;

    public float jumpForce = 600f;
    public float jumpFactor = 1.2f;
    public float bounceFactor = 0.5f;

    public Transform groundCheck;
    public LayerMask whatIsGround;

    private bool _isGrounded = false;

    public AudioClip jumpFx;
    public AudioClip dieFx;
    public AudioClip powerUpFx;

    public GameObject CeilingCollision;
    public GameObject GroundCollision;

    private bool isDead = false;
    private bool isSuperMario = false;
    private bool isGrowing = false;
    private bool isShrinking = false;
    

	// Use this for initialization
	void Start () {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
	}

    void LateUpdate ()
    {
        Vector3 localScale = transform.localScale;

        if (_facingRight && vx < 0)
        {
            _facingRight = false;
            localScale.x *= -1;
        }
        else if (!_facingRight && vx > 0)
        {
            _facingRight = true;
            localScale.x *= -1;
        }

        transform.localScale = localScale;
    }

    bool _isJumping = false;
    bool _isBouncing = false;

    void FixedUpdate ()
    {
        if (_isJumping)
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, 0);
            _rigidBody.AddForce(new Vector2(0, jumpForce));
            _animator.SetTrigger("Jump");

            _audio.PlayOneShot(jumpFx);

            _isJumping = false;
        }

        if (_isBouncing)
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, 0);
            _rigidBody.AddForce(new Vector2(0, jumpForce * bounceFactor));

            _isBouncing = false;
        }
        
    }

    float vx = 0;
    float vy = 0;

    // Update is called once per frame
    void Update () {
        if (!_playerCanMove)
            return;

        vy = _rigidBody.velocity.y;

        //_isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, whatIsGround);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.15f, whatIsGround);

        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    _isGrounded = true;
            }
        }
        else
            _isGrounded = false;

        _animator.SetBool("Grounded", _isGrounded);

        if (_isGrounded && Input.GetButton("Jump"))
        {
            _isJumping = true;
            vy = 0;
        }

        float inputX = Input.GetAxisRaw("Horizontal");

        vx = 0;

        if (inputX > 0)
            vx = moveSpeed;
        else if (inputX < 0)
            vx = -moveSpeed;

        _rigidBody.velocity = new Vector2(vx, vy);
        _animator.SetFloat("Velocity", Mathf.Abs(vx));

        if (isGrowing)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(transform.localScale.x, 2, transform.localScale.z), Time.deltaTime * 2);

            if (transform.localScale.y == 2)
                isGrowing = false;
        }

        if (isShrinking)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(transform.localScale.x, 1, transform.localScale.z), Time.deltaTime * 2);

            if (transform.localScale.y == 1)
                isShrinking = false;
        }
    }

    public void Kill (bool Completely = false)
    {
        if (isDead || isShrinking)
            return;        

        if (!Completely && isSuperMario)
        {
            Shrink();
            return;
        }

        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Stop();

        isDead = true;
        _playerCanMove = false;

        _animator.SetTrigger("Die");
        DisableColliders();
        _rigidBody.velocity = new Vector2();
        _rigidBody.AddForce(new Vector2(0, jumpForce));
        _audio.PlayOneShot(dieFx);

        Invoke("Reset", 5f);
    }

    private void Reset ()
    {
        GameManager.gameManager.Reset();
    }

    public void Grow ()
    {
        if (isSuperMario)
            return;

        isGrowing = true;
        isSuperMario = true;
        jumpForce *= jumpFactor;

        CircleCollider2D groundCollider = GroundCollision.GetComponent<CircleCollider2D>();
        groundCollider.offset = new Vector2(groundCollider.offset.x, -0.4f);
        groundCollider.radius = 0.1f;
        _audio.PlayOneShot(powerUpFx);
    }

    public void Shrink()
    {
        isShrinking = true;
        isSuperMario = false;
        jumpForce /= jumpFactor;
        CircleCollider2D groundCollider = GroundCollision.GetComponent<CircleCollider2D>();
        groundCollider.offset = new Vector2(groundCollider.offset.x, -0.28f);
        groundCollider.radius = 0.21f;
    }

    public void Bounce ()
    {
        _isBouncing = true;
    }

    void DisableColliders ()
    {
        GetComponent<Collider2D>().enabled = false;
        GroundCollision.GetComponent<Collider2D>().enabled = false;
        CeilingCollision.GetComponent<Collider2D>().enabled = false;
    }

    void EnableColliders()
    {
        GetComponent<Collider2D>().enabled = true;
        GroundCollision.GetComponent<Collider2D>().enabled = true;
        CeilingCollision.GetComponent<Collider2D>().enabled = true;
    }
}
