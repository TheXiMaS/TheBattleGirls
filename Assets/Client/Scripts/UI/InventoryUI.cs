using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private GameObject activeSlot;

    private void Awake()
    {
        if (activeSlot == null) 
            Debug.Log($"{nameof(InventoryUI)}: Acive slot image is not found!");
    }

    public void UpdateUI()
    {
        if (gameObject.transform.childCount != 0)
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                Destroy(gameObject.transform.GetChild(i).gameObject);
            }
        }

        List<InventorySlot> slots = inventory.GetInventorySlots();

        if (slots == null)
        {
            Debug.Log("Slots is null!");
            return;
        }

        foreach (InventorySlot slot in slots)
        {
            var drawedSlot = Instantiate(activeSlot, transform);
            if (slot.GetItem() != null)
            {
                var itemSpite = slot.GetItem().GetComponent<SpriteRenderer>().sprite;
                drawedSlot.GetComponent<Image>().sprite = itemSpite;
            }
        }
    }
}
