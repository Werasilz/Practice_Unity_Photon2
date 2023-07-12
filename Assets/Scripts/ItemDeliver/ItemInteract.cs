using Photon.Pun;
using UnityEngine;

public class ItemInteract : MonoBehaviour, IInteractable
{
    private PhotonView _photonView;
    public Item item;

    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
    }

    public void Interact(Interaction interaction, ItemHolder itemHolder, PhotonView photonView)
    {
        // Create new Item Instance
        GoodsItem newGoodsItem = ScriptableObject.CreateInstance<GoodsItem>();
        newGoodsItem.Init(item);

        // Set holding item
        interaction.SetHoldingItem(newGoodsItem);
        itemHolder.SetActiveItemObject(true);

        // Destroy Object
        Destroy(gameObject);
        _photonView.RPC("RPC_SyncDestroy", RpcTarget.All);
    }

    [PunRPC]
    private void RPC_SyncDestroy()
    {
        Destroy(gameObject);
    }
}