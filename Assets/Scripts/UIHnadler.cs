using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class UIHnadler : MonoBehaviourPunCallbacks
{
    public InputField createRoomTF;
    public InputField joinRoomTF;
    
    public void OnClick_CreateRoom()
    {
        PhotonNetwork.CreateRoom(createRoomTF.text,new RoomOptions{ MaxPlayers = 4},null);
    }

    public void OnClick_JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinRoomTF.text,null);
    }

    public override void OnJoinedRoom()
    {
        print("Room Join Success");
        PhotonNetwork.LoadLevel("Level101");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        print("Room Join Failed " + returnCode +"Message: "+message);
    }
}
