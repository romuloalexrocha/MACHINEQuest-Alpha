using UnityEngine;

public class ItemColetavel : MonoBehaviour, IClicavel
{
    [Header("Configurações do Item")]
    public string nomeItem;
    public TipoItem tipoItem = TipoItem.Geral;
    public Sprite iconeItem;
    public bool ehEPI = true;

    [Header("Referências")]
    public ControleInventario controleInventario;
    public GerenciadorEPI gerenciadorEPI;

    private Color corOriginal = Color.white; // Cor padrão do item, caso queira restaurar depois

    private void Awake()
    {
        // Pega automaticamente o Sprite do próprio objeto se o campo estiver vazio
        // ATENÇÃO: APENAS PARA TESTE
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (iconeItem == null && sr !=  null)
        {
            corOriginal = sr.color;
            iconeItem = sr.sprite;
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
        }
        // Se o gerenciador de EPI não estiver atribuído, tenta encontrá-lo na cena
        if (gerenciadorEPI == null)
        {
            gerenciadorEPI = FindFirstObjectByType<GerenciadorEPI>(FindObjectsInactive.Include);
        }
    }

    public void Coletar()
    {
        // Tenta buscar novamente caso não tenha achado no Awake
        if (controleInventario == null || gerenciadorEPI == null)
        {
            BuscarReferencias();
        }

        // Caminho 1: Se for EPI, vai direto para a barra horizontal inferior
        if (ehEPI)
        {
            if (gerenciadorEPI != null)
            {
                gerenciadorEPI.ColetarEPI(nomeItem, iconeItem);
                gameObject.SetActive(false);
            }
            return;
        }

        // Caminho 2: Se for ferramenta/insumo, vai para a gaveta vertical
        if (controleInventario != null)
        {
            bool guardadoComSucesso = controleInventario.AdicionarItem(nomeItem, tipoItem, iconeItem, corOriginal);

            if (guardadoComSucesso)
            {
                gameObject.SetActive(false);
            }
            else
            {
                Debug.LogError("ControleInventario não foi encontrado na cena!");
            }
        }
    }
}
