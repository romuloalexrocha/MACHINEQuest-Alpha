using UnityEngine;
using UnityEngine.UI;

public class MiniGameLubrificacao : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private GerenciadorAudio ga;
    [SerializeField] private GerenciadorDialogo gd;
    [SerializeField] private PontoLubrificacao pl;
    [SerializeField] private ControleCarcassa cc;
    [SerializeField] private Image imagemOleoVisor;
    [SerializeField] private RectTransform rectMarcador;
    [SerializeField] private RectTransform rectZonaEspecial;
    [SerializeField] private float impulsoClique = 0.15f;
    [SerializeField] private float alturaBarra = 300f; // Altura da barra de óleo em pixels

    [Header("Diálogos")]
    [SerializeField] private DialogoDados dialogoTransbordou;
    //[SerializeField] private DialogoDados dialogoConcluido;

    [Header("Parâmetros do Mini-Game")]
    private float nivelMarcador = 0f;
    private float posicaoZona = 0.5f;
    private float progressoVisor = 0f;
    private float metaNivel = 0.9f;


    [SerializeField] private float gravidade = 0.35f;
    [SerializeField] private float velocidadeZona = 1f;
    [SerializeField] private float velocidadeRapida = 0.5f;
    [SerializeField] private float velocidadeNormal = 0.25f;
    [SerializeField] private float margemZona = 0.15f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BuscarReferencias();
    }

    // Update is called once per frame
    void Update()
    {
        nivelMarcador -= Time.deltaTime * gravidade;
        nivelMarcador = Mathf.Max(0f, nivelMarcador);

        posicaoZona = Mathf.PingPong(Time.time * velocidadeZona, 1f);

        bool naZonaEspecial = Mathf.Abs(nivelMarcador - posicaoZona) <= margemZona;

        if (nivelMarcador > 0.05f)
        {
            if (naZonaEspecial)
                progressoVisor += velocidadeRapida * Time.deltaTime;
            else
                progressoVisor += velocidadeNormal * Time.deltaTime;
        }

        imagemOleoVisor.fillAmount = progressoVisor;

        // Atualiza a posição vertical do marcador do jogador
        if (rectMarcador != null)
        {
            float novaPosicaoY = nivelMarcador * alturaBarra;
            rectMarcador.anchoredPosition = new Vector2(rectMarcador.anchoredPosition.x, novaPosicaoY);
        }

        // Atualiza a posição vertical da zona especial
        if (rectZonaEspecial != null)
        {
            float alturaUtil = alturaBarra - rectZonaEspecial.rect.height;
            float novaPosicaoZonaY = (posicaoZona * alturaUtil) + (rectZonaEspecial.rect.height * 0.5f);
            rectZonaEspecial.anchoredPosition = new Vector2(rectZonaEspecial.anchoredPosition.x, novaPosicaoZonaY);
        }

        if (progressoVisor >= metaNivel)
        {
            pl.ConcluirLubrificacao();
            cc.LiberarFechamento();
            //ga.TocarSomAcerto();
            //gd.IniciarDialogo(dialogoConcluido);
            gameObject.SetActive(false);
        }
    }

    void OnEnable()
    {
        BuscarReferencias();
        nivelMarcador = 0f;
        progressoVisor = 0f;
    }

    private void BuscarReferencias()
    {
        if (ga == null) ga = FindFirstObjectByType<GerenciadorAudio>(FindObjectsInactive.Include);
        if (gd == null) gd = FindFirstObjectByType<GerenciadorDialogo>(FindObjectsInactive.Include);
        if (pl == null) pl = FindFirstObjectByType<PontoLubrificacao>(FindObjectsInactive.Include);
        if (cc == null) cc = FindFirstObjectByType<ControleCarcassa>(FindObjectsInactive.Include);
    }

    public void AoClicarBombear()
    {
        nivelMarcador += impulsoClique;
        if (nivelMarcador > 1f)
        {
            nivelMarcador = 0f;
            progressoVisor = 0f;
            ga.TocarSomErro();
            gd.IniciarDialogo(dialogoTransbordou);
        }
    }
}
