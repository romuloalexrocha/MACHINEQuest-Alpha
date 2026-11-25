using UnityEngine;

public class PlugueDreno : MonoBehaviour, IClicavel
{
    [Header("Referências da Cena")]
    [SerializeField] private PontoBandeja pontoBandeja;       // Para checar se a bandeja está no lugar
    [SerializeField] private GerenciadorDialogo gd;          // Para disparar as falas do Sérgio
    [SerializeField] private SpriteRenderer srPlugue;         // Para esconder o plugue ao tirar

    [Header("Diálogos de Referência")]
    [SerializeField] private DialogoDados dialogoSemBandeja;  // Aviso: "Coloque a bandeja antes de abrir!"
    [SerializeField] private DialogoDados dialogoDrenouOleo;  // Sucesso: "Óleo drenado com sucesso!"
    [SerializeField] private DialogoDados dialogoCarcassa;      // Para o diálogo da desmontagem da carcaça
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
    }

    public void AoClicar()
    {
        if(!drenoAberto) TentarAbrirDreno();
    }

    public bool EstaDrenado()
    {
        return drenoAberto;
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
            if (gd != null && dialogoDrenouOleo != null && dialogoCarcassa != null)
            {
                gd.IniciarDialogoEmSequencia(dialogoDrenouOleo, dialogoCarcassa);
            }

            // Desativa o colisor do PlugueDreno para evitar cliques repetidos
            Collider2D colisor = GetComponent<Collider2D>();
            if (colisor != null)
            {
                colisor.enabled = false;
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
