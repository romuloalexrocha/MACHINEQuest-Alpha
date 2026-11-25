using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject PainelOpcoes;

    public void AbrirOpcoes()
    {
        PainelOpcoes.SetActive(true);
    }

    public void FecharOpcoes()
    {
        PainelOpcoes.SetActive(false);
    }

    public void VoltarMenu()
    {
        SceneManager.LoadScene("Scene_Menu");
    }
}
