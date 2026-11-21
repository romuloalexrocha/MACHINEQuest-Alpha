using UnityEngine;

public class DialogoGatilho2D : MonoBehaviour, IClicavel
{
    [SerializeField] private DialogoDados dadosDialogo;
    [SerializeField] private GerenciadorDialogo dialogoManager;

    [Header("Configurações")]
    [SerializeField] private bool dispararApenasUmaVez = false;
    private bool jaDisparou = false;

    private Camera cameraPrincipal;

    private void Awake()
    {
        cameraPrincipal = Camera.main;
    }

    public void AoClicar()
    {
        // Se estiver marcado para disparar só uma vez e já tiver disparado, para aqui
        if (dispararApenasUmaVez && jaDisparou) return;
        jaDisparou = true;

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
