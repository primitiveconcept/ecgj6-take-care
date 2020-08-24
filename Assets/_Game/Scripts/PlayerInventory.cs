namespace TakeCare
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    
    
    public class PlayerInventory : MonoBehaviour
    {
        private static PlayerInventory instance;
        
        public List<InventorySlot> Items;

        private Dictionary<IItem, InventorySlot> index;
        private int currentItemIndex = 0;


        public void Awake()
        {
            instance = this;
            instance.index = new Dictionary<IItem, InventorySlot>();
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
        

        public static void AddItem(IItem item, int quantity = 1)
        {
            if (!instance.index.ContainsKey(item))
                instance.index.Add(item, new InventorySlot(item, 0));

            instance.index[item].Quantity += quantity;
        }


        public static void RemoveItem(IItem item, int quantity = 1)
        {
            if (!instance.index.ContainsKey(item))
                return;

            instance.index[item].Quantity -= quantity;
            if (instance.index[item].Quantity < 1)
                instance.index.Remove(item);
        }

        public static int GetItemQuantity(IItem item)
        {
            if (!instance.index.ContainsKey(item))
                return 0;
            
            return instance.index[item].Quantity;
        }

    }


    [Serializable]
    public class InventorySlot
    {
        public IItem Item;
        public int Quantity;


        public InventorySlot(IItem item, int quantity)
        {
            this.Item = item;
            this.Quantity = quantity;
        }
    }
}