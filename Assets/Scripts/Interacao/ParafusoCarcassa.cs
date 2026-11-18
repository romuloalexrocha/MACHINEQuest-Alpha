using UnityEngine;

public class ParafusoCarcassa : MonoBehaviour, IClicavel
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

    public void AoClicar()
    {
        if(!temChave) TentarDesparafusar();
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
