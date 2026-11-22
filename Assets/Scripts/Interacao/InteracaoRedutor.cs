using UnityEngine;

public class InteracaoRedutor : MonoBehaviour, IClicavel
{
    [Header("Referências de Diálogo")]
    [SerializeField] private GerenciadorDialogo gd;
    [SerializeField] private DialogoDados dialogoInspecaoRedutor; // Opcional: diálogo ao clicar no redutor durante o jogo

    private Collider2D colisor2D;
    private bool jaInteragiu = false;

    private void Start()
    {
        colisor2D = GetComponent<Collider2D>();
        if (colisor2D == null)
        {
            Debug.LogError("O objeto " + gameObject.name + " não possui um Collider2D.");
        }
    }

    public void AoClicar()
    {
        if (jaInteragiu) return;
        jaInteragiu = true;

        if (gd == null)
        {
            gd = FindFirstObjectByType<GerenciadorDialogo>(FindObjectsInactive.Include);
        }

        if (gd != null && dialogoInspecaoRedutor != null)
        {
            gd.IniciarDialogo(dialogoInspecaoRedutor);
        }

        // Desativa o collider do Redutor para liberar o clique nos componentes internos (parafusos, carcaça, etc.)
        if (colisor2D != null)
        {
            colisor2D.enabled = false;
        }
    }

    public void LiberarNovaInteracao()
    {
        jaInteragiu = false;
        if (colisor2D != null) colisor2D.enabled = true;
    }
}
