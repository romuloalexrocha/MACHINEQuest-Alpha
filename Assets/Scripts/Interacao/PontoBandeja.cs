using UnityEngine;

public class PontoBandeja : MonoBehaviour, IClicavel
{
    [SerializeField] private ControleInventario inventario;
    [SerializeField] private SpriteRenderer srBandeja;
    [SerializeField] private SpriteRenderer srPonto;
    [SerializeField] private GerenciadorDialogo gd;
    [SerializeField] private GerenciadorAudio ga;
    [SerializeField] private string nomeItemBandeja = "Bandeja"; // Mesmo nome usado no ItemColetavel!

    [Header("Diálogos de Referência")]
    [SerializeField] private DialogoDados dialogoSemBandeja, dialogoComBandeja;

    private Camera cameraPrincipal;
    public bool bandejaPosicionada = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventario = FindFirstObjectByType<ControleInventario>(FindObjectsInactive.Include);
        gd = FindFirstObjectByType<GerenciadorDialogo>(FindObjectsInactive.Include);
        ga = FindFirstObjectByType<GerenciadorAudio>(FindObjectsInactive.Include);

        // Pega o SpriteRenderer do próprio objeto se não foi arrastado
        if (srPonto == null) srPonto = GetComponent<SpriteRenderer>();

        // Copia a imagem do item da mesa e desativa a visualização no dreno
        if (srBandeja != null && srPonto != null)
        {
            srPonto.sprite = srBandeja.sprite;
            srPonto.color = srBandeja.color;
            srPonto.enabled = false;
        }
        cameraPrincipal = Camera.main;
    }

    public void AoClicar()
    {
        if (!bandejaPosicionada) TentarPosicionarBandeja();
    }

    private void TentarPosicionarBandeja()
    {
        // 1. Verifica se o item "Bandeja" está no inventário
        if (inventario != null && inventario.TemItem("Bandeja"))
        {
            bandejaPosicionada = true;

            if (srPonto != null)
            {
                srPonto.enabled = true; // Exibe a imagem sob o dreno!
            }

            if (ga != null) ga.TocarSomAcerto();

            // Chama o diálogo de sucesso do Sérgio se tiver configurado
            if (gd != null && dialogoComBandeja != null)
            {
                gd.IniciarDialogo(dialogoComBandeja);
            }

            // Remove a bandeja da gaveta vertical da UI
            if (inventario != null)
            {
                inventario.RemoverItem(nomeItemBandeja);
            }

            // Desativa o colisor do PontoBandeja para evitar cliques repetidos
            Collider2D colisor = GetComponent<Collider2D>();
            if (colisor != null)
            {
                colisor.enabled = false;
            }
        }
        else
        {
            // Jogador não tem a bandeja: Sérgio avisa!
            if (gd != null && dialogoSemBandeja != null)
            {
                gd.IniciarDialogo(dialogoSemBandeja);
            }
        }
    }
}
