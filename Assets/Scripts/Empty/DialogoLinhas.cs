using UnityEngine;
/* 
 *  Permite que a classe DialogueLine seja serializável no Unity,
 *  permitindo que seus campos sejam exibidos e
 *  editados no Inspector do Unity Editor.
*/
[System.Serializable]
public class DialogoLinhas
{
    public string nomePersonagem; // Nome do personagem que está falando a linha de diálogo
    // Permite que o campo dialogueText seja exibido como uma área de texto no Inspector do Unity Editor
    [TextArea(3, 5)] public string falaPersonagem; // Texto da linha de diálogo
    public Sprite spritePersonagem; // Sprite do personagem associado à linha de diálogo
}
