using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    //Store player prefab
    public GameObject playerPrefab;

    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    private void Start()
    {
        //spawn location
        Vector3 spawnPosition = new Vector3(Random.Range(minX, maxX), 2.25f, Random.Range(minZ, maxZ));
        //instantiate object
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPosition, Quaternion.identity);
    }

}
