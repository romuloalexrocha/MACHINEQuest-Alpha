using UnityEngine;

[System.Serializable]
public class VistaRedutor
{
    public string nomeVista;            // Ex: "Lateral", "Traseira", "Superior"
    public Sprite spriteRedutor;        // Sprite do ângulo (pode repetir o atual enquanto não há arte)
    public GameObject grupoInteracoes;  // Objeto pai com as peças/colisores visíveis nesse ângulo
    public bool habilitada = true;      // Desmarque no Inspector para vistas que começam bloqueadas (ex: Superior)
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

        // Garante que o jogo não comece em uma vista desabilitada
        if (vistas != null && vistas.Length > 0 && !vistas[indiceAtual].habilitada)
        {
            GirarDireita();
            return;
        }

        AtualizarVista();
    }

    // Conecte este método no OnClick() do Botão Seta Direita
    public void GirarDireita()
    {
        if (vistas == null || vistas.Length == 0) return;

        for (int tentativa = 0; tentativa < vistas.Length; tentativa++)
        {
            indiceAtual = (indiceAtual + 1) % vistas.Length;
            if (vistas[indiceAtual].habilitada)
            {
                AtualizarVista();
                return;
            }
        }
    }

    // Conecte este método no OnClick() do Botão Seta Esquerda
    public void GirarEsquerda()
    {
        if (vistas == null || vistas.Length == 0) return;

        for (int tentativa = 0; tentativa < vistas.Length; tentativa++)
        {
            indiceAtual--;
            if (indiceAtual < 0)
            {
                indiceAtual = vistas.Length - 1;
            }

            if (vistas[indiceAtual].habilitada)
            {
                AtualizarVista();
                return;
            }
        }
    }

    public void DefinirEstadoVista(string nomeAlvo, bool habilitar)
    {
        if (vistas == null) return;

        for (int i = 0; i < vistas.Length; i++)
        {
            if (vistas[i].nomeVista.Equals(nomeAlvo, System.StringComparison.OrdinalIgnoreCase))
            {
                vistas[i].habilitada = habilitar;

                // Se estamos desabilitando a vista que o jogador está olhando agora, gira automaticamente para uma válida
                if (!habilitar && indiceAtual == i)
                {
                    GirarDireita();
                }
                return;
            }
        }
    }

    public string ObterNomeVistaAtual()
    {
        if (vistas == null || vistas.Length == 0) return "";
        return vistas[indiceAtual].nomeVista;
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
    }
}
