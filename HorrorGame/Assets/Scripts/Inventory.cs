using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    public int space = 10;
    public List<Item> items = new List<Item>();
    public static Inventory instance;

    private void Awake()
    {
        instance = this;
    }

    public bool Add(Item item)
    {
        if (items.Count >= space)
        {
            Debug.Log("Not enough space");
            return false;
        }
        items.Add(item);
        if(onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
        return true;
    
    }

    public void Remove (Item item)
    {
		item.Respawn (Camera.main.transform);
        items.Remove(item);
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}
