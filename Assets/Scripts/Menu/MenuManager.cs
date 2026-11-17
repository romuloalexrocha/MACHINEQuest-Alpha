using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    [Header("Nome Cenas")]
    [SerializeField]private string cenaJogo;
    [SerializeField] private GameObject PainelMenu;
    [SerializeField] private GameObject PainelOpcoes;
    [SerializeField] private GameObject PainelCreditos;

    public void Jogar()
    {
        SceneManager.LoadScene(cenaJogo);
    }

    public void AbrirOpcoes()
    {
        PainelMenu.SetActive(false);
        PainelOpcoes.SetActive(true);
    }

    public void FecharOpcoes()
    {
        PainelMenu.SetActive(true);
        PainelOpcoes.SetActive(false);
    }

    public void AbrirCreditos()
    {
        PainelMenu.SetActive(false);
        PainelCreditos.SetActive(true);
    }

    public void FecharCreditos()
    {
        PainelMenu.SetActive(true);
        PainelCreditos.SetActive(false);
    }

    public void Sair()
    {
        Application.Quit();
    }
}
