using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Inventory")]
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private InventoryType inventoryType;
    [SerializeField] private bool inventoryIsLocked;
    [SerializeField][Range(1, 40)] private int slotsCount;

    [SerializeField] private List<InventorySlot> slots = new();

    [Header("Pickup and Drop")]
    [SerializeField] private float pickupRadius = 1.0f;

    private Vector2 pickupArea;

    private void Start()
    {
        InitializeInventory();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            OnInteraction();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            SetActivePrevSlot();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            SetActiveNextSlot();
        }
    }

    private void InitializeInventory()
    {
        int initializedSlots = 0;
        for (int i = 0; i < slotsCount; i++)
        {
            if (i == 0)
            {
                slots.Add(new InventorySlot(i, true));
            }
            else
            {
                slots.Add(new InventorySlot(i));
            }

            initializedSlots++;
            Debug.Log($"Slot #{i + 1} " +
                $"(Active: {slots[i].IsActive()}, Empty: {slots[i].IsEmpty()})" +
                $" has been initialized...");

            if (inventoryType == InventoryType.SingleSlot) break;
        }

        inventoryUI.UpdateUI();
        Debug.Log("Initialized slots: " + initializedSlots);
    }

    private void OnInteraction()
    {
        if (inventoryIsLocked) return;

        foreach (InventorySlot slot in slots)
        {
            if (slot.IsActive())
            {
                if (slot.IsEmpty())
                {
                    PickupItem();
                    break;
                }
                else
                {
                    DropItem();
                    break;
                }
            }
        } 
    }

    private void PickupItem()
    {
        if (!InventoryIsFull())
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(
                        transform.position,
                        pickupArea,
                        0f);

            foreach (InventorySlot slot in slots)
            {
                if (slot.IsActive())
                {
                    foreach (Collider2D collider in colliders)
                    {
                        if (collider.gameObject.CompareTag("Player")) continue;

                        try
                        {
                            slot.AddItem(collider.gameObject);
                            inventoryUI.UpdateUI();
                        }
                        catch (System.Exception e)
                        {
                            Debug.LogException(e);
                        }

                        break;
                    }

                    break;
                }
            }
        }
        else
        {
            Debug.Log("Inventory is full!");
        }
    }

    private void DropItem()
    {
        foreach (InventorySlot slot in slots)
        {   
            if (slot.IsActive())
            {
                try
                {
                    slot.RemoveItem();
                    inventoryUI.UpdateUI();
                }
                catch (System.Exception e)
                {
                    Debug.LogException(e);
                }

                break;
            }
        }
    }

    private void SetActivePrevSlot()
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.IsActive())
            {
                slot.SetActive(false);

                if (slot.GetID() - 1 >= 0)
                {
                    slots[slot.GetID() - 1].SetActive(true);
                }
                else
                {
                    slots[slots.Count - 1].SetActive(true);
                }

                break;
            }
        }
    }

    private void SetActiveNextSlot()
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.IsActive())
            {
                slot.SetActive(false);
                
                if (slot.GetID() + 1 < slots.Count)
                {
                    slots[slot.GetID() + 1].SetActive(true);
                }
                else
                {
                    slots[0].SetActive(true);
                }

                break;
            }
        }
    }

    private void SetActiveSlot(int slotIndex)
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.IsActive())
            {
                if (slot.GetID() == slotIndex)
                {
                    Debug.Log("This slot is already active!");
                    break;
                }
                else
                {
                    slot.SetActive(false);
                }
            }
            else
            {
                if (slot.GetID() == slotIndex)
                {
                    slot.SetActive(true);
                }
            }
        }
    }

    private InventorySlot GetSlotOfItem(GameObject item) 
    {
        foreach (InventorySlot slot in slots)
        {
            if (!slot.IsEmpty()) continue;

            if (slot.GetItem() == item) return slot;
        }
        return null;
    }

    private bool InventoryIsFull()
    {
        bool isFull = true;
        foreach (InventorySlot slot in slots)
        {
            if (slot.IsEmpty())
            {
                isFull = false;
                break;
            }
        }

        return isFull;
    }

    private void OnDrawGizmosSelected()
    {
        pickupArea = new Vector2(
            transform.localScale.x + pickupRadius,
            transform.localScale.y + pickupRadius
            );

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position, pickupArea);
    }

    public List<InventorySlot> GetInventorySlots() => slots;
}

enum InventoryType
{
    SingleSlot,
    MultySlot
}

[System.Serializable]
public class InventorySlot
{
    [SerializeField][CustomAttributes.ReadOnly] private int _id;
    [SerializeField][CustomAttributes.ReadOnly] private GameObject _item;
    [SerializeField][CustomAttributes.ReadOnly] private bool _isEmpty;
    [SerializeField][CustomAttributes.ReadOnly] private bool _isActive;

    public InventorySlot(int id)
    {
        _id = id;
        _item = null;
        _isEmpty = _item == null;
        _isActive = false;
    }

    public InventorySlot(int id, bool isActive)
    {
        _id = id;
        _item = null;
        _isEmpty = _item == null;
        _isActive = isActive;
    }

    public InventorySlot(int id, GameObject item, bool isActive)
    {
        _id = id;
        _item = item;
        _isEmpty = _item == null;
        _isActive = isActive;
    }

    public void AddItem(GameObject item)
    {
        if (!IsEmpty()) return;

        _item = item;
        _isEmpty = false;
        Debug.Log($"{_item.name} has been added to inventory!");
    }

    public void RemoveItem()
    {
        if (IsEmpty()) return;

        Debug.Log($"{_item.name} has been removed from inventory!");
        _item = null;
        _isEmpty = true;
    }

    public void SetActive(bool value)
    {
        if (_isActive == value) return;
        
        _isActive = value;
        if (_isActive == true)
        {
            Debug.Log($"Slot #{_id + 1} is now active!");
        }
    } 

    public int GetID() => _id;
    public GameObject GetItem() => _item;
    public bool IsEmpty() => _isEmpty;
    public bool IsActive() => _isActive;
}