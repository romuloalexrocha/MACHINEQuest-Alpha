using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InteracaoRedutor : MonoBehaviour
{
    [Header("Referências de Diálogo")]
    [SerializeField] private GerenciadorDialogo gd;
    [SerializeField] private DialogoDados dialogoIntroducao;

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

    private void Update()
    {
        // Se já interagiu, nem precisa processar a verificação de clique no Update
        if (jaInteragiu) return;

        // Verifica se o botão esquerdo do mouse foi pressionado nesta frame (usando o New Input System)
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (Camera.main == null) return;

            // Converte a posição do ponteiro do mouse na tela para o mundo 2D
            Vector2 posicaoMouse = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            // Dispara um raio na posição exata do clique
            RaycastHit2D hit = Physics2D.Raycast(posicaoMouse, Vector2.zero);

            // Verifica se o raio atingiu ESTE objeto
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                Debug.Log("Clique detectado via New Input System no objeto: " + gameObject.name);
                AbrirInterfaceUI();
            }
        }
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
        if (gd != null && dialogoIntroducao != null)
        {
            gd.IniciarDialogo(dialogoIntroducao);
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