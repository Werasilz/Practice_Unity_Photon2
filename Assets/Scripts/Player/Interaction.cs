using Photon.Pun;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    private ControlsInput _input;
    private ItemHolder _itemHolder;

    public Item HoldingItem;

    [Header("Raycast Settings")]
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private float sphereRadius = 0.75f;

    private void Start()
    {
        _input = GetComponent<ControlsInput>();
        _itemHolder = GetComponentInChildren<ItemHolder>();
    }

    void Update()
    {
        if (_input.interact)
        {
            // Find all colliders within a sphere around this object
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, sphereRadius, targetLayer);

            // Initialize variables for tracking the closest collider and its distance
            float closestDistance = Mathf.Infinity;
            Collider closestCollider = null;

            // Iterate through all the colliders found
            foreach (var hitCollider in hitColliders)
            {
                // Calculate the distance between this object and the current collider
                float distance = Vector3.Distance(transform.position, hitCollider.transform.position);

                // If this collider is closer than the previous closest one, update the variables
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestCollider = hitCollider;
                }
            }

            // If a closest collider was found, do something with it
            if (closestCollider != null)
            {
                // Get the script from the object that the ray hits.
                closestCollider.gameObject.TryGetComponent<ItemInteract>(out var itemInteract);
                closestCollider.gameObject.TryGetComponent<DeliverStation>(out var deliverStation);

                if (itemInteract != null && HoldingItem == null)
                {
                    itemInteract.Interact(this, _itemHolder);
                }

                if (deliverStation != null && HoldingItem != null)
                {
                    deliverStation.Interact(this, _itemHolder);
                }
            }
        }
    }

    public void SetHolderItem(Item item)
    {
        if (item != null)
        {
            HoldingItem = item;
        }
        else
        {
            Destroy(item);
            HoldingItem = null;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        // Draw the sphere cast gizmo in the Scene view.
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sphereRadius);
    }
#endif
}