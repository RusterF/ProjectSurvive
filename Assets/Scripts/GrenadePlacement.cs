using UnityEngine;

public class GrenadePlacement : MonoBehaviour
{
    public GameObject previewPrefab; // Prefab for the preview model
    private GameObject currentPreview; // Instance of the preview model
    private Vector3 placementPosition; // Position where the grenade will be placed

    public LayerMask placementLayer; // Set this in the inspector

    void Update()
    {
        if (currentPreview != null)
        {
            // Update the preview position based on mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f, placementLayer))
            {
                placementPosition = hit.point; // Set the placement position to the hit point
                currentPreview.transform.position = placementPosition; // Move the preview
            }
        }
    }

    public void SetPreviewModel(GameObject prefab)
    {
        if (currentPreview != null)
        {
            Destroy(currentPreview); // Remove the existing preview model
        }

        currentPreview = Instantiate(prefab); // Create a new preview model
        currentPreview.layer = LayerMask.NameToLayer("Default"); // Ensure it's on the correct layer
    }

    public void ShowPreview()
    {
        if (currentPreview != null)
        {
            currentPreview.SetActive(true); // Show the preview
        }
    }

    public void HidePreview()
    {
        if (currentPreview != null)
        {
            currentPreview.SetActive(false); // Hide the preview
        }
    }

    public Vector3 GetPlacementPosition()
    {
        return placementPosition; // Return the current placement position
    }
}
