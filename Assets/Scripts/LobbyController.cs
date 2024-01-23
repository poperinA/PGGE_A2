using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;


namespace PGGE
{
    namespace Multiplayer
    {
        public class LobbyController : MonoBehaviourPunCallbacks
        {
            //reference to UI buttons
            public GameObject JoinBtn;
            public GameObject CreateBtn;

            //reference to lobby panels
            public GameObject JoinRoomPanel;
            public GameObject CreateRoomPanel;

            //reference to created room name by player
            public InputField roomInputField;

            public RoomItem roomItemPrefab;
            List<RoomItem> roomItemsList = new List<RoomItem>();
            public Transform contentObject;


            //shows join panel
            public void JoinPanel()
            {
                if (JoinRoomPanel.activeSelf == false)
                {
                    CreateRoomPanel.SetActive(false);
                    JoinRoomPanel.SetActive(true);
                }
            }

            //shows create panel
            public void CreatePanel()
            {
                if (CreateRoomPanel.activeSelf == false)
                {
                    JoinRoomPanel.SetActive(false);
                    CreateRoomPanel.SetActive(true);
                }
            }

            private void Start()
            {
                PhotonNetwork.JoinLobby();
            }

            public void OnClickCreate()
            {
                if (roomInputField.text.Length >= 1)
                {
                    PhotonNetwork.CreateRoom(roomInputField.text, new RoomOptions() { MaxPlayers = 5 });
                }
            }

            public override void OnJoinedRoom()
            {
                Debug.Log("OnJoinedRoom() called by PUN. Client is in a room.");
                if (PhotonNetwork.IsMasterClient)
                {
                    Debug.Log("We load the default room for multiplayer");
                    PhotonNetwork.LoadLevel("MultiplayerMap00");
                }
            }

            public override void OnRoomListUpdate(List<RoomInfo> roomList)
            {
                UpdateRoomList(roomList);
            }

            void UpdateRoomList(List<RoomInfo> list)
            {
                foreach (RoomItem item in roomItemsList)
                {
                    Destroy(item.gameObject);
                }
                roomItemsList.Clear();

                foreach (RoomInfo room in list)
                {
                    RoomItem newRoom = Instantiate(roomItemPrefab, contentObject);
                    newRoom.SetRoomName(room.Name);
                    roomItemsList.Add(newRoom);
                }
            }


        }
    }
}
