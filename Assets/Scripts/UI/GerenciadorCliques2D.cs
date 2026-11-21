using UnityEngine;
//using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GerenciadorCliques2D : MonoBehaviour
{
    [SerializeField] private GerenciadorAudio gerenciadorAudio;

    private void Start()
    {
        if (gerenciadorAudio == null)
        {
            gerenciadorAudio = FindFirstObjectByType<GerenciadorAudio>(FindObjectsInactive.Include);
        }
    }

    private void Update()
    {
        // Dentro do Update(), antes de checar o clique no cenário:
        //if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        //    return;

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            VerificarClique();
        }
    }

    private void VerificarClique()
    {
        if (Camera.main == null) return;

        Vector2 posicaoMouse = Mouse.current.position.ReadValue();
        Vector2 posicaoMundo = Camera.main.ScreenToWorldPoint(posicaoMouse);

        RaycastHit2D hit = Physics2D.Raycast(posicaoMundo, Vector2.zero);

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