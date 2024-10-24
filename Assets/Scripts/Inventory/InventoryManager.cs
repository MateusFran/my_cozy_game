using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int itemAmountLimit;
    public Item testItem;
    
    [Header("Inventory")]
    [SerializeField] private Item[] inventory;

    private void Update(){
        if (Input.GetKeyDown(KeyCode.O)){
            AddItem(testItem);
        }
        if (Input.GetKeyDown(KeyCode.F)){
            Debug.Log(IsFull(testItem));
        }
    }

    public bool AddItem(Item addItem){
        Debug.Log(addItem);

        if (IsFull(addItem)) return false;

        for (int i = 0; i < inventory.Length; i++){
            Item inv = inventory[i];

            if (inv.data == addItem.data && inv.amount < itemAmountLimit){
                inventory[i].amount++;
                Debug.Log("Inventory: Item Added.");
                return true;
            }
            else if (inv.data == null){
                inventory[i] = new Item(){data=addItem.data, amount=addItem.amount};
                Debug.Log("Inventory: Item Added.");
                return true;
            }
        }
        return false;
    }

    public bool IsFull(Item itemCheck){
        for (int i = 0; i < inventory.Length; i++){
            if (inventory[i].data == null){
                return false;
            }
            else if (inventory[i].data == itemCheck.data && inventory[i].amount < itemAmountLimit){
                return false;
            }
        }
        return true;
    }
}
