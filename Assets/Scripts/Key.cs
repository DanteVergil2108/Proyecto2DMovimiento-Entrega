using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Busca el script del jugador y le da la llave
            PlayerMovement player = collision.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.hasKey = true;
                Debug.Log("¡Llave recolectada!");
                Destroy(gameObject); // Desaparece la llave de la escena
            }
        }
    }
}