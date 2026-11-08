using UnityEngine;

public class ControleCarcassa : MonoBehaviour
{
    [Header("Configurações")]
    [SerializeField] private int totalParafusos = 4; // Total de parafusos que precisam ser removidos

    [Header("Referências da Cena")]
    [SerializeField] private GerenciadorDialogo gd; // Referência ao gerenciador de diálogo para iniciar diálogos quando todos os parafusos forem removidos
    //[SerializeField] private SpriteRenderer srCarcassaFechada;
    //[SerializeField] private SpriteRenderer srCarcassaAberta;

    [Header("Diálogos")]
    [SerializeField] private DialogoDados dialogoCarcassaAberta; // Diálogo de sucesso quando todos os parafusos são removidos

    private int parafusosRemovidos = 0; // Contador de parafusos removidos
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(gd == null) gd = FindFirstObjectByType<GerenciadorDialogo>(FindObjectsInactive.Include);
        //srCarcassaFechada.enabled = true; // Mostra a carcassa fechada inicialmente
        //srCarcassaAberta.enabled = false; // Esconde a carcassa aberta inicialmente
    }

    public void ParafusoRemovido()
    {
        parafusosRemovidos++;
        if(parafusosRemovidos >= totalParafusos)
        {
            //if(srCarcassaFechada != null) srCarcassaFechada.enabled = false; // Esconde a carcassa fechada
            //if(srCarcassaAberta != null) srCarcassaAberta.enabled = true; // Mostra a carcassa aberta
            if(gd != null && dialogoCarcassaAberta != null)
            {
                gd.IniciarDialogo(dialogoCarcassaAberta); // Inicia o diálogo de sucesso
            }
            // Aqui você pode adicionar a lógica que deve ocorrer quando o parafuso é removido.
            // Por exemplo, você pode abrir a carcassa ou ativar algum outro objeto.
            Debug.Log("Todos os parafusos removidos! A carcaça pode ser aberta agora.");
        }
    }
}
