using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject PainelOpcoes;

    public void AbrirOpcoes()
    {
        PainelOpcoes.SetActive(true);
        Time.timeScale = 0f;
    }

    public void FecharOpcoes()
    {
        PainelOpcoes.SetActive(false);
        Time.timeScale = 1f;
    }

    public void VoltarMenu()
    {
        SceneManager.LoadScene("Scene_Menu");
    }
}
