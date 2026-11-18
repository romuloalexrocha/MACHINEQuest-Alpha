using UnityEngine;

public class DialogoGatilho2D : MonoBehaviour, IClicavel
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

    public void AoClicar()
    {
        AcionarDialogo();
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
