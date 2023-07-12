using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class GoodsItem : Item
{
    public void Init(Item newItem)
    {
        Id = newItem.Id;
        name = newItem.name + "#" + this.GetInstanceID().ToString();
    }
}
