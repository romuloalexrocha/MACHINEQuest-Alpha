using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    [Header("Nome Cenas")]
    [SerializeField] private string cenaJogo;
    [SerializeField] private string cenaUI;
    [SerializeField] private GameObject PainelMenu;
    [SerializeField] private GameObject PainelOpcoes;
    [SerializeField] private GameObject PainelCreditos;

    public void Jogar()
    {
        // Carrega a cena de gameplay
        SceneManager.LoadScene(cenaJogo, LoadSceneMode.Single);
        // Carrega a cena da interface do usuário de forma aditiva
        // SceneManager.LoadScene(cenaUI, LoadSceneMode.Additive);
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
