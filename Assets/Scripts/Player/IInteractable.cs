using Photon.Pun;

public interface IInteractable
{
    void Interact(Interaction interaction, ItemHolder itemHolder, PhotonView photonView);
}
