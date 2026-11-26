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

    [Header("Diálogos Orientativos de Vista do Redutor")]
    [SerializeField] private int etapaPosicionarBandeja = 3;
    [SerializeField] private DialogoDados dialogoOrientacaoVistaBandeja;
    [SerializeField] private int etapaDesparafusarCarcaca = 4;
    [SerializeField] private DialogoDados dialogoOrientacaoVistaParafusos;

    [Header("Botões e Vistas Travados Antes dos EPIs")]
    [SerializeField] private Button botaoCima;
    [SerializeField] private Button botaoDireita;
    [SerializeField] private Button botaoVoltar;
    [SerializeField] private ControladorVistasRedutor cvr;

    [Header("Controle das Setas de Giro do Redutor")]
    [SerializeField] private BotoesGiroUI botoesGiroUI;

    public Transform cameraPrincipal;

    private bool jaOrientouVistaBandeja = false;
    private bool jaOrientouVistaParafusos = false;

    void Start()
    {
        if(cvr == null)
        {
            cvr = FindFirstObjectByType<ControladorVistasRedutor>(FindObjectsInactive.Include);
        }

        if (cameraPrincipal == null && Camera.main != null)
        {
            cameraPrincipal = Camera.main.transform;
        }

        if (botoesGiroUI == null)
        {
            botoesGiroUI = FindFirstObjectByType<BotoesGiroUI>(FindObjectsInactive.Include);
        }

        TravarNavegacaoInicial();

        // Começa na visão principal, então as setas ficam visíveis
        if (botoesGiroUI != null) botoesGiroUI.DefinirVisibilidadeSetas(true);
    }

    public void IrParaBancadaPrincipal()
    {
        if (gerenciadorEPI != null && !gerenciadorEPI.TodosEPIsColetados())
        {
            if (gerenciadorDialogo != null && dialogoFaltamEPIs != null)
            {
                gerenciadorDialogo.IniciarDialogo(dialogoFaltamEPIs);
            }
            return;
        }

        MoverCamera(visaoBancadaPrincipal);
        VerificarDialogoOrientativoRedutor();
    }

    private void VerificarDialogoOrientativoRedutor()
    {
        if (gerenciadorDialogo == null) return;

        int etapaAtual = gerenciadorDialogo.RetornarValorDeEtapa();

        if (etapaAtual == etapaPosicionarBandeja && !jaOrientouVistaBandeja && dialogoOrientacaoVistaBandeja != null)
        {
            jaOrientouVistaBandeja = true;
            cvr.DefinirEstadoVista("Traseira", true);
            gerenciadorDialogo.IniciarDialogo(dialogoOrientacaoVistaBandeja);
        }
        else if (etapaAtual == etapaDesparafusarCarcaca && !jaOrientouVistaParafusos && dialogoOrientacaoVistaParafusos != null)
        {
            jaOrientouVistaParafusos = true;
            gerenciadorDialogo.IniciarDialogo(dialogoOrientacaoVistaParafusos);
        }
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
            if (cameraPrincipal == null && Camera.main != null)
            {
                cameraPrincipal = Camera.main.transform;
            }

            if (cameraPrincipal != null)
            {
                cameraPrincipal.position = new Vector3(destino.position.x, destino.position.y, -10f);
            }

            // Mostra as setas de giro APENAS se estiver na bancada principal do Redutor
            if (botoesGiroUI != null)
            {
                bool estaNaBancadaPrincipal = (destino == visaoBancadaPrincipal);
                botoesGiroUI.DefinirVisibilidadeSetas(estaNaBancadaPrincipal);
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
