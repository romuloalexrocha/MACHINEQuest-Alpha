using UnityEngine;

[System.Serializable]
public class VistaRedutor
{
    public string nomeVista;            // Ex: "Lateral", "Traseira", "Superior"
    public Sprite spriteRedutor;        // Sprite do ângulo (pode repetir o atual enquanto não há arte)
    public GameObject grupoInteracoes;  // Objeto pai com as peças/colisores visíveis nesse ângulo
}

public class ControladorVistasRedutor : MonoBehaviour
{
    [Header("Componentes do Redutor")]
    [SerializeField] private SpriteRenderer spriteRendererRedutor;
    [SerializeField] private VistaRedutor[] vistas;

    private int indiceAtual = 0;

    private void Start()
    {
        if (spriteRendererRedutor == null)
        {
            spriteRendererRedutor = GetComponent<SpriteRenderer>();
        }

        AtualizarVista();
    }

    // Conecte este método no OnClick() do Botão Seta Direita
    public void GirarDireita()
    {
        if (vistas == null || vistas.Length == 0) return;

        indiceAtual = (indiceAtual + 1) % vistas.Length;
        AtualizarVista();
    }

    // Conecte este método no OnClick() do Botão Seta Esquerda
    public void GirarEsquerda()
    {
        if (vistas == null || vistas.Length == 0) return;

        indiceAtual--;
        if (indiceAtual < 0)
        {
            indiceAtual = vistas.Length - 1;
        }
        AtualizarVista();
    }

    private void AtualizarVista()
    {
        for (int i = 0; i < vistas.Length; i++)
        {
            bool ehVistaAtiva = (i == indiceAtual);

            if (ehVistaAtiva && spriteRendererRedutor != null && vistas[i].spriteRedutor != null)
            {
                spriteRendererRedutor.sprite = vistas[i].spriteRedutor;
            }

            if (vistas[i].grupoInteracoes != null)
            {
                vistas[i].grupoInteracoes.SetActive(ehVistaAtiva);
            }
        }

        Debug.Log("Vista atual do Redutor: " + vistas[indiceAtual].nomeVista);
    }
}
