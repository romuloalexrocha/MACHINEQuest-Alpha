using UnityEngine;

public class GerenciadorNavegacao : MonoBehaviour
{
    [Header("Posições da Câmera / Visões")]
    public Transform visaoBancadaPrincipal;
    public Transform visaoMesaEPI;
    public Transform visaoMesaEquipamentos;
    public Transform visaoQuadroFerramentas;

    public Transform cameraPrincipal;

    void Start()
    {
        if (cameraPrincipal == null && Camera.main != null)
        {
            cameraPrincipal = Camera.main.transform;
        }
    }

    public void IrParaBancadaPrincipal()
    {
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
}