using NUnit.Framework.Internal.Execution;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControleInventario : MonoBehaviour
{
    public GameObject gavetaSlots;
    public Image[] slotsIcones;

    private List<string> itensColetados = new List<string>();
    private List<Sprite> iconesColetados = new List<Sprite>();
    private List<Color> coresColetadas = new List<Color>();

    public void AlternarInventario()
    {
        gavetaSlots.SetActive(!gavetaSlots.activeSelf);
    }

    public bool AdicionarItem(string nomeItem, Sprite iconeItem, Color corItem = default)
    {
        if (itensColetados.Count >= 5) return false;

        // Se a cor não for informada, usa branco como cor padrão pura
        if (corItem == default) corItem = Color.white;

        itensColetados.Add(nomeItem);
        iconesColetados.Add(iconeItem);
        coresColetadas.Add(corItem);

        AtualizarInterface();
        return true;
    }

    public bool TemItem(string nomeItem)
    {
        return itensColetados.Contains(nomeItem);
    }

    private void AtualizarInterface()
    {
        for (int i = 0; i < slotsIcones.Length; i++)
        {
            if (i < itensColetados.Count)
            {
                slotsIcones[i].sprite = iconesColetados[i];
                slotsIcones[i].color = coresColetadas[i];
                slotsIcones[i].enabled = true;
                // Atribui o ícone do item correspondente
            }
            else
            {
                slotsIcones[i].enabled = false;
            }
        }
    }

    public bool VerificarItem(string nomeItem)
    {
        return itensColetados.Contains(nomeItem);
    }
}
