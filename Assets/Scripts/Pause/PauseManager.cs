using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject PainelOpcoes;
    [SerializeField] private GerenciadorAudio gAudio;
    [SerializeField] private GerenciadorDialogo gDialogo;

    public void AbrirOpcoes()
    {
        PainelOpcoes.SetActive(true);
        if (gAudio != null) gAudio.DefinirPausaAudio(true);
        if (gDialogo != null) gDialogo.DefinirPausaDialogo(true);
    }

    public void FecharOpcoes()
    {
        PainelOpcoes.SetActive(false);
        if (gAudio != null) gAudio.DefinirPausaAudio(false);
        if (gDialogo != null) gDialogo.DefinirPausaDialogo(false);
    }

    public void VoltarMenu()
    {
        if (gAudio != null) gAudio.DefinirPausaAudio(false);
        if (gDialogo != null) gDialogo.DefinirPausaDialogo(false);
        SceneManager.LoadScene("Scene_Menu");
    }

    private void Start()
    {
        gAudio = FindFirstObjectByType<GerenciadorAudio>(FindObjectsInactive.Include);
        gDialogo = FindFirstObjectByType<GerenciadorDialogo>(FindObjectsInactive.Include);
    }
}
