using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [Header("Interfaz de Victoria")]
    public GameObject victoryPanel; // Arrastrarás el panel aquí desde Unity

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement player = collision.GetComponent<PlayerMovement>();

            if (player != null && player.hasKey)
            {
                Debug.Log("¡FELICIDADES! ¡HAS GANADO EL JUEGO!");

                // Activa el panel visual en la pantalla
                if (victoryPanel != null)
                {
                    victoryPanel.SetActive(true);
                }

                // Opcional: Pausa el juego para que el personaje no se siga moviendo
                Time.timeScale = 0f;
            }
            else
            {
                Debug.Log("Te falta la llave para abrir el portal.");
            }
        }
    }

    // Método para el botón de reiniciar
    public void ReiniciarNivel()
    {
        Time.timeScale = 1f; // Restaura el tiempo normal del juego
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Recarga la escena actual
    }
}