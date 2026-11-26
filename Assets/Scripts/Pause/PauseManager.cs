using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject PainelOpcoes;
    [SerializeField] private GerenciadorAudio audio;
    [SerializeField] private GerenciadorDialogo dialogo;

    public void AbrirOpcoes()
    {
        PainelOpcoes.SetActive(true);
        if (audio != null) audio.DefinirPausaAudio(true);
        if (dialogo != null) dialogo.DefinirPausaDialogo(true);
    }

    public void FecharOpcoes()
    {
        PainelOpcoes.SetActive(false);
        if (audio != null) audio.DefinirPausaAudio(false);
        if (dialogo != null) dialogo.DefinirPausaDialogo(false);
    }

    public void VoltarMenu()
    {
        if (audio != null) audio.DefinirPausaAudio(false);
        if (dialogo != null) dialogo.DefinirPausaDialogo(false);
        SceneManager.LoadScene("Scene_Menu");
    }

    private void Start()
    {
        GerenciadorAudio audio = FindFirstObjectByType<GerenciadorAudio>(FindObjectsInactive.Include);
        GerenciadorDialogo dialogo = FindFirstObjectByType<GerenciadorDialogo>(FindObjectsInactive.Include);
    }
}
