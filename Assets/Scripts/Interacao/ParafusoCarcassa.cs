using UnityEngine;

public class ParafusoCarcassa : MonoBehaviour, IClicavel
{
    [SerializeField] private ControleInventario inventario;
    [SerializeField] private SpriteRenderer srParafuso;
    [SerializeField] private Collider2D colParafuso;
    [SerializeField] private GerenciadorDialogo gd;
    [SerializeField] private GerenciadorAudio ga;
    [SerializeField] private ControleCarcassa controle;

    [Header("Pré-requisito de Drenagem")]
    [SerializeField] private PlugueDreno plugueDreno;

    [Header("Diálogos de Referência")]
    [SerializeField] private DialogoDados dialogoSemChave;
    [SerializeField] private DialogoDados dialogoSemDrenar; // Opcional

    private Camera cameraPrincipal;
    public bool temChave = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventario = FindFirstObjectByType<ControleInventario>(FindObjectsInactive.Include);
        gd = FindFirstObjectByType<GerenciadorDialogo>(FindObjectsInactive.Include);
        if (ga == null) ga = FindFirstObjectByType<GerenciadorAudio>(FindObjectsInactive.Include);

        if (srParafuso == null) srParafuso = GetComponent<SpriteRenderer>();
        if (colParafuso == null) colParafuso = GetComponent<Collider2D>();

        cameraPrincipal = Camera.main;
    }

    public void AoClicar()
    {
        // Bloqueia a remoção se o óleo ainda não foi drenado
        if (plugueDreno != null && !plugueDreno.EstaDrenado())
        {
            if (ga != null)
            {
                ga.TocarSomErro();
            }

            if (gd != null && dialogoSemDrenar != null)
            {
                gd.IniciarDialogo(dialogoSemDrenar);
            }
            return;
        }

        if (!temChave) TentarDesparafusar();
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
