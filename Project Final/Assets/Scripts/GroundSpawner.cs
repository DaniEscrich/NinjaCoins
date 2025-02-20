using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject groundTilePrefab;

    Vector3 nextSpawnPoint;

    public void spawnTile()
    {
        GameObject tempGround = Instantiate(groundTilePrefab, nextSpawnPoint, Quaternion.identity);
        nextSpawnPoint = tempGround.transform.GetChild(1).transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i<10; i++){
            spawnTile();
        }
    }

}
