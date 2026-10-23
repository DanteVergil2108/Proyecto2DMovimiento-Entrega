using UnityEngine;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    private int sandiaCount = 0;
    private int pinaCount = 0;
    private int manzanaCount = 0;
    private int peraCount = 0;
    private int naranjaCount = 0;
    private int duraznoCount = 0;
    private int totalFrutas = 0;

    [Header("UI Reference")]
    public TextMeshProUGUI contadorText;

    private void Start()
    {
        ActualizarTextoUI();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificamos si choca con los objetos según su nombre exacto
        if (collision.gameObject.name.Contains("Sandia"))
        {
            sandiaCount++;
            totalFrutas++;
            ActualizarTextoUI();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.name.Contains("Pina"))
        {
            pinaCount++;
            totalFrutas++;
            ActualizarTextoUI();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.name.Contains("Manzana"))
        {
            manzanaCount++;
            totalFrutas++;
            ActualizarTextoUI();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.name.Contains("Pera"))
        {
            peraCount++;
            totalFrutas++;
            ActualizarTextoUI();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.name.Contains("Naranja"))
        {
            naranjaCount++;
            totalFrutas++;
            ActualizarTextoUI();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.name.Contains("Durazno"))
        {
            duraznoCount++;
            totalFrutas++;
            ActualizarTextoUI();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.name.Contains("Cake"))
        {
            duraznoCount++;
            totalFrutas++;
            ActualizarTextoUI();
            Destroy(collision.gameObject);
        }
    }

    void ActualizarTextoUI()
    {
        if (contadorText != null)
        {
            // Muestra el detalle completo de cada fruta en la esquina inferior
            contadorText.text = "Sandia: " + sandiaCount + 
                                "  Pina: " + pinaCount +
                                "  Manzana: " + manzanaCount +
                                "  Pera: " + peraCount +
                                "  Naranja: " + naranjaCount +
                                "  Durazno: " + duraznoCount +
                                "  Cake: " + duraznoCount;
        }
    }
}