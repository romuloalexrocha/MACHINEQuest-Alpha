using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BotoesGiroUI : MonoBehaviour
{
    [Header("Botões Dedicados ao Giro do Redutor")]
    [SerializeField] private Button botaoGirarEsquerda;
    [SerializeField] private Button botaoGirarDireita;

    [Header("Indicador de Visão na Tela")]
    [SerializeField] private TextMeshProUGUI textoVisaoAtual;

    private ControladorVistasRedutor controladorRedutor;

    private void Start()
    {
        controladorRedutor = FindFirstObjectByType<ControladorVistasRedutor>(FindObjectsInactive.Include);

        if (botaoGirarEsquerda != null)
        {
            botaoGirarEsquerda.onClick.AddListener(ClicarEsquerda);
        }

        if (botaoGirarDireita != null)
        {
            botaoGirarDireita.onClick.AddListener(ClicarDireita);
        }

        AtualizarTextoVisao();
        DefinirVisibilidadeSetas(true);
    }

    public void DefinirVisibilidadeSetas(bool visivel)
    {
        if (botaoGirarEsquerda != null)
            botaoGirarEsquerda.gameObject.SetActive(visivel);

        if (botaoGirarDireita != null)
            botaoGirarDireita.gameObject.SetActive(visivel);

        if (textoVisaoAtual != null)
            textoVisaoAtual.gameObject.SetActive(visivel);
    }

    private void ClicarEsquerda()
    {
        if (controladorRedutor == null)
            controladorRedutor = FindFirstObjectByType<ControladorVistasRedutor>(FindObjectsInactive.Include);

        if (controladorRedutor != null)
        {
            controladorRedutor.GirarEsquerda();
            AtualizarTextoVisao();
        }
    }

    private void ClicarDireita()
    {
        if (controladorRedutor == null)
            controladorRedutor = FindFirstObjectByType<ControladorVistasRedutor>(FindObjectsInactive.Include);

        if (controladorRedutor != null)
        {
            controladorRedutor.GirarDireita();
            AtualizarTextoVisao();
        }
    }

    private void AtualizarTextoVisao()
    {
        if (textoVisaoAtual == null) return;

        if (controladorRedutor == null)
            controladorRedutor = FindFirstObjectByType<ControladorVistasRedutor>(FindObjectsInactive.Include);

        if (controladorRedutor != null)
        {
            string nomeVista = controladorRedutor.ObterNomeVistaAtual();
            if (!string.IsNullOrEmpty(nomeVista))
            {
                textoVisaoAtual.text = "Visão " + nomeVista.ToUpper();
            }
        }
    }
}
