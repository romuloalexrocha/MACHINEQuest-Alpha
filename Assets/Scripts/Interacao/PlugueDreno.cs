using UnityEngine;
using UnityEngine.InputSystem;

public class PlugueDreno : MonoBehaviour
{
    [Header("Referências da Cena")]
    [SerializeField] private PontoBandeja pontoBandeja;       // Para checar se a bandeja está no lugar
    [SerializeField] private GerenciadorDialogo gd;          // Para disparar as falas do Sérgio
    [SerializeField] private SpriteRenderer srPlugue;         // Para esconder o plugue ao tirar

    [Header("Diálogos de Referência")]
    [SerializeField] private DialogoDados dialogoSemBandeja;  // Aviso: "Coloque a bandeja antes de abrir!"
    [SerializeField] private DialogoDados dialogoDrenouOleo;  // Sucesso: "Óleo drenado com sucesso!"

    private Camera cameraPrincipal;
    public bool drenoAberto = false;                         // Controle se o dreno já foi aberto

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Busca referências na cena se não foram arrastadas no Inspector
        if (pontoBandeja == null)
            pontoBandeja = FindFirstObjectByType<PontoBandeja>(FindObjectsInactive.Include);

        if (gd == null)
            gd = FindFirstObjectByType<GerenciadorDialogo>(FindObjectsInactive.Include);

        if (srPlugue == null)
            srPlugue = GetComponent<SpriteRenderer>();

        cameraPrincipal = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // Se o dreno já foi aberto, ignora novos cliques (Early Return)
        if (drenoAberto) return;

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
            TentarAbrirDreno();
        }
    }

    private void TentarAbrirDreno()
    {
        // Checa se a referência do PontoBandeja existe e se a bandeja já foi posicionada lá
        if (pontoBandeja != null && pontoBandeja.bandejaPosicionada)
        {
            drenoAberto = true;

            // Esconde o plugue (simulando que foi retirado)
            if (srPlugue != null)
            {
                srPlugue.enabled = false;
            }

            // Fala de sucesso do Sérgio
            if (gd != null && dialogoDrenouOleo != null)
            {
                gd.IniciarDialogo(dialogoDrenouOleo);
            }
        }
        else
        {
            // Fala de aviso: precisa colocar a bandeja primeiro!
            if (gd != null && dialogoSemBandeja != null)
            {
                gd.IniciarDialogo(dialogoSemBandeja);
            }
        }
    }
}
