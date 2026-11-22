using UnityEngine;

public class ContornoHover : MonoBehaviour
{
    [Header("Configuração do Contorno")]
    [SerializeField] private Color corContorno = new Color(1f, 0.9f, 0f, 1f); // Amarelo forte
    [SerializeField] private float espessuraBorda = 0.05f;

    private SpriteRenderer spritePrincipal;
    private Collider2D colisor2D;
    private GameObject containerBorda;
    private SpriteRenderer[] rendersBorda;
    private bool estaDestacado = false;

    private void Start()
    {
        spritePrincipal = GetComponent<SpriteRenderer>();
        colisor2D = GetComponent<Collider2D>();

        if (spritePrincipal != null)
        {
            CriarContornoAutomatico();
        }
    }

    private void Update()
    {
        if (estaDestacado && rendersBorda != null && spritePrincipal != null)
        {
            for (int i = 0; i < rendersBorda.Length; i++)
            {
                if (rendersBorda[i].sprite != spritePrincipal.sprite)
                {
                    rendersBorda[i].sprite = spritePrincipal.sprite;
                }
            }
        }
    }

    private void CriarContornoAutomatico()
    {
        containerBorda = new GameObject("ContornoHover");
        containerBorda.transform.SetParent(transform);
        containerBorda.transform.localPosition = Vector3.zero;
        containerBorda.transform.localRotation = Quaternion.identity;
        containerBorda.transform.localScale = Vector3.one;

        Vector3[] direcoes = new Vector3[]
        {
            new Vector3(0f, espessuraBorda, 0f),
            new Vector3(0f, -espessuraBorda, 0f),
            new Vector3(-espessuraBorda, 0f, 0f),
            new Vector3(espessuraBorda, 0f, 0f),
            new Vector3(espessuraBorda, espessuraBorda, 0f),
            new Vector3(-espessuraBorda, espessuraBorda, 0f),
            new Vector3(espessuraBorda, -espessuraBorda, 0f),
            new Vector3(-espessuraBorda, -espessuraBorda, 0f)
        };

        rendersBorda = new SpriteRenderer[direcoes.Length];

        for (int i = 0; i < direcoes.Length; i++)
        {
            GameObject lado = new GameObject("Borda_" + i);
            lado.transform.SetParent(containerBorda.transform);
            lado.transform.localPosition = direcoes[i];
            lado.transform.localRotation = Quaternion.identity;
            lado.transform.localScale = Vector3.one;

            SpriteRenderer sr = lado.AddComponent<SpriteRenderer>();
            sr.sprite = spritePrincipal.sprite;
            sr.sharedMaterial = spritePrincipal.sharedMaterial; // Compatível com URP 2D
            sr.sortingLayerID = spritePrincipal.sortingLayerID;
            sr.sortingOrder = spritePrincipal.sortingOrder - 1;
            sr.color = corContorno;

            rendersBorda[i] = sr;
        }

        containerBorda.SetActive(false);
    }

    public void AtivarDestaque()
    {
        if (estaDestacado) return;
        if (colisor2D != null && !colisor2D.enabled) return;

        estaDestacado = true;
        if (containerBorda != null)
        {
            containerBorda.SetActive(true);
        }
    }

    public void DesativarDestaque()
    {
        if (!estaDestacado) return;

        estaDestacado = false;
        if (containerBorda != null)
        {
            containerBorda.SetActive(false);
        }
    }

    private void OnDisable()
    {
        DesativarDestaque();
    }
}