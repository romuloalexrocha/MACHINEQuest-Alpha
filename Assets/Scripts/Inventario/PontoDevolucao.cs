using UnityEngine;

public class PontoDevolucao : MonoBehaviour, IClicavel
{
    [Header("Configuração do Item")]
    [Tooltip("Nome exato do item esperado neste local (ex: Item_ChaveAllen)")]
    [SerializeField] private string nomeItemEsperado;

    [Tooltip("Arraste o GameObject original da cena que foi desativado na coleta")]
    [SerializeField] private GameObject objetoOriginalNaCena;

    [Header("Referências da Cena")]
    [SerializeField] private ControleInventario ci;
    // [SerializeField] private GerenciadorDialogo gd;
    // [SerializeField] private GerenciadorAudio ga;

    //[Header("Feedbacks (Opcionais)")]
    //[SerializeField] private DialogoDados dialogoItemErrado;
    //[SerializeField] private DialogoDados dialogoItemDevolvido;

    private void Start()
    {
        BuscarReferencias();
    }

    private void BuscarReferencias()
    {
        if (ci == null) ci = FindFirstObjectByType<ControleInventario>(FindObjectsInactive.Include);
        //if (gd == null) gd = FindFirstObjectByType<GerenciadorDialogo>(FindObjectsInactive.Include);
        // if (ga == null) ga = FindFirstObjectByType<GerenciadorAudio>(FindObjectsInactive.Include);
    }

    public void AoClicar()
    {
        if (ci == null) return;

        string itemNaMao = ci.ObterItemEquipado();

        // Se o jogador clicou no ponto sem estar segurando nada no mouse, ignora
        if (string.IsNullOrEmpty(itemNaMao)) return;

        // Verifica se o item na mão corresponde ao espaço clicado
        if (itemNaMao == nomeItemEsperado)
        {
            // Remove do inventário (isso também limpa o ícone do cursor automaticamente)
            ci.RemoverItem(nomeItemEsperado);

            // Reativa o objeto original no local de origem
            if (objetoOriginalNaCena != null)
                objetoOriginalNaCena.SetActive(true);

            // if (ga != null) ga.TocarSomAcerto();
            // if (gd != null && dialogoItemDevolvido != null) gd.IniciarDialogo(dialogoItemDevolvido);
        }
        else
        {
            // O jogador tentou devolver a ferramenta no lugar errado
            // if (ga != null) ga.TocarSomErro();
            // if (gd != null && dialogoItemErrado != null) gd.IniciarDialogo(dialogoItemErrado);
        }
    }
}