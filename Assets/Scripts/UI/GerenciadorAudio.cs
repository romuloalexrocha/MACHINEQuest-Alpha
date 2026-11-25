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

    private bool jogoPausado = false;

    private void Start()
    {
        // Garante que o volume salvo nas opções seja aplicado ao carregar a UI
        AudioListener.volume = PlayerPrefs.GetFloat("VolumeGeral", 1f);
    }

    private void Update()
    {
        // Bloqueia detecção de cliques na cena enquanto o jogo estiver pausado
        if (jogoPausado) return;

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            VerificarCliqueEmObjeto2D();
        }
    }

    // Método para o Vitor chamar ao abrir/fechar o menu de Pause
    public void DefinirPausaAudio(bool pausado)
    {
        jogoPausado = pausado;
        AudioListener.pause = pausado;
    }

    // Método opcional caso o menu de Pause também tenha controle de volume
    public void DefinirVolumeGeral(float novoVolume)
    {
        AudioListener.volume = Mathf.Clamp01(novoVolume);
        PlayerPrefs.SetFloat("VolumeGeral", AudioListener.volume);
        PlayerPrefs.Save();
    }

    private void VerificarCliqueEmObjeto2D()
    {
        if (Camera.main == null) return;

        Vector2 posicaoMouse = Mouse.current.position.ReadValue();
        Vector2 posicaoMundo = Camera.main.ScreenToWorldPoint(posicaoMouse);

        RaycastHit2D hit = Physics2D.Raycast(posicaoMundo, Vector2.zero);

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

    public void TocarSomErro()
    {
        if (fonteSFX != null && somErro != null)
        {
            fonteSFX.PlayOneShot(somErro);
        }
    }
}