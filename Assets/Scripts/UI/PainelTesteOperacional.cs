using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PainelTesteOperacional : MonoBehaviour
{
    [Header("Componentes da UI")]
    [SerializeField] private GameObject painelVisual;
    [SerializeField] private Slider medidorEstabilizacao;
    [SerializeField] private Image imagemPreenchimentoSlider;
    [SerializeField] private TextMeshProUGUI textoStatusTeste;
    [SerializeField] private TextMeshProUGUI textoRPM;

    [Header("Configurações do Teste")]
    [SerializeField] private float duracaoTeste = 2.0f;
    [SerializeField] private float valorZonaVerde = 0.8f; // 80% da barra = Zona Verde
    [SerializeField] private Color corAnalisando = new Color(1f, 0.75f, 0f); // Amarelo
    [SerializeField] private Color corZonaVerde = new Color(0.18f, 0.8f, 0.44f); // Verde

    [Header("Conclusão da OS")]
    [SerializeField] private GerenciadorDialogo gerenciadorDialogo;
    [SerializeField] private GerenciadorAudio gerenciadorAudio;
    [SerializeField] private DialogoDados dialogoConclusaoOS;
    [SerializeField] private GameObject telaMissaoConcluida;

    private bool testando = false;
    private float tempoDecorrido = 0f;

    private void Start()
    {
        if (painelVisual != null) painelVisual.SetActive(false);
        if (telaMissaoConcluida != null) telaMissaoConcluida.SetActive(false);

        if (gerenciadorDialogo == null)
            gerenciadorDialogo = FindFirstObjectByType<GerenciadorDialogo>(FindObjectsInactive.Include);

        if (gerenciadorAudio == null)
            gerenciadorAudio = FindFirstObjectByType<GerenciadorAudio>(FindObjectsInactive.Include);
    }

    private void Update()
    {
        if (!testando) return;

        tempoDecorrido += Time.deltaTime;
        float progresso = tempoDecorrido / duracaoTeste;

        if (progresso < 1f)
        {
            // Sobe a barra e o RPM proporcionalmente ao tempo
            if (medidorEstabilizacao != null)
                medidorEstabilizacao.value = progresso * valorZonaVerde;

            if (textoRPM != null)
            {
                int rpmAtual = (int)(progresso * 1750f);
                textoRPM.text = rpmAtual + " RPM";
            }
        }
        else
        {
            // Chegou ao fim do tempo e estabilizou na Zona Verde
            testando = false;
            ConcluirTeste();
        }
    }

    public void IniciarTesteOperacional()
    {
        tempoDecorrido = 0f;
        testando = true;

        if (painelVisual != null)
            painelVisual.SetActive(true);

        if (imagemPreenchimentoSlider != null)
            imagemPreenchimentoSlider.color = corAnalisando;

        if (textoStatusTeste != null)
            textoStatusTeste.text = "ACIONANDO MOTOR... CALIBRANDO";
    }

    private void ConcluirTeste()
    {
        if (medidorEstabilizacao != null)
            medidorEstabilizacao.value = valorZonaVerde;

        if (textoRPM != null)
            textoRPM.text = "1750 RPM";

        if (imagemPreenchimentoSlider != null)
            imagemPreenchimentoSlider.color = corZonaVerde;

        if (textoStatusTeste != null)
            textoStatusTeste.text = "ZONA VERDE — OPERAÇÃO ESTÁVEL ✓";

        if (gerenciadorAudio != null)
            gerenciadorAudio.TocarSomAcerto();

        if (gerenciadorDialogo != null && dialogoConclusaoOS != null)
            gerenciadorDialogo.IniciarDialogo(dialogoConclusaoOS);
    }
}
