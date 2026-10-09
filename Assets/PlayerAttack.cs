using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackPoint;    // El objeto vacío colocado frente a la espada
    public float attackRange = 0.5f; // Radio del área de golpe
    public LayerMask enemyLayers;    // La capa para detectar al enemigo

    private Animator animator;       // Referencia al componente Animator

    void Start()
    {
        // Obtenemos automáticamente el Animator que está en el mismo objeto
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Al presionar la letra C, ejecuta el ataque
        if (Input.GetKeyDown(KeyCode.C))
        {
            Attack();
        }
    }

    void Attack()
    {
        Debug.Log("¡El jugador atacó con la C!");

        // Dispara la animación de ataque en el Animator usando el Trigger
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

        // Detectar enemigos en un círculo frente al jugador
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            // Intentar obtener el script del enemigo y restarle un golpe
            EnemyPatrol enemyScript = enemy.GetComponent<EnemyPatrol>();
            if (enemyScript != null)
            {
                enemyScript.TakeHit();
            }
        }
    }

    // Para ver el círculo de rango de ataque en la ventana "Scene" de Unity
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}