using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlacement : MonoBehaviour
{
    [SerializeField] private float dropChance = 0.5f;
    [SerializeField] private ItemPickup[] itemPrefabs;

    private void Start()
    {
        if (Random.value <= dropChance)
        {
            ItemPickup itemPrefab = itemPrefabs[Random.Range(0, itemPrefabs.Length)];
            Instantiate(itemPrefab, transform.position, Quaternion.identity);
        }
    }
}
