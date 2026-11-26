using UnityEngine;

public class ControleCarcassa : MonoBehaviour, IClicavel
{
    [Header("Configurações")]
    [SerializeField] private int totalParafusos = 4; // Total de parafusos que precisam ser removidos

    [Header("Referências da Cena")]
    [SerializeField] private GerenciadorDialogo gd; // Referência ao gerenciador de diálogo para iniciar diálogos quando todos os parafusos forem removidos
    [SerializeField] private GerenciadorAudio ga; // Referência ao gerenciador de áudio para tocar sons quando todos os parafusos forem removidos
    //[SerializeField] private SpriteRenderer srCarcassaFechada;
    //[SerializeField] private SpriteRenderer srCarcassaAberta;

    [Header("Diálogos")]
    [SerializeField] private DialogoDados dialogoCarcassaAberta; // Diálogo de sucesso quando todos os parafusos são removidos

    [Header("Fechamento")]
    [SerializeField] private DialogoDados dialogoCarcassaFechada; // Diálogo de sucesso quando a carcassa é fechada
    private Collider2D colisorCarcassa; // Colisor da carcassa para permitir que o jogador clique nela para fechá-la
    public bool carcassaAberta = false; // Estado da carcassa, se está aberta ou fechada
    private bool prontoParaFechar = false; // Estado que indica se a carcassa pode ser fechada

    private int parafusosRemovidos = 0; // Contador de parafusos removidos

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BuscarReferencias();
        colisorCarcassa = GetComponent<Collider2D>();
        if (colisorCarcassa != null) colisorCarcassa.enabled = false; // Desativa o colisor inicialmente)
        //srCarcassaFechada.enabled = true; // Mostra a carcassa fechada inicialmente
        //srCarcassaAberta.enabled = false; // Esconde a carcassa aberta inicialmente
    }

    public void AoClicar()
    {
        if (!prontoParaFechar) return;

        // Lógica para fechar a carcassa
        carcassaAberta = false;
        prontoParaFechar = false;
        if(colisorCarcassa != null) colisorCarcassa.enabled = false; // Desativa o colisor para evitar múltiplos cliques)

        ga.TocarSomAcerto(); // Toca o som de sucesso
        gd.IniciarDialogo(dialogoCarcassaFechada); // Inicia o diálogo de sucesso
<<<<<<< Updated upstream:Assets/Scripts/Inventario/ControleCarcassa.cs

=======
        cvr.DefinirEstadoVista("Superior", false);

        BotoeiraTeste botoeira = FindFirstObjectByType<BotoeiraTeste>(FindObjectsInactive.Include);
        if (botoeira != null) botoeira.LiberarBotoeira();
>>>>>>> Stashed changes:Assets/Scripts/Interacao/ControleCarcassa.cs
    }

    private void BuscarReferencias()
    {
        if (gd == null) gd = FindFirstObjectByType<GerenciadorDialogo>(FindObjectsInactive.Include);
        if (ga == null) ga = FindFirstObjectByType<GerenciadorAudio>(FindObjectsInactive.Include);
        //if (srCarcassaFechada == null) srCarcassaFechada = GetComponent<SpriteRenderer>();
        //if (srCarcassaAberta == null) srCarcassaAberta = GetComponent<SpriteRenderer>();
    }

    public void ParafusoRemovido()
    {
        parafusosRemovidos++;
        carcassaAberta = parafusosRemovidos >= totalParafusos;
        if (parafusosRemovidos >= totalParafusos)
        {
            //if(srCarcassaFechada != null) srCarcassaFechada.enabled = false; // Esconde a carcassa fechada
            //if(srCarcassaAberta != null) srCarcassaAberta.enabled = true; // Mostra a carcassa aberta
            if (gd != null && ga != null && dialogoCarcassaAberta != null)
            {
                ga.TocarSomAcerto(); // Toca o som de sucesso
                gd.IniciarDialogo(dialogoCarcassaAberta); // Inicia o diálogo de sucesso
            }
            // Aqui você pode adicionar a lógica que deve ocorrer quando o parafuso é removido.
            // Por exemplo, você pode abrir a carcassa ou ativar algum outro objeto.
            ControlePrancheta cp = FindFirstObjectByType<ControlePrancheta>(FindObjectsInactive.Include);
            if (cp != null) cp.LiberarPrancheta();
            Debug.Log("Todos os parafusos removidos! A carcaça pode ser aberta agora.");
        }
    }

    public void LiberarFechamento()
    {
        colisorCarcassa.enabled = true; // Ativa o colisor para permitir que o jogador clique na carcaça
        prontoParaFechar = true;
    }
}
