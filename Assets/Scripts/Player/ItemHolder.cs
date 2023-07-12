using UnityEngine;

public class ItemHolder : MonoBehaviour
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
