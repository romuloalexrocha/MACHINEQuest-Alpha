using System;
using UnityEngine;

public class EncaixeEngrenagem : MonoBehaviour, IClicavel
{
    [Header("Referências da Cena")]
    [SerializeField] private ControleCarcassa cc;
    [SerializeField] private PontoLubrificacao pl;
    [SerializeField] private ControlePrancheta cp;
    [SerializeField] private ControleInventario ci;
    [SerializeField] private GerenciadorDialogo gd;
    [SerializeField] private GerenciadorAudio ga;

    [Header("Referências da Engrenagem")]
    [SerializeField] private GameObject visualEngrQuebrada, visualEngrNova;
    [SerializeField] private string nomeEngrCorreta = "Engrenagem_20D";
    [SerializeField] private string[] nomesEngrErradas = { "Engrenagem_14D", "Engrenagem_28D" };
    private Collider2D colisorEngrenagem;

    [Header("Diálogos")]
    [SerializeField]
    private DialogoDados dialogoBloqueado, dialogoQuebradaRemovida,
        dialogoEixoVazio, dialogoEngrErrada, dialogoEngrEncaixada;

    private bool engrNovaEncaixada = false; // Variável para controlar se a engrenagem já foi encaixada
    private bool engrQuebradaRemovida = false; // Variável para controlar se a engrenagem quebrada já foi coletada

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        colisorEngrenagem = GetComponent<Collider2D>();

        BuscarReferencias();

        if (visualEngrQuebrada != null) visualEngrQuebrada.SetActive(true);
        if (visualEngrNova != null) visualEngrNova.SetActive(false);
    }

    private void BuscarReferencias()
    {
        if (cc == null) cc = FindFirstObjectByType<ControleCarcassa>(FindObjectsInactive.Include);
        if (pl == null) pl = FindFirstObjectByType<PontoLubrificacao>(FindObjectsInactive.Include);
        if (cp == null) cp = FindFirstObjectByType<ControlePrancheta>(FindObjectsInactive.Include);
        if (ci == null) ci = FindFirstObjectByType<ControleInventario>(FindObjectsInactive.Include);
        if (gd == null) gd = FindFirstObjectByType<GerenciadorDialogo>(FindObjectsInactive.Include);
        if (ga == null) ga = FindFirstObjectByType<GerenciadorAudio>(FindObjectsInactive.Include);
    }

    public void AoClicar()
    {
        // 1. Se já finalizou a troca, não faz mais nada
        if (engrNovaEncaixada) return;

        // 2. Trava de segurança (carcaça aberta e diagnóstico concluído)
        if (cc == null || !cc.carcassaAberta ||
            cp == null || !cp.diagnosticoConcluido)
        {
            if (gd != null && dialogoBloqueado != null) gd.IniciarDialogo(dialogoBloqueado);
            return;
        }

        // 3. Remove a engrenagem quebrada
        if (!engrQuebradaRemovida)
        {
            engrQuebradaRemovida = true;
            if (visualEngrQuebrada != null) visualEngrQuebrada.SetActive(false);

            if (ga != null) ga.TocarSomAcerto();
            if (gd != null && dialogoQuebradaRemovida != null) gd.IniciarDialogo(dialogoQuebradaRemovida);
            return;
        }

        // 4. Tenta encaixar a nova engrenagem a partir do inventário
        if (ci == null) return;

        if (ci.EstaComItemEquipado(nomeEngrCorreta))
        {
            if (ci != null) ci.RemoverItem(nomeEngrCorreta);
            engrNovaEncaixada = true;
            if (colisorEngrenagem != null) colisorEngrenagem.enabled = false; // Desativa o colisor para evitar cliques adicionais)
            if (pl != null) pl.LiberarLubrificacao();

            if (visualEngrNova != null) visualEngrNova.SetActive(true);
            if (ga != null) ga.TocarSomAcerto();
            if (gd != null && dialogoEngrEncaixada != null) gd.IniciarDialogo(dialogoEngrEncaixada);
            return;
        }

        foreach (string nomeErrado in nomesEngrErradas)
        {
            if (ci.EstaComItemEquipado(nomeErrado))
            {
                if (ga != null) ga.TocarSomErro();
                if (gd != null && dialogoEngrErrada != null) gd.IniciarDialogo(dialogoEngrErrada);
                return;
            }
        }

        // 5. Eixo vazio e nenhuma engrenagem no inventário
        if (gd != null && dialogoEixoVazio != null) gd.IniciarDialogo(dialogoEixoVazio);
    }
}
