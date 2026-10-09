using UnityEngine;

public class DoubleJumpPowerUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificamos si choca con el jugador (ya sea por Tag o por componente)
        PlayerMovement player = collision.GetComponent<PlayerMovement>();

        if (player != null)
        {
            player.canDoubleJump = true;
            Debug.Log("¡Power-up tomado! Doble salto habilitado.");
            Destroy(gameObject); // Desaparece el coleccionable
        }
    }
}