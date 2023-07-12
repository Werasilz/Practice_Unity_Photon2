using UnityEngine;

public class DeliverStation : MonoBehaviour, IInteractable
{
    public void Interact(Interaction interaction, ItemHolder itemHolder)
    {
        itemHolder.SetActiveItemObject(false);
        interaction.SetHoldingItem(null);
        print("Score +1");
    }
}
