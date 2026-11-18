using UnityEngine;

public class ItemColetavel : MonoBehaviour, IClicavel
{
    [Header("Configurações do Item")]
    public string nomeItem;
    public Sprite iconeItem;
    public bool ehEPI = true;

    [Header("Referências")]
    public ControleInventario controleInventario;
    public GerenciadorEPI gerenciadorEPI;

    private Camera cameraPrincipal;
    private Color corOriginal = Color.white; // Cor padrão do item, caso queira restaurar depois

    private void Awake()
    {
        cameraPrincipal = Camera.main;

        // Pega automaticamente o Sprite do próprio objeto se o campo estiver vazio
        // ATENÇÃO: APENAS PARA TESTE
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (iconeItem == null)
        {
            corOriginal = sr.color;
            if (sr != null) iconeItem = sr.sprite;
        }
    }

    private void Start()
    {
        // Tenta buscar referências caso não tenham sido atribuídas no Inspector
        BuscarReferencias();
    }

    public void AoClicar()
    {
        Coletar();
    }

    private void BuscarReferencias()
    {
        // Se o controle de inventário não estiver atribuído, tenta encontrá-lo na cena
        if (controleInventario == null)
        {
            controleInventario = FindFirstObjectByType<ControleInventario>(FindObjectsInactive.Include);
            if (controleInventario == null)
            {
                Debug.LogWarning("ControleInventario não encontrado na cena.");
            }
        }
        // Se o gerenciador de EPI não estiver atribuído, tenta encontrá-lo na cena
        if (gerenciadorEPI == null)
        {
            gerenciadorEPI = FindFirstObjectByType<GerenciadorEPI>(FindObjectsInactive.Include);
            if (gerenciadorEPI == null)
            {
                Debug.LogWarning("GerenciadorEPI não encontrado na cena.");
            }
        }
    }

    public void Coletar()
    {
        // Tenta buscar novamente caso não tenha achado no Awake
        if (controleInventario == null || gerenciadorEPI == null)
        {
            BuscarReferencias();
        }

        if (controleInventario != null)
        {
            bool guardadoComSucesso = controleInventario.AdicionarItem(nomeItem, iconeItem, corOriginal);

            if (guardadoComSucesso)
            {
                if (ehEPI && gerenciadorEPI != null)
                {
                    gerenciadorEPI.ColetarEPI(nomeItem);
                }

                gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.LogError("ControleInventario não foi encontrado na cena!");
        }
    }
}