using UnityEngine;
using System.Collections.Generic;

/*
 * Permite que o Game Designer crie instâncias/arquivos de ScriptableObject
 * do tipo DialogoDados a partir do menu "Assets/Create"
 * no Unity Editor.
 */
[CreateAssetMenu(fileName = "NovoDialogo", menuName = "Sistema de Diálogo/Dados de Diálogo")]
public class DialogoDados : ScriptableObject
{
    public List<DialogoLinhas> linhasDialogo; // Lista de linhas de diálogo que compõem o diálogo
}
