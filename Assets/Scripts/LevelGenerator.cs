using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [ExecuteInEditMode]
public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPositions;
    [SerializeField] private GameObject[] roomPrefabs;
    [SerializeField] private Transform roomParent;

    private void Start()
    {
        SpawnRooms();
    }

    private void SpawnRooms()
    {
        foreach (Transform spawnPos in spawnPositions)
        {
            int randIdx = Random.Range(0, roomPrefabs.Length);
            GameObject roomPrefab = roomPrefabs[randIdx];
            Instantiate(roomPrefab, spawnPos.position, Quaternion.identity, roomParent);
        }
    }
}
