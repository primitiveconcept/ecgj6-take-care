namespace TakeCare
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    
    
    public class PlayerInventory : MonoBehaviour
    {
        private static PlayerInventory instance;
        
        public List<InventorySlot> Items;

        private Dictionary<ItemData, InventorySlot> index;
        private int currentItemIndex = 0;


        public void Awake()
        {
            instance = this;
            instance.index = new Dictionary<ItemData, InventorySlot>();
        }
        
        public static InventorySlot GetNext()
        {
            Debug.Log("INVENTORY: Next item");
            
            if (instance.Items.Count == 0)
                return null;
            
            instance.currentItemIndex++;
            if (instance.currentItemIndex == instance.Items.Count)
                instance.currentItemIndex = 0;

            return instance.Items[instance.currentItemIndex];
        }


        public static InventorySlot GetPrevious()
        {
            Debug.Log("INVENTORY: Previous item");
            
            if (instance.Items.Count == 0)
                return null;

            instance.currentItemIndex--;
            if (instance.currentItemIndex < 0)
                instance.currentItemIndex = instance.Items.Count - 1;

            return instance.Items[instance.currentItemIndex];
        }
        

        public static void AddItem(ItemData itemData, int quantity = 1)
        {
            if (!instance.index.ContainsKey(itemData))
                instance.index.Add(itemData, new InventorySlot(itemData, 0));

            instance.index[itemData].Quantity += quantity;
        }


        public static void RemoveItem(ItemData itemData, int quantity = 1)
        {
            if (!instance.index.ContainsKey(itemData))
                return;

            instance.index[itemData].Quantity -= quantity;
            if (instance.index[itemData].Quantity < 1)
                instance.index.Remove(itemData);
        }

        public static int GetItemQuantity(ItemData itemData)
        {
            if (!instance.index.ContainsKey(itemData))
                return 0;
            
            return instance.index[itemData].Quantity;
        }

    }


    [Serializable]
    public class InventorySlot
    {
        public ItemData itemData;
        public int Quantity;


        public InventorySlot(ItemData itemData, int quantity)
        {
            this.itemData = itemData;
            this.Quantity = quantity;
        }
    }
}