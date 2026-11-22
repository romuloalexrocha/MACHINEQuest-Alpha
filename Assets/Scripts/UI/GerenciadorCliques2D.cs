using UnityEngine;
//using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GerenciadorCliques2D : MonoBehaviour
{
    [SerializeField] private GerenciadorAudio gerenciadorAudio;
    [SerializeField] private GameObject painelPrancheta;
    [SerializeField] private GameObject painelInventario;
    [SerializeField] private GameObject painelMiniGame;

    private DestaqueHover hoverAtual;

    private void Start()
    {
        if (gerenciadorAudio == null)
            gerenciadorAudio = FindFirstObjectByType<GerenciadorAudio>(FindObjectsInactive.Include);
    }

    private void Update()
    {
        // Dentro do Update(), antes de checar o clique no cenário:
        //if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        //    return;

        if ((painelPrancheta != null && painelPrancheta.activeInHierarchy) ||
    (painelInventario != null && painelInventario.activeInHierarchy) ||
    (painelMiniGame != null && painelMiniGame.activeInHierarchy))
        {
            LimparHoverAtual();
            return;
        }

        if (Mouse.current == null || Camera.main == null) return;

        Vector2 posicaoMouse = Mouse.current.position.ReadValue();
        Vector2 posicaoMundo = Camera.main.ScreenToWorldPoint(posicaoMouse);
        RaycastHit2D hit = Physics2D.Raycast(posicaoMundo, Vector2.zero);

        VerificarHover(hit);

        if (Mouse.current.leftButton.wasPressedThisFrame)
            VerificarClique(hit);
    }

    private void VerificarHover(RaycastHit2D hit)
    {
        DestaqueHover novoHover = null;

        if (hit.collider != null)
        {
            novoHover = hit.collider.GetComponent<DestaqueHover>();
        }

        // Se o mouse saiu do objeto anterior ou passou para outro objeto
        if (novoHover != hoverAtual)
        {
            LimparHoverAtual();
            hoverAtual = novoHover;

            if (hoverAtual != null)
            {
                hoverAtual.AtivarDestaque();
            }
        }
    }

    private void LimparHoverAtual()
    {
        if (hoverAtual != null)
        {
            hoverAtual.DesativarDestaque();
            hoverAtual = null;
        }
    }

    private void VerificarClique(RaycastHit2D hit)
    {
        if (hit.collider != null)
        {
            Debug.Log("Bati em: " + hit.collider.name);
            // Pega TODOS os componentes clicáveis no objeto (ex: ItemColetavel + DialogoGatilho2D)
            IClicavel[] objetosClicaveis = hit.collider.GetComponents<IClicavel>();

            if (objetosClicaveis.Length > 0)
            {
                if (gerenciadorAudio != null)
                {
                    gerenciadorAudio.TocarSomClique();
                }

                // Executa o AoClicar() de cada script anexado no objeto
                foreach (IClicavel item in objetosClicaveis)
                {
                    item.AoClicar();
                }
            }
        }
    }
}
