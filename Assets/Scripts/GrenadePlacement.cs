using UnityEngine;

public class GrenadePlacement : MonoBehaviour
{
    private GameObject currentPreviewInstance;
    public LayerMask placementLayer; // Layer for valid placements

    // Method to set the preview model
    public void SetPreviewModel(GameObject previewPrefab)
    {
        // Destroy existing preview if any
        if (currentPreviewInstance != null)
        {
            Destroy(currentPreviewInstance);
        }

        // Instantiate new preview instance based on selected grenade type
        currentPreviewInstance = Instantiate(previewPrefab);
        currentPreviewInstance.SetActive(false); // Hide initially
    }

    private void Update()
    {
        if (currentPreviewInstance != null && currentPreviewInstance.activeSelf)
        {
            UpdatePreviewPosition();

            if (Input.GetMouseButtonDown(0)) // Left-click to place grenade
            {
                PlaceGrenade();
            }
        }
    }

    public void ShowPreview()
    {
        if (currentPreviewInstance != null)
        {
            currentPreviewInstance.SetActive(true);
        }
    }

    public void HidePreview()
    {
        if (currentPreviewInstance != null)
        {
            currentPreviewInstance.SetActive(false);
        }
    }

    private void UpdatePreviewPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, placementLayer))
        {
            currentPreviewInstance.transform.position = hit.point;
            currentPreviewInstance.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
        }
    }

    private void PlaceGrenade()
    {
        if (currentPreviewInstance != null)
        {
            Instantiate(currentPreviewInstance.GetComponent<ItemData>().grenadePrefab, currentPreviewInstance.transform.position, currentPreviewInstance.transform.rotation);
            HidePreview(); // Hide preview after placing
        }
    }


}
