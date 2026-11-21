using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ControleInventario : MonoBehaviour
{
    public GameObject gavetaSlots;
    public Image[] slotsIcones;

    [Header("Item Equipado no Mouse")]
    [SerializeField] private Image iconeItemEquipado; // Image solta no Canvas (Raycast Target DESMARCADO!)

    private List<string> itensColetados = new List<string>();
    private List<Sprite> iconesColetados = new List<Sprite>();
    private List<Color> coresColetadas = new List<Color>();

    private string itemEquipado = "";

    public void Start()
    {
        // Inicializa a gaveta de slots como desativada
        if (gavetaSlots != null) gavetaSlots.SetActive(false);

        if(iconeItemEquipado != null)
        {
            iconeItemEquipado.raycastTarget = false; // Garante que nunca bloqueará cliques na cena
            iconeItemEquipado.enabled = false;
        }
    }

    private void Update()
    {
        // Faz o ícone do item equipado seguir o ponteiro do mouse
        if (!string.IsNullOrEmpty(itemEquipado) && iconeItemEquipado != null && Mouse.current != null)
        {
            iconeItemEquipado.transform.position = Mouse.current.position.ReadValue();
        }
    }

    // Conecte este método no OnClick() de cada Button dos slots, passando o índice (0, 1, 2, 3, 4)
    public void SelecionarSlot(int indiceSlot)
    {
        // Se clicou em um slot vazio (ou fora do alcance dos itens), guarda o item de volta
        if (indiceSlot < 0 || indiceSlot >= itensColetados.Count)
        {
            DesequiparItem();
            return;
        }

        string itemClicado = itensColetados[indiceSlot];

        // Se clicou no mesmo item que já está na mão, guarda ele de volta também
        if (itemEquipado == itemClicado)
        {
            DesequiparItem();
            return;
        }

        // Equipa o novo item no ponteiro do mouse
        itemEquipado = itemClicado;

        if (iconeItemEquipado != null)
        {
            iconeItemEquipado.sprite = iconesColetados[indiceSlot];
            iconeItemEquipado.color = coresColetadas[indiceSlot];
            iconeItemEquipado.enabled = true;
            if (Mouse.current != null)
            {
                iconeItemEquipado.transform.position = Mouse.current.position.ReadValue();
            }
        }
    }

    public void DesequiparItem()
    {
        itemEquipado = "";
        if (iconeItemEquipado != null)
        {
            iconeItemEquipado.sprite = null;
            iconeItemEquipado.enabled = false;
        }
    }

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
                slotsIcones[i].sprite = null;
                slotsIcones[i].enabled = false;
            }
        }
    }

    public void RemoverItem(string nomeItem)
    {
        // Se o item removido estava equipado no mouse, limpa o cursor
        if (itemEquipado == nomeItem)
        {
            DesequiparItem();
        }

        for (int i = 0; i < itensColetados.Count; i++)
        {
            if (itensColetados[i] == nomeItem)
            {
                // Remove da mesma posição nas 3 listas paralelas
                itensColetados.RemoveAt(i);
                iconesColetados.RemoveAt(i);
                coresColetadas.RemoveAt(i);

                AtualizarInterface();
                Debug.Log($"Item removido do inventário: {nomeItem}");
                return;
            }
        }
    }

    // Método simples para outros scripts consultarem qual item está na mão do jogador
    public string ObterItemEquipado()
    {
        return itemEquipado;
    }
}
