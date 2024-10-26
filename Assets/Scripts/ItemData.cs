using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public string itemName;
    public float radius;
    public float force;
    public GameObject explosionEffect;
    public float price;
}
