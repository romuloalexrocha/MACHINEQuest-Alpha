using UnityEngine;

public class GerenciadorEPI : MonoBehaviour
{
    private int episColetados = 0;
    private const int TOTAL_EPIS = 3; // Ajuste para a quantidade exata de EPIs da cena

    public void ColetarEPI(string nomeEPI)
    {
        episColetados++;
        Debug.Log("EPI Coletado: " + nomeEPI + " | Total: " + episColetados + "/" + TOTAL_EPIS);

        if (episColetados >= TOTAL_EPIS)
        {
            Debug.Log("Todos os EPIs foram coletados! O Sérgio já pode liberar a bancada.");
            // Aqui chamaremos a transição de diálogo do Sérgio mais para frente
        }
    }
}