using UnityEngine;
using UnityEngine.UI;

public class BotoesGiroUI : MonoBehaviour
{
    [Header("Botões Dedicados ao Giro do Redutor")]
    [SerializeField] private Button botaoGirarEsquerda;
    [SerializeField] private Button botaoGirarDireita;

    private ControladorVistasRedutor controladorRedutor;

    private void Start()
    {
        // Encontra o controlador do Redutor na Scene_Gameplay
        controladorRedutor = FindFirstObjectByType<ControladorVistasRedutor>(FindObjectsInactive.Include);

        if (botaoGirarEsquerda != null)
        {
            botaoGirarEsquerda.onClick.AddListener(ClicarEsquerda);
        }

        if (botaoGirarDireita != null)
        {
            botaoGirarDireita.onClick.AddListener(ClicarDireita);
        }
    }

    private void ClicarEsquerda()
    {
        if (controladorRedutor == null)
            controladorRedutor = FindFirstObjectByType<ControladorVistasRedutor>(FindObjectsInactive.Include);

        if (controladorRedutor != null)
            controladorRedutor.GirarEsquerda();
    }

    private void ClicarDireita()
    {
        if (controladorRedutor == null)
            controladorRedutor = FindFirstObjectByType<ControladorVistasRedutor>(FindObjectsInactive.Include);

        if (controladorRedutor != null)
            controladorRedutor.GirarDireita();
    }
}
