using UnityEngine;

public class GerenciadorNavegacao : MonoBehaviour
{
    [Header("Posições da Câmera / Visões")]
    public Transform visaoBancadaPrincipal;
    public Transform visaoMesaEPI;
    public Transform visaoMesaEquipamentos;
    public Transform visaoQuadroFerramentas;

    public Transform cameraPrincipal;

    public void IrParaBancadaPrincipal()
    {
        cameraPrincipal.position = visaoBancadaPrincipal.position;
    }

    public void IrParaMesaEPI()
    {
        cameraPrincipal.position = visaoMesaEPI.position;
    }

    public void IrParaMesaEquipamentos()
    {
        cameraPrincipal.position = visaoMesaEquipamentos.position;
    }

    public void IrParaQuadroFerramentas()
    {
        cameraPrincipal.position = visaoQuadroFerramentas.position;
    }
}