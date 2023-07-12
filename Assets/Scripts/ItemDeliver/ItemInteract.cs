using Photon.Pun;
using UnityEngine;

public class ItemInteract : MonoBehaviour, IInteractable
{
    private PhotonView photonView;
    public Item item;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    public void Interact(Interaction interaction, ItemHolder itemHolder)
    {
        // New Item Instance
        GoodsItem newGoodsItem = ScriptableObject.CreateInstance<GoodsItem>();
        newGoodsItem.Init(item);

        // Set holding item
        interaction.SetHolderItem(newGoodsItem);
        itemHolder.SetActiveItemObject(true);

        // Destroy Object
        if (photonView.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }

        photonView.RPC("RPC_Destroy", RpcTarget.All);
    }

    [PunRPC]
    private void RPC_Destroy()
    {
        Destroy(gameObject);
    }
}