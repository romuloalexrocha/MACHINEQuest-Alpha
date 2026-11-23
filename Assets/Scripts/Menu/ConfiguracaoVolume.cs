using UnityEngine;
using UnityEngine.UI;

public class ConfiguracaoVolume : MonoBehaviour
{
    [SerializeField] private Slider sliderVolume;

    private void Start()
    {
        // Carrega o volume salvo anteriormente (se nunca foi alterado, começa em 1 = 100%)
        float volumeSalvo = PlayerPrefs.GetFloat("VolumeGeral", 1f);

        // Aplica o volume imediatamente no jogo
        AudioListener.volume = volumeSalvo;

        if (sliderVolume != null)
        {
            sliderVolume.value = volumeSalvo;
            // Fica escutando toda vez que o jogador arrastar a bolinha do slider
            sliderVolume.onValueChanged.AddListener(MudarVolume);
        }
    }

    public void MudarVolume(float valor)
    {
        // Altera o volume global do jogo (vai de 0.0 até 1.0)
        AudioListener.volume = valor;

        // Salva a preferência do jogador na memória
        PlayerPrefs.SetFloat("VolumeGeral", valor);
        PlayerPrefs.Save();
    }
}