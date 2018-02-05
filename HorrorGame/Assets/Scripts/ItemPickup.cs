using UnityEngine;

public class ItemPickup : Interactable {

    public Item item;

    public override void Interact()
    {
        base.Interact();

        PickUp();
		
    }
	void PickUp()
    {
        Debug.Log("Picking up Item");
        bool wasPickedUp = Inventory.instance.Add(item);
		gameObject.SendMessage ("Deactivate");
        if(wasPickedUp)
          Destroy(gameObject);
    }
}
