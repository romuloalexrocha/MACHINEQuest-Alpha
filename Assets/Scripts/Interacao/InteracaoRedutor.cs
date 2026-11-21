using UnityEngine;
using UnityEngine.SceneManagement;

public class InteracaoRedutor : MonoBehaviour, IClicavel
{
    [Header("Referências de Diálogo")]
    [SerializeField] private GerenciadorDialogo gd;
    [SerializeField] private DialogoDados dialogoIntroducao;
    [SerializeField] private DialogoDados dialogoEPI;

    private Collider2D colisor2D;
    private bool jaInteragiu = false; // Flag para verificar se a interação já ocorreu

    private void Start()
    {
        colisor2D = GetComponent<Collider2D>();
        if (colisor2D == null)
        {
            Debug.LogError("O objeto " + gameObject.name + " não possui um Collider2D. A interação não funcionará corretamente.");
        }
    }

    public void AoClicar()
    {
        Debug.Log("Cliquei no Redutor!");
        if (!jaInteragiu) AbrirInterfaceUI();
    }

    public void AbrirInterfaceUI()
    {
        // Se a cena de UI não estiver carregada, carrega de forma aditiva
        if (!SceneManager.GetSceneByName("Scene_UI").isLoaded)
        {
            SceneManager.LoadSceneAsync("Scene_UI", LoadSceneMode.Additive);
            Debug.Log("Redutor clicado! Carregando a UI...");
        }

        ExecutarIntroducao();
    }

    public void ExecutarIntroducao()
    {
        // Se já interagiu uma vez, ignora e deixa os outros objetos receberem o clique
        if (jaInteragiu) return;

        jaInteragiu = true;

        // Tenta buscar o GerenciadorDialogo na cena caso a referência não esteja arrastada
        if (gd == null)
        {
            gd = FindFirstObjectByType<GerenciadorDialogo>(FindObjectsInactive.Include);
        }

        // Inicia o diálogo de introdução
        if (gd != null && dialogoIntroducao != null && dialogoEPI != null)
        {
            gd.IniciarDialogoEmSequencia(dialogoIntroducao, dialogoEPI);
        }

        // Desativa o collider do próprio Redutor para liberar o clique dos outros objetos
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