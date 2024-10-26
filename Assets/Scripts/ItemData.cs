using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public string itemName;
    public float radius;
    public float force;
    public GameObject grenadePrefab; // Reference to the actual grenade prefab
    public GameObject explosionEffect; // Separate explosion effect
    public GameObject previewPrefab; // Add this field for the preview model
    public float price;
}
