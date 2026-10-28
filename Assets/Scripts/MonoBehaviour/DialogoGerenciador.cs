using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogoGerenciador : MonoBehaviour
{
    [Header("Referência dos Dados")]
    [SerializeField] private DialogoDados dialogoAtual;
    private int indexListaDialogos = 0;

    [Header("Componentes da UI")]
    [SerializeField] private TextMeshProUGUI campoNome;
    [SerializeField] private TextMeshProUGUI campoTexto;
    [SerializeField] private Image imagemAvatar;
    [SerializeField] private GameObject caixaDialogo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        indexListaDialogos = 0; // Inicializa o índice da lista de diálogos
        IniciarDialogo(dialogoAtual); // Inicia o diálogo atual
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AvancarParaProximaFala()
    {
        indexListaDialogos++;

        // Verifica se ainda há falas restantes no diálogo
        if (indexListaDialogos >= dialogoAtual.linhasDialogo.Count)
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

    private void ExibirFalaAtual()
    {
        // Obtém a fala atual com base no índice
        DialogoLinhas falaAtual = dialogoAtual.linhasDialogo[indexListaDialogos];
        // Atualiza os campos da UI com as informações da fala atual
        campoNome.text = falaAtual.nomePersonagem;
        campoTexto.text = falaAtual.falaPersonagem;
        imagemAvatar.sprite = falaAtual.spritePersonagem;
    }

    private void FecharCaixaDialogo()
    {
        // Desativa a caixa de diálogo
        caixaDialogo.SetActive(false);

        // Reinicia o índice para o início do diálogo
        //indexListaDialogos = 0;
    }
}
