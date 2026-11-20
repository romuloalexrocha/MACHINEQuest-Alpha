using UnityEngine;
using UnityEngine.UI;

public class GerenciadorNavegacao : MonoBehaviour
{
    [Header("Posições da Câmera / Visões")]
    public Transform visaoBancadaPrincipal;
    public Transform visaoMesaEPI;
    public Transform visaoMesaEquipamentos;
    public Transform visaoQuadroFerramentas;

    [Header("Trava de EPIs")]
    [SerializeField] private GerenciadorEPI gerenciadorEPI;
    [SerializeField] private GerenciadorDialogo gerenciadorDialogo;
    [SerializeField] private DialogoDados dialogoFaltamEPIs;

    [Header("Botões Travados Antes dos EPIs")]
    [SerializeField] private Button botaoCima;
    [SerializeField] private Button botaoDireita;
    [SerializeField] private Button botaoVoltar;

    public Transform cameraPrincipal;

    void Start()
    {
        if (cameraPrincipal == null && Camera.main != null)
        {
            cameraPrincipal = Camera.main.transform;
        }

        TravarNavegacaoInicial();
    }

    public void IrParaBancadaPrincipal()
    {
        // Bloqueia o retorno caso ainda não tenha coletado os 5 EPIs
        if (gerenciadorEPI != null && !gerenciadorEPI.TodosEPIsColetados())
        {
            if (gerenciadorDialogo != null && dialogoFaltamEPIs != null)
            {
                gerenciadorDialogo.IniciarDialogo(dialogoFaltamEPIs);
            }
            return; // Interrompe aqui e não move a câmera!
        }

        MoverCamera(visaoBancadaPrincipal);
    }

    public void IrParaMesaEPI()
    {
        MoverCamera(visaoMesaEPI);
    }

    public void IrParaMesaEquipamentos()
    {
        MoverCamera(visaoMesaEquipamentos);
    }

    public void IrParaQuadroFerramentas()
    {
        MoverCamera(visaoQuadroFerramentas);
    }

    private void MoverCamera(Transform destino)
    {
        if (destino != null)
        {
            // Pega a Câmera principal da Gameplay caso a referência não esteja manual
            if (cameraPrincipal == null && Camera.main != null)
            {
                cameraPrincipal = Camera.main.transform;
            }

            if (cameraPrincipal != null)
            {
                // Move X e Y para o destino, mas TRAVA o Z em -10
                cameraPrincipal.position = new Vector3(destino.position.x, destino.position.y, -10f);
            }
        }
    }

    public void TravarNavegacaoInicial()
    {
        if (botaoCima != null) botaoCima.interactable = false;
        if (botaoDireita != null) botaoDireita.interactable = false;
        if (botaoVoltar != null) botaoVoltar.interactable = false;
    }

    public void LiberarNavegacaoGeral()
    {
        if (botaoCima != null) botaoCima.interactable = true;
        if (botaoDireita != null) botaoDireita.interactable = true;
        if (botaoVoltar != null) botaoVoltar.interactable = true;
    }
}