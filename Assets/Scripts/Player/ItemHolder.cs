using Photon.Pun;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    private PhotonView photonView;
    private Interaction interaction;

    [Header("Item Objects")]
    [SerializeField] private GameObject[] itemObjects;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        interaction = GetComponent<Interaction>();
    }

    public void SetActiveItemObject(bool isActive)
    {
        // Enable item object
        if (interaction.HoldingItem != null)
        {
            itemObjects[interaction.HoldingItem.Id].SetActive(isActive);
        }

        photonView.RPC("RPC_SyncActiveItem", RpcTarget.Others, isActive);
    }

    [PunRPC]
    private void RPC_SyncActiveItem(bool isActive)
    {
        SetActiveItemObject(isActive);
    }
}
