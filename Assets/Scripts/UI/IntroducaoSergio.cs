using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroducaoSergio : MonoBehaviour
{
    [Header("Referências de Cena e Diálogo")]
    [SerializeField] private string nomeCenaUI = "Scene_UI";
    [SerializeField] private GerenciadorDialogo gd;
    [SerializeField] private DialogoDados dialogoIntroducao;
    [SerializeField] private DialogoDados dialogoEPI;

    [Header("Animação de Entrada do Sérgio")]
    [SerializeField] private GameObject objetoSergioIntro; // Canvas ou objeto pai do Sérgio na Scene_Gameplay
    [SerializeField] private RectTransform rectSergioIntro;
    [SerializeField] private Image imagemSergioIntro;
    [SerializeField] private Sprite[] spritesCaminhando;
    [SerializeField] private Sprite spriteParado;
    [SerializeField] private float intervaloFrame = 0.15f;
    [SerializeField] private float deslocamentoInicialX = -600f;
    [SerializeField] private float duracaoEntrada = 0.8f;

    private Vector2 posicaoInicialSergio;
    private Vector2 posicaoFinalSergio;
    private float tempoDecorrido = 0f;
    private float timerSprite = 0f;
    private int indexSprite = 0;

    private bool entradaConcluida = false;
    private bool aguardandoCarregarUI = false;
    private bool introducaoDisparada = false;

    private void Awake()
    {
        // Carrega a Scene_UI antes do Start() dos objetos da cena rodar
        if (!SceneManager.GetSceneByName(nomeCenaUI).isLoaded)
        {
            SceneManager.LoadScene(nomeCenaUI, LoadSceneMode.Additive);
        }
    }

    private void Start()
    {
        if (rectSergioIntro != null)
        {
            if (objetoSergioIntro != null) objetoSergioIntro.SetActive(true);
            posicaoFinalSergio = rectSergioIntro.anchoredPosition;
            posicaoInicialSergio = new Vector2(posicaoFinalSergio.x + deslocamentoInicialX, posicaoFinalSergio.y);
            rectSergioIntro.anchoredPosition = posicaoInicialSergio;
        }
        else
        {
            // Caso não haja animação configurada, carrega a UI direto
            entradaConcluida = true;
            CarregarCenaUI();
        }
    }

    private void Update()
    {
        // 1. Anima a entrada do Sérgio pela esquerda
        if (!entradaConcluida && rectSergioIntro != null)
        {
            tempoDecorrido += Time.deltaTime;
            float progresso = Mathf.Clamp01(tempoDecorrido / duracaoEntrada);

            rectSergioIntro.anchoredPosition = Vector2.Lerp(posicaoInicialSergio, posicaoFinalSergio, progresso);

            if (spritesCaminhando != null && spritesCaminhando.Length > 0 && imagemSergioIntro != null)
            {
                timerSprite += Time.deltaTime;
                if (timerSprite >= intervaloFrame)
                {
                    timerSprite = 0f;
                    indexSprite = (indexSprite + 1) % spritesCaminhando.Length;
                    imagemSergioIntro.sprite = spritesCaminhando[indexSprite];
                }
            }

            if (progresso >= 1f)
            {
                entradaConcluida = true;
                rectSergioIntro.anchoredPosition = posicaoFinalSergio;

                if (spriteParado != null && imagemSergioIntro != null)
                {
                    imagemSergioIntro.sprite = spriteParado;
                }

                CarregarCenaUI();
            }
        }

        // 2. Assim que a Scene_UI terminar de carregar, troca o avatar temporário pela UI oficial e inicia o diálogo
        if (aguardandoCarregarUI && SceneManager.GetSceneByName(nomeCenaUI).isLoaded)
        {
            aguardandoCarregarUI = false;

            if (objetoSergioIntro != null)
            {
                objetoSergioIntro.SetActive(false);
            }

            DispararDialogosIniciais();
        }
    }

    private void CarregarCenaUI()
    {
        if (!SceneManager.GetSceneByName(nomeCenaUI).isLoaded)
        {
            SceneManager.LoadSceneAsync(nomeCenaUI, LoadSceneMode.Additive);
        }

        aguardandoCarregarUI = true;
    }

    private void DispararDialogosIniciais()
    {
        if (introducaoDisparada) return;
        introducaoDisparada = true;

        if (gd == null)
        {
            gd = FindFirstObjectByType<GerenciadorDialogo>(FindObjectsInactive.Include);
        }

        if (gd != null && dialogoIntroducao != null && dialogoEPI != null)
        {
            gd.IniciarDialogoEmSequencia(dialogoIntroducao, dialogoEPI);
        }
    }
}