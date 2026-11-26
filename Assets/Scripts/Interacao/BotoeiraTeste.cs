using UnityEngine;

public class BotoeiraTeste : MonoBehaviour, IClicavel
{
    [Header("Configurações Visuais (Opcional)")]
    [SerializeField] private SpriteRenderer spriteChave;
    [SerializeField] private Sprite spriteChaveLigada;

    [Header("Referências")]
    [SerializeField] private GerenciadorAudio gerenciadorAudio;
    [SerializeField] private GerenciadorDialogo gerenciadorDialogo;
    [SerializeField] private DialogoDados dialogoChaveBloqueada;

    private PainelTesteOperacional painelTeste;
    private bool prontaParaTestar = false;
    private bool testeIniciado = false;

    private void Start()
    {
        if (gerenciadorAudio == null)
            gerenciadorAudio = FindFirstObjectByType<GerenciadorAudio>(FindObjectsInactive.Include);

        if (gerenciadorDialogo == null)
            gerenciadorDialogo = FindFirstObjectByType<GerenciadorDialogo>(FindObjectsInactive.Include);

        painelTeste = FindFirstObjectByType<PainelTesteOperacional>(FindObjectsInactive.Include);
    }

    // Chame este método ao concluir o fechamento da carcaça (ou após a lubrificação)
    public void LiberarBotoeira()
    {
        prontaParaTestar = true;
    }

    public void AoClicar()
    {
        if (testeIniciado) return;

        if (!prontaParaTestar)
        {
            if (gerenciadorAudio != null) gerenciadorAudio.TocarSomErro();
            if (gerenciadorDialogo != null && dialogoChaveBloqueada != null)
                gerenciadorDialogo.IniciarDialogo(dialogoChaveBloqueada);
            return;
        }

        testeIniciado = true;

        if (spriteChave != null && spriteChaveLigada != null)
            spriteChave.sprite = spriteChaveLigada;

        if (gerenciadorAudio != null)
            gerenciadorAudio.TocarSomAcerto();

        if (painelTeste == null)
            painelTeste = FindFirstObjectByType<PainelTesteOperacional>(FindObjectsInactive.Include);

        if (painelTeste != null)
            painelTeste.IniciarTesteOperacional();
    }
}