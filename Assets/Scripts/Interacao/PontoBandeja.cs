using UnityEngine;
using UnityEngine.InputSystem;

public class PontoBandeja : MonoBehaviour
{
    [SerializeField] private ControleInventario inventario;
    [SerializeField] private SpriteRenderer srBandeja;
    [SerializeField] private SpriteRenderer srPonto;
    [SerializeField] private GerenciadorDialogo gd;

    [Header("Diálogos de Referência")]
    [SerializeField] private DialogoDados dialogoSemBandeja, dialogoComBandeja;

    private Camera cameraPrincipal;
    public bool bandejaPosicionada = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventario = FindFirstObjectByType<ControleInventario>(FindObjectsInactive.Include);
        gd = FindFirstObjectByType<GerenciadorDialogo>(FindObjectsInactive.Include);

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

    // Update is called once per frame
    void Update()
    {
        if (bandejaPosicionada) return; // Se a bandeja já estiver posicionada, não faz nada

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            VerificarClique();
        }
    }

    private void VerificarClique()
    {
        if (cameraPrincipal == null) cameraPrincipal = Camera.main;

        Vector2 posicaoMouse = Mouse.current.position.ReadValue();
        Vector3 posicaoMundo3D = cameraPrincipal.ScreenToWorldPoint(new Vector3(posicaoMouse.x, posicaoMouse.y, cameraPrincipal.nearClipPlane));
        Vector2 posicaoMundo = new Vector2(posicaoMundo3D.x, posicaoMundo3D.y);

        RaycastHit2D hit = Physics2D.Raycast(posicaoMundo, Vector2.zero);

        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            TentarPosicionarBandeja();
        }
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

            // Chama o diálogo de sucesso do Sérgio se tiver configurado
            if (gd != null && dialogoComBandeja != null)
            {
                gd.IniciarDialogo(dialogoComBandeja);
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
