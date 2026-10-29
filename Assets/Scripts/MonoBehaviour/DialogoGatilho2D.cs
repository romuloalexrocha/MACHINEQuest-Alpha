using UnityEngine;

public class DialogoGatilho2D : MonoBehaviour
{
    [SerializeField] private DialogoDados dadosDialogo;
    [SerializeField] private GerenciadorDialogo dialogoManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        // Se a referência não foi arrastada manualmente, busca o gerenciador na cena
        if (dialogoManager == null)
        {
            dialogoManager = FindFirstObjectByType<GerenciadorDialogo>();
        }

        if (dialogoManager != null && dadosDialogo != null)
        {
            // Inicia o diálogo quando o objeto é clicado
            dialogoManager.IniciarDialogo(dadosDialogo);
        }
        else
        {
            Debug.LogWarning("Diálogo não configurado corretamente no objeto " + gameObject.name);
        }
    }
}
