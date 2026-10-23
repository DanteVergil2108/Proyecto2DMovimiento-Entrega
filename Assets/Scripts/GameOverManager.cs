using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel; // Arrastra aquí tu panel de GameOver desde la Hierarchy

    void Start()
    {
        // Nos aseguramos de que al iniciar o reiniciar el nivel, el tiempo corra normal
        Time.timeScale = 1f;

        // Ocultamos el panel al iniciar
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    public void MostrarGameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // Congelamos el tiempo de juego cuando aparece el panel
        Time.timeScale = 0f;
    }

    public void ReiniciarJuego()
    {
        // IMPORTANTE: Descongelamos el tiempo ANTES de reiniciar la escena
        Time.timeScale = 1f;

        // Recargamos la escena activa actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}