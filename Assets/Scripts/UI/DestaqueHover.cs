using UnityEngine;

public class DestaqueHover : MonoBehaviour
{
    [Header("Opção A: Objeto de Borda / Contorno (Recomendado)")]
    [SerializeField] private GameObject objetoBorda;

    [Header("Opção B: Realce Direto no Sprite (Opcional)")]
    [SerializeField] private bool usarRealceNoSprite = false;
    [SerializeField] private Color corDestaque = new Color(1f, 0.85f, 0.35f, 1f); // Amarelo/laranja suave
    [SerializeField] private float escalaHover = 1.03f;

    private SpriteRenderer spriteRenderer;
    private Collider2D colisor2D;
    private Color corOriginal;
    private Vector3 escalaOriginal;
    private bool estaDestacado = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        colisor2D = GetComponent<Collider2D>();

        if (spriteRenderer != null)
        {
            corOriginal = spriteRenderer.color;
        }
        escalaOriginal = transform.localScale;

        if (objetoBorda != null)
        {
            objetoBorda.SetActive(false);
        }
    }

    public void AtivarDestaque()
    {
        if (estaDestacado) return;
        if (colisor2D != null && !colisor2D.enabled) return;

        estaDestacado = true;

        if (objetoBorda != null)
        {
            objetoBorda.SetActive(true);
        }

        if (usarRealceNoSprite)
        {
            if (spriteRenderer != null) spriteRenderer.color = corDestaque;
            transform.localScale = escalaOriginal * escalaHover;
        }
    }

    public void DesativarDestaque()
    {
        if (!estaDestacado) return;
        estaDestacado = false;

        if (objetoBorda != null)
        {
            objetoBorda.SetActive(false);
        }

        if (usarRealceNoSprite)
        {
            if (spriteRenderer != null) spriteRenderer.color = corOriginal;
            transform.localScale = escalaOriginal;
        }
    }

    private void OnDisable()
    {
        // Garante que o destaque apague se o objeto for coletado ou desativado
        DesativarDestaque();
    }
}
