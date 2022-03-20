using UnityEngine;
using Photon.Pun;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject point1;
    public GameObject point2;
    public GameObject player;
    void Start()
    {
        SpawnSoldier();
    }
    public void SpawnSoldier()
    {   
        PhotonNetwork.Instantiate(player.name,player.transform.position,player.transform.rotation);
    }
    
}
