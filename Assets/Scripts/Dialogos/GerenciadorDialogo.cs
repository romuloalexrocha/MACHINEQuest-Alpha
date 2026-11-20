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

    private DialogoDados dialogoEmFila; // Armazena o próximo diálogo a ser iniciado após o atual

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        indexListaDialogos = 0;
        // Mantém a caixa fechada até ser solicitada por clique/evento
        if (caixaDialogo != null)
        {
            caixaDialogo.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AvancarParaProximaFala()
    {
        indexListaDialogos++;

        // Verifica se ainda há falas restantes no diálogo
        if (dialogoAtual != null && indexListaDialogos >= dialogoAtual.linhasDialogo.Count)
        {
            // O diálogo acabou!
            FecharCaixaDialogo();
        }
        else
        {
            // Existem falas, exibir a próxima fala
            ExibirFalaAtual();
        }
    }

    public void IniciarDialogo(DialogoDados novoDialogo)
    {
        dialogoAtual = novoDialogo;
        indexListaDialogos = 0;
        caixaDialogo.SetActive(true); // Liga a caixa apenas UMA vez no início!
        ExibirFalaAtual();
    }

    public void IniciarDialogoEmSequencia(DialogoDados primeiroDialogo, DialogoDados proximoDialogo)
    {
        dialogoEmFila = proximoDialogo;
        IniciarDialogo(primeiroDialogo);
    }

    public void EnfileirarDialogo(DialogoDados proximoDialogo)
    {
        // Apenas coloca na fila!
        dialogoEmFila = proximoDialogo;

        // Se ao final deste frame a caixa continuar fechada (caso um EPI não tenha fala), abre direto
        if (caixaDialogo != null && !caixaDialogo.activeSelf)
        {
            Invoke(nameof(DispararFilaSeCaixaFechada), 0.05f);
        }
    }

    private void DispararFilaSeCaixaFechada()
    {
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
        // Obtém a fala atual com base no índice
        DialogoLinhas falaAtual = dialogoAtual.linhasDialogo[indexListaDialogos];
        // Atualiza os campos da UI com as informações da fala atual
        campoNome.text = falaAtual.nomePersonagem;
        campoTexto.text = falaAtual.falaPersonagem;
        // Trata a imagem do avatar (só ativa se houver um sprite configurado)
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
        // Se houver um diálogo na fila, inicia ele direto sem fechar a caixa
        if (dialogoEmFila != null)
        {
            DialogoDados proximo = dialogoEmFila;
            dialogoEmFila = null; // Limpa a fila para encerrar normalmente depois
            IniciarDialogo(proximo);
            return;
        }

        // Desativa a caixa de diálogo
        caixaDialogo.SetActive(false);

        // Reinicia o índice para o início do diálogo
        indexListaDialogos = 0;
    }
}
