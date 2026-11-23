using UnityEngine;
using UnityEngine.InputSystem;

public class GerenciadorAudio : MonoBehaviour
{
    [Header("Fonte de Efeitos Sonoros (SFX)")]
    [SerializeField] private AudioSource fonteSFX;

    [Header("Clipes de Áudio")]
    [SerializeField] private AudioClip somClique;
    [SerializeField] private AudioClip somAcerto;
    [SerializeField] private AudioClip somErro;

    private void Start()
    {
        // Garante que o volume salvo seja aplicado mesmo se o jogo não começar pelo Menu
        AudioListener.volume = PlayerPrefs.GetFloat("VolumeGeral", 1f);
    }

    private void Update()
    {
        // Detecta o clique esquerdo usando o New Input System
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            VerificarCliqueEmObjeto2D();
        }
    }

    private void VerificarCliqueEmObjeto2D()
    {
        if (Camera.main == null) return;

        Vector2 posicaoMouse = Mouse.current.position.ReadValue();
        Vector2 posicaoMundo = Camera.main.ScreenToWorldPoint(posicaoMouse);

        // Dispara o raio 2D exatamente como nos outros scripts
        RaycastHit2D hit = Physics2D.Raycast(posicaoMundo, Vector2.zero);

        // Se acertou qualquer objeto clicável (Collider2D ativo) na cena, toca o som!
        if (hit.collider != null)
        {
            TocarSomClique();
        }
    }

    public void TocarSomClique()
    {
        if (fonteSFX != null && somClique != null)
        {
            fonteSFX.PlayOneShot(somClique);
        }
    }

    public void TocarSomAcerto()
    {
        if (fonteSFX != null && somAcerto != null)
        {
            fonteSFX.PlayOneShot(somAcerto);
        }
    }

    internal void TocarSomErro()
    {
        if (fonteSFX != null && somErro != null)
        {
            fonteSFX.PlayOneShot(somErro);
        }
    }
}
