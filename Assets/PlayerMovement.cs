using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 7f;

    private Rigidbody2D rb;
    private Animator anim;
    private bool isGrounded;

    [Header("Detector de Suelo")]
    public Transform groundCheck;
    public float checkRadius = 0.2f;
    public LayerMask whatIsGround;

    private bool mirandoDerecha = true;

    [Header("Power-ups y Coleccionables")]
    public bool hasKey = false;         // Indica si tiene la llave
    public bool canDoubleJump = false;  // Se activa al tomar el power-up
    private bool doubleJumpAvailable;   // Controla si ya gastó el salto extra en el aire

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float movement = Input.GetAxis("Horizontal");

        // Detección de suelo
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if (isGrounded)
        {
            doubleJumpAvailable = true; // Recarga el doble salto al tocar el suelo
        }

        rb.linearVelocity = new Vector2(movement * speed, rb.linearVelocity.y);

        // Animación de velocidad
        float currentSpeed = isGrounded ? Mathf.Abs(movement) : 0f;
        anim.SetFloat("Speed", currentSpeed);

        // Voltear sprite
        if (movement > 0 && !mirandoDerecha)
        {
            Voltear();
        }
        else if (movement < 0 && mirandoDerecha)
        {
            Voltear();
        }

        // Lógica de Salto y Doble Salto
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                Saltar();
            }
            else if (canDoubleJump && doubleJumpAvailable)
            {
                Saltar();
                doubleJumpAvailable = false; // Gasta el doble salto
            }
        }
    }

    private void Saltar()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        anim.SetTrigger("JumpTrigger");
    }

    private void Voltear()
    {
        mirandoDerecha = !mirandoDerecha;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
        }
    }
}