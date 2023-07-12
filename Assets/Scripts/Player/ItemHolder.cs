using Photon.Pun;
using UnityEngine;

public class ItemHolder : MonoBehaviourPunCallbacks
{
    private Interaction interaction;

    [Header("Item Objects")]
    [SerializeField] private GameObject[] itemObjects;

    private void Start()
    {
        interaction = GetComponentInParent<Interaction>();
    }

    public void SetActiveItemObject(bool isActive)
    {
        // Enable item object
        itemObjects[interaction.HoldingItem.Id].SetActive(isActive);
    }
}
