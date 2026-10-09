using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // Necesario para usar las corrutinas de tiempo

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 2f;
    public float patrolDistance = 2f;

    private Vector2 startPos;
    private Rigidbody2D rb;
    private Animator anim; // Referencia para controlar el Animator
    private bool movingRight = true;

    // Variables para el sistema de daño y aturdimiento
    private int hitCount = 0;
    private bool isStunned = false;

    [Header("Sprites de Aturdimiento")]
    public Sprite shellSprite;         // Arrastra aquí el sprite del caracol en el caparazón
    private SpriteRenderer spriteRenderer;
    private Sprite originalSprite;     // Para guardar el sprite normal y restaurarlo luego

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>(); // Obtenemos el componente Animator al iniciar
        startPos = transform.position;

        // Guardamos el sprite con el que nace el caracol
        if (spriteRenderer != null)
        {
            originalSprite = spriteRenderer.sprite;
        }
    }

    void Update()
    {
        // Si está aturdido, no se mueve
        if (isStunned)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            return;
        }

        // Movimiento horizontal constante dentro de sus límites
        if (movingRight)
        {
            rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);

            if (transform.position.x >= startPos.x + patrolDistance)
            {
                movingRight = false;
                Flip();
            }
        }
        else
        {
            rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);

            if (transform.position.x <= startPos.x - patrolDistance)
            {
                movingRight = true;
                Flip();
            }
        }
    }

    void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    // Método para recibir impactos del ataque del jugador
    public void TakeHit()
    {
        if (isStunned) return; // Si ya está aturdido, ignorar nuevos golpes hasta que despierte

        hitCount++;
        Debug.Log("¡Enemigo golpeado! Golpes actuales: " + hitCount);

        if (hitCount >= 3)
        {
            StartCoroutine(StunRoutine());
        }
    }

    // Corrutina que maneja los 3 segundos de inmovilidad, sprite de caparazón y seguridad
    IEnumerator StunRoutine()
    {
        isStunned = true;
        hitCount = 0; // Reiniciamos el contador de golpes

        // 1. Apagar el Animator para que ceda el control total del SpriteRenderer
        if (anim != null)
        {
            anim.enabled = false;
        }

        // 2. Cambiar al sprite del caparazón al aturdirse
        if (shellSprite != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = shellSprite;
        }

        Debug.Log("¡Enemigo aturdido por 5 segundos! Ahora es inofensivo.");

        yield return new WaitForSeconds(5f); // Espera 5 segundos exactos

        // 3. Regresar al sprite original cuando despierte
        if (originalSprite != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = originalSprite;
        }

        // 4. Volver a encender el Animator para que reanude sus animaciones normales
        if (anim != null)
        {
            anim.enabled = true;
        }

        isStunned = false;
        Debug.Log("¡El enemigo ha vuelto a la vida y se mueve de nuevo!");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si choca con el jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            // Solo mata al jugador SI EL ENEMIGO NO ESTÁ ATURDIDO
            if (!isStunned)
            {
                Debug.Log("¡El enemigo activo tocó al jugador! Reiniciando nivel...");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                Debug.Log("El enemigo está aturdido, el jugador lo atraviesa sin sufrir daño.");
            }
        }
    }
}