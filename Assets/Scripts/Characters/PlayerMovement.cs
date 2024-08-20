using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D coll;
    [SerializeField] private LayerMask jumpableGround;
 
    public float moveSpeed = 0f;
    public float jumpForce = 0f;

    private bool moveLeft;
    private bool moveRight;
    private float horizontalMove;
    private bool facingRight = true;
    private enum MovementState { idle, running, jumping, falling }

    public GameObject dustEffectPrefab;
    private GameObject currentDustEffect;
    public Vector2 dustOffset;
    AudioManager audioManager;

    public Gem gem;

    private void Awake()
    {
        audioManager=GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();

        moveLeft = false;
        moveRight = false;;
    }

    public void PointerDownLeft()
    {
        moveLeft = true;
    }

    public void PointerUpLeft()
    {
        moveLeft = false;
    }

    public void PointerDownRight()
    {
        moveRight = true;
    }

    public void PointerUpRight()
    {
        moveRight = false;
    }
    void Update()
    {
        MovePlayer();
        UpdateAnimationState();
    }
    private void MovePlayer()
    {
        if (moveLeft)
        {
            horizontalMove = -moveSpeed;
            if (facingRight) 
            {
                Rotate();
            }
          //  audioManager.PlaySFX(audioManager.runGrass);
        }
        else if (moveRight)
        {
            horizontalMove = moveSpeed;
            if (!facingRight) 
            {
                Rotate();
            }
           // audioManager.PlaySFX(audioManager.runGrass);

        }
        else
        {
            horizontalMove = 0;
        }
    }
    public void jumpButton()
    {
        if (IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalMove, rb.velocity.y);
    }
    
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
    private void UpdateAnimationState()
    {
        MovementState state;

        if (horizontalMove != 0)
        {
            state = MovementState.running;
            PlayDustEffect();

        }
        else
        {
            state = MovementState.idle;
            if (currentDustEffect != null)
            {
                Destroy(currentDustEffect);
                currentDustEffect = null;
            }
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
            if (currentDustEffect != null)
            {
                Destroy(currentDustEffect);
                currentDustEffect = null;
            }
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }
    private void Rotate()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
        if (currentDustEffect != null)
        {
            Vector3 theScale = currentDustEffect.transform.localScale;
            theScale.x *= -1;
            currentDustEffect.transform.localScale = theScale;
        }
    }
    private void PlayDustEffect()
    {
        if (currentDustEffect == null)
        {
            Vector2 dustPosition = new Vector2(transform.position.x + (facingRight ? dustOffset.x : -dustOffset.x), transform.position.y + dustOffset.y);
            currentDustEffect = Instantiate(dustEffectPrefab, dustPosition, Quaternion.identity);
            currentDustEffect.transform.localScale = new Vector3(facingRight ? 1 : -1, 1, 1); // Lật theo hướng của nhân vật
        }
        else
        {
            currentDustEffect.transform.position = new Vector2(transform.position.x + (facingRight ? dustOffset.x : -dustOffset.x), transform.position.y + dustOffset.y);
        }
    }
     
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Gem"))
        {
            Destroy(other.gameObject);
            gem.gemCount++;
        }
    }
}
