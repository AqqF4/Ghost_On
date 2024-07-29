using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;            // Скорость движения
    public float jumpForce = 10f;       // Сила прыжка
    public Transform groundCheck;       // Точка проверки земли
    public float groundCheckRadius = 0.2f;  // Радиус проверки земли
    public LayerMask groundLayer;       // Слой, представляющий землю
    public Animator anim;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isJumping;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if (moveInput > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Смотрит вправо
            anim.SetBool("isRunning", true);
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Смотрит влево
            anim.SetBool("isRunning", true);
        }
        else if(moveInput == 0)
        {
            anim.SetBool("isRunning", false);
        }
    }

    void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}