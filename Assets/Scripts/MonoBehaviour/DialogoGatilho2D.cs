using UnityEngine;
using UnityEngine.InputSystem;

public class DialogoGatilho2D : MonoBehaviour
{
    [SerializeField] private DialogoDados dadosDialogo;
    [SerializeField] private GerenciadorDialogo dialogoManager;

    private Camera cameraPrincipal;

    private void Awake()
    {
        cameraPrincipal = Camera.main;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            VerificarClique();
        }
    }

    private void VerificarClique()
    {
        if (cameraPrincipal == null) cameraPrincipal = Camera.main;

        Vector2 posicaoMouse = Mouse.current.position.ReadValue();
        Vector3 posicaoMundo3D = cameraPrincipal.ScreenToWorldPoint(new Vector3(posicaoMouse.x, posicaoMouse.y, cameraPrincipal.nearClipPlane));
        Vector2 posicaoMundo = new Vector2(posicaoMundo3D.x, posicaoMundo3D.y);

        RaycastHit2D hit = Physics2D.Raycast(posicaoMundo, Vector2.zero);

        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            AcionarDialogo();
        }
    }

    private void AcionarDialogo()
    {
        if (dialogoManager == null)
        {
            dialogoManager = FindFirstObjectByType<GerenciadorDialogo>(FindObjectsInactive.Include);
        }

        if (dialogoManager != null && dadosDialogo != null)
        {
            dialogoManager.IniciarDialogo(dadosDialogo);
        }
        else
        {
            Debug.LogWarning("Diálogo não configurado corretamente no objeto " + gameObject.name);
        }
    }
}
