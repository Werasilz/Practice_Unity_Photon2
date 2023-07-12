using UnityEngine;

public class ItemInteract : MonoBehaviour, IInteractable
{
    public Item item;

    public void Interact(Interaction interaction, ItemHolder itemHolder)
    {
        // New Item Instance
        GoodsItem newGoodsItem = ScriptableObject.CreateInstance<GoodsItem>();
        newGoodsItem.Init(item);

        // Set holding item
        interaction.SetHolderItem(newGoodsItem);
        itemHolder.SetActiveItemObject(true);
        Destroy(gameObject);
    }
}