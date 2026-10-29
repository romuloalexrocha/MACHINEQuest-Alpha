using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InteracaoRedutor : MonoBehaviour
{
    private void Update()
    {
        // Verifica se o botão esquerdo do mouse foi pressionado nesta frame (usando o New Input System)
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
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
        if (!SceneManager.GetSceneByName("Scene_UI").isLoaded)
        {
            SceneManager.LoadSceneAsync("Scene_UI", LoadSceneMode.Additive);
            Debug.Log("Redutor clicado! Carregando a UI...");
        }
    }
}