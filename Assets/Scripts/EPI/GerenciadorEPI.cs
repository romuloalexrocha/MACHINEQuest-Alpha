using UnityEngine;
using UnityEngine.UI;

public class GerenciadorEPI : MonoBehaviour
{
    [Header("Interface dos EPIs (Barra Horizontal)")]
    [SerializeField] private Image[] slotsEPIs;

    [Header("Gerenciadores")]
    [SerializeField] private GerenciadorAudio ga;
    [SerializeField] private GerenciadorNavegacao gn;

    [Header("Diálogo Próxima Etapa (Bandeja)")]
    [SerializeField] private GerenciadorDialogo gd;
    [SerializeField] private DialogoDados dialogoBandeja;

    private const int TOTAL_EPIS = 5; // Ajuste para a quantidade exata de EPIs da cena
    private int episColetados = 0;

    public void ColetarEPI(string nomeEPI, Sprite iconeEPI)
    {
        if (episColetados < slotsEPIs.Length && slotsEPIs[episColetados] != null)
        {
            slotsEPIs[episColetados].sprite = iconeEPI;
            slotsEPIs[episColetados].color = Color.white;
            slotsEPIs[episColetados].enabled = true;
        }

        episColetados++;
        Debug.Log($"EPI coletado: {nomeEPI} ({episColetados}/{TOTAL_EPIS})");

        if (episColetados >= TOTAL_EPIS)
        {
            if (ga != null) ga.TocarSomAcerto();
            Debug.Log("Todos os EPIs foram coletados! O Sérgio já pode liberar a bancada.");
            // Aqui chamaremos a transição de diálogo do Sérgio mais para frente
            if (gn != null) gn.LiberarNavegacaoGeral();
            if(gd != null && dialogoBandeja != null)
                gd.EnfileirarDialogo(dialogoBandeja);
        }
    }

    public bool TodosEPIsColetados()
    {
        return episColetados >= TOTAL_EPIS;
    }
}
