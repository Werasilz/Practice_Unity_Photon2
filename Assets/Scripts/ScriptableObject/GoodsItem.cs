using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class GoodsItem : Item
{
    public void Init(Item newItem)
    {
        Id = newItem.Id;
        name = newItem.ItemName + "#" + this.GetInstanceID().ToString();
    }

    public void Init(int itemId, string itemName)
    {
        Id = itemId;
        name = itemName + "#" + this.GetInstanceID().ToString();
    }
}
