using Photon.Pun;
using UnityEngine;

public class DeliverStation : MonoBehaviour, IInteractable
{
    public void Interact(Interaction interaction, ItemHolder itemHolder, PhotonView photonView)
    {
        itemHolder.SetActiveItemObject(false);
        interaction.SetHoldingItem(null);
        PlayerManager.Find(photonView.Owner).GetScore();
    }
}
