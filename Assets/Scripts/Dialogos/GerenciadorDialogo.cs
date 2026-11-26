using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GerenciadorDialogo : MonoBehaviour
{
    [Header("Referência dos Dados")]
    [SerializeField] private DialogoDados dialogoAtual;
    private int indexListaDialogos = 0;

    [Header("Componentes da UI")]
    [SerializeField] private TextMeshProUGUI campoNome;
    [SerializeField] private TextMeshProUGUI campoTexto;
    [SerializeField] private Image imagemAvatar;
    [SerializeField] private GameObject caixaDialogo;

    private DialogoDados dialogoEmFila;
    private bool jogoPausado = false;
    private bool estavaAtivoAntesDoPause = false;

    void Start()
    {
        indexListaDialogos = 0;
        if (caixaDialogo != null)
        {
            caixaDialogo.SetActive(false);
        }
    }

    // Método para o Vitor chamar ao abrir/fechar o menu de Pause
    public void DefinirPausaDialogo(bool pausado)
    {
        jogoPausado = pausado;

        if (caixaDialogo == null) return;

        if (jogoPausado == true)
        {
            // Guarda se a caixa estava aberta e oculta para não sobrepor o menu de pause
            estavaAtivoAntesDoPause = caixaDialogo.activeSelf;
            if (estavaAtivoAntesDoPause)
            {
                caixaDialogo.SetActive(false);
            }
        }
        else
        {
            // Restaura a caixa exatamente onde estava ao despausar
            if (estavaAtivoAntesDoPause)
            {
                caixaDialogo.SetActive(true);
                estavaAtivoAntesDoPause = false;
            }
        }
    }

    public int RetornarValorDeEtapa()
    {
        if (dialogoEmFila != null)
        {
            return dialogoEmFila.etapa;
        }

        if (dialogoAtual != null)
        {
            return dialogoAtual.etapa;
        }

        return 0; // Nenhuma etapa/diálogo iniciado ainda
    }

    public void AvancarParaProximaFala()
    {
        if (jogoPausado) return;

        indexListaDialogos++;

        if (dialogoAtual != null && indexListaDialogos >= dialogoAtual.linhasDialogo.Count)
        {
            FecharCaixaDialogo();
        }
        else
        {
            ExibirFalaAtual();
        }
    }

    public void IniciarDialogo(DialogoDados novoDialogo)
    {
        if (novoDialogo == null) return;

        dialogoAtual = novoDialogo;
        indexListaDialogos = 0;

        if (jogoPausado)
        {
            // Se um diálogo disparar exatamente no frame do pause, marca para abrir ao despausar
            estavaAtivoAntesDoPause = true;
            ExibirFalaAtual();
            return;
        }

        caixaDialogo.SetActive(true);
        ExibirFalaAtual();
    }

    public void IniciarDialogoEmSequencia(DialogoDados primeiroDialogo, DialogoDados proximoDialogo)
    {
        dialogoEmFila = proximoDialogo;
        IniciarDialogo(primeiroDialogo);
    }

    public void EnfileirarDialogo(DialogoDados proximoDialogo)
    {
        dialogoEmFila = proximoDialogo;

        if (caixaDialogo != null && !caixaDialogo.activeSelf && !estavaAtivoAntesDoPause)
        {
            Invoke(nameof(DispararFilaSeCaixaFechada), 0.05f);
        }
    }

    private void DispararFilaSeCaixaFechada()
    {
        if (jogoPausado) return;

        if (dialogoEmFila != null && caixaDialogo != null && !caixaDialogo.activeSelf)
        {
            DialogoDados proximo = dialogoEmFila;
            dialogoEmFila = null;
            IniciarDialogo(proximo);
        }
    }

    private void ExibirFalaAtual()
    {
        if (dialogoAtual == null || dialogoAtual.linhasDialogo.Count == 0) return;

        DialogoLinhas falaAtual = dialogoAtual.linhasDialogo[indexListaDialogos];
        campoNome.text = "Sérgio";
        campoTexto.text = falaAtual.falaPersonagem;

        if (imagemAvatar != null)
        {
            if (falaAtual.spritePersonagem != null)
            {
                imagemAvatar.sprite = falaAtual.spritePersonagem;
                imagemAvatar.gameObject.SetActive(true);
            }
            else
            {
                imagemAvatar.gameObject.SetActive(false);
            }
        }
    }

    private void FecharCaixaDialogo()
    {
        if (dialogoEmFila != null)
        {
            DialogoDados proximo = dialogoEmFila;
            dialogoEmFila = null;
            IniciarDialogo(proximo);
            return;
        }

        caixaDialogo.SetActive(false);
        indexListaDialogos = 0;
    }
}
