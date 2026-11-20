using UnityEngine;
using UnityEngine.UI;

public class ControlePrancheta : MonoBehaviour
{
    [Header("Elementos da UI")]
    [SerializeField] private GameObject janelaPrancheta;
    [SerializeField] private Toggle toggleCorreto;

    [Header("Gerenciadores e Diálogos")]
    [SerializeField] private GerenciadorDialogo gd;
    [SerializeField] private GerenciadorAudio ga;
    [SerializeField] private DialogoDados dialogoPranchetaAcerto, 
        dialogoPranchetaErro, dialogoPranchetaAviso;
    
    public bool diagnosticoConcluido = false;
    private bool podeInteragir = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (janelaPrancheta != null) janelaPrancheta.SetActive(false);
        
    }

    public void AoMarcarOpcaoCorreta(bool marcado)
    {
        if (marcado)
        {
            diagnosticoConcluido = true;
            toggleCorreto.interactable = false;
            
            if (ga != null) ga.TocarSomAcerto();
            if (gd != null) gd.IniciarDialogo(dialogoPranchetaAcerto);
        }
    }

    public void AoMarcarOpcaoErrada(Toggle toggleClicado)
    {
        if (!diagnosticoConcluido)
        {
            if (toggleClicado.isOn)
            {
                toggleClicado.isOn = false;
                if (ga != null) ga.TocarSomErro();
                if (gd != null) gd.IniciarDialogo(dialogoPranchetaErro);
            }
        }
    }

    public void AlternarPrancheta()
    {
        if (!podeInteragir)
        {
            // if (ga != null) ga.TocarSomErro();
            if (gd != null) gd.IniciarDialogo(dialogoPranchetaAviso);
            return;
        }

        if(janelaPrancheta != null)
            janelaPrancheta.SetActive(!janelaPrancheta.activeSelf);
    }

    public void LiberarPrancheta()
    {
        podeInteragir = true;
    }
}
