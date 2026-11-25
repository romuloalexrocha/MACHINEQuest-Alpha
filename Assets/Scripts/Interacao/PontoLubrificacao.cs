using UnityEngine;

public class PontoLubrificacao : MonoBehaviour, IClicavel
{
    [Header("Referências")]
    [SerializeField] private GerenciadorAudio ga;
    [SerializeField] private GerenciadorDialogo gd;
    [SerializeField] private ControleInventario ci;
    [SerializeField] private MiniGameLubrificacao miniGame;
    private Collider2D colisorPonto;

    [Header("Diálogos")]
    [SerializeField] private DialogoDados dialogoAvisoTroca;
    [SerializeField] private DialogoDados dialogoErroLubrificante;
    [SerializeField] private DialogoDados dialogoSemLubrificante;
    [SerializeField] private DialogoDados dialogoLubrConcluida;

    private bool lubrificacaoConcluida = false;
    private bool liberado = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        colisorPonto = GetComponent<Collider2D>();
        if(colisorPonto != null) colisorPonto.enabled = false; // Desativa o colisor inicialmente

        BuscarReferencias();

        miniGame = FindFirstObjectByType<MiniGameLubrificacao>(FindObjectsInactive.Include);
        if (miniGame != null) miniGame.gameObject.SetActive(false);
    }

    private void BuscarReferencias()
    {
        if (ga == null) ga = FindFirstObjectByType<GerenciadorAudio>(FindObjectsInactive.Include);
        if (gd == null) gd = FindFirstObjectByType<GerenciadorDialogo>(FindObjectsInactive.Include);
        if (ci == null) ci = FindFirstObjectByType<ControleInventario>(FindObjectsInactive.Include);
    }

    public void AoClicar()
    {
        if (lubrificacaoConcluida) return;

        if (!liberado)
        {
            ga.TocarSomErro();
            gd.IniciarDialogo(dialogoAvisoTroca);
            return;
        }

        if(ci == null) return;
        else if (ci.EstaComItemEquipado("Oleo_ISO_VG_220"))
        {
            ga.TocarSomAcerto();
            miniGame.gameObject.SetActive(true);
        }
        else if (ci.EstaComItemEquipado("Oleo_Hidraulico") || ci.EstaComItemEquipado("Graxa_Litio"))
        {
            ga.TocarSomErro();
            gd.IniciarDialogo(dialogoErroLubrificante);
        }
        else gd.IniciarDialogo(dialogoSemLubrificante);
        
    }

    public void LiberarLubrificacao()
    {
        liberado = true;
        if(colisorPonto != null) colisorPonto.enabled = true; // Ativa o colisor para permitir cliques
    }

    public void ConcluirLubrificacao()
    {
        if (ci != null) ci.RemoverItem("Oleo_ISO_VG_220");
        if (ga != null) ga.TocarSomAcerto();
        if (gd != null) gd.IniciarDialogo(dialogoLubrConcluida);
        lubrificacaoConcluida = true;
        if(colisorPonto != null) colisorPonto.enabled = false; // Desativa o colisor para evitar cliques adicionais
    }
}
