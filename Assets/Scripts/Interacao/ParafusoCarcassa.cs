using UnityEngine;
using UnityEngine.InputSystem;

public class ParafusoCarcassa : MonoBehaviour
{
    [SerializeField] private ControleInventario inventario;
    [SerializeField] private SpriteRenderer srParafuso;
    [SerializeField] private Collider2D colParafuso;
    [SerializeField] private GerenciadorDialogo gd;
    [SerializeField] private ControleCarcassa controle;

    [Header("Diálogos de Referência")]
    [SerializeField] private DialogoDados dialogoSemChave;

    private Camera cameraPrincipal;
    public bool temChave = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventario = FindFirstObjectByType<ControleInventario>(FindObjectsInactive.Include);
        gd = FindFirstObjectByType<GerenciadorDialogo>(FindObjectsInactive.Include);

        if (srParafuso == null) srParafuso = GetComponent<SpriteRenderer>();
        if (colParafuso == null) colParafuso = GetComponent<Collider2D>();

        cameraPrincipal = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
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
            TentarDesparafusar();
        }
    }

    private void TentarDesparafusar()
    {
        // Checa se o jogador tem a chave no inventário
        if (inventario != null && inventario.TemItem("Chave Allen"))
        {
            temChave = true;
            colParafuso.enabled = false; // Desativa o collider do parafuso
            srParafuso.enabled = false; // Esconde o parafuso

            if(controle != null)
            {
                controle.ParafusoRemovido(); // Notifica o controle da carcassa que o parafuso foi removido
            }
        }
        else
        {
            // Se não tiver a chave, mostra um diálogo de aviso
            if (gd != null && dialogoSemChave != null)
            {
                gd.IniciarDialogo(dialogoSemChave);
            }
        }
    }
}
