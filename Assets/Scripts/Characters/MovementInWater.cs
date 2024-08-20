using UnityEngine;
using UnityEngine.UI;

public class MovementInWater : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D coll;
    [SerializeField] private LayerMask jumpableGround;

    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float waterMoveSpeed = 2f;
    public float waterJumpForce = 5f;

    private bool moveLeft;
    private bool moveRight;
    private float horizontalMove;
    private bool isInWater = false;
    private bool facingRight = true; // Biến theo dõi hướng nhân vật
    private GameObject interactObject;

    public Button interactButton;
    public Gem gem;
    private enum MovementState { idle, running, jumping, falling }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();

        moveLeft = false;
        moveRight = false;

        interactButton.gameObject.SetActive(false);
        interactButton.onClick.AddListener(OnInteractButtonClicked);
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
                Flip();
            }
        }
        else if (moveRight)
        {
            horizontalMove = moveSpeed;
            if (!facingRight) 
            {
                Flip();
            }
        }
        else
        {
            horizontalMove = 0;
        }
    }

    public void JumpButton()
    {
        if (IsGrounded())
        {
            float jumpPower = isInWater ? waterJumpForce : jumpForce;
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }
    }

    private void FixedUpdate()
    {
        float adjustedSpeed = isInWater ? waterMoveSpeed : moveSpeed;
        rb.velocity = new Vector2(horizontalMove * adjustedSpeed, rb.velocity.y);
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
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            isInWater = true;
        }
        else if (other.CompareTag("InteractObject"))
        {
            interactObject = other.gameObject;
            interactButton.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            isInWater = false;
        }
        else if (other.CompareTag("InteractObject"))
        {
            interactObject = null;
            interactButton.gameObject.SetActive(false);
        }
    }

    public void OnInteractButtonClicked()
    {
        if (interactObject != null)
        {
            Chest chest = interactObject.GetComponent<Chest>();
            if (chest != null)
            {
                chest.ToggleChest();
            }
            else
            {
                CantCloseChest cantCloseChest = interactObject.GetComponent<CantCloseChest>();
                if (cantCloseChest != null)
                {
                    cantCloseChest.OpenChest();
                }
            }
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
