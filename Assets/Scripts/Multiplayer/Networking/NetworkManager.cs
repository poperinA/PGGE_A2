using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

namespace PGGE.Multiplayer
{
    public class NetworkManager : MonoBehaviourPunCallbacks
    {
        private const string gameVersion = "1";

        [Header("Connection Settings")]
        [SerializeField] private byte maxPlayersPerRoom = 5;
        [SerializeField] private Text joinButtonText;
        [SerializeField] private GameObject backButton;


        private bool isConnecting = false;

        private void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        public void Connect()
        {
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                isConnecting = PhotonNetwork.ConnectUsingSettings();
                joinButtonText.text = "Connecting...";
                backButton.SetActive(false);
                PhotonNetwork.GameVersion = gameVersion;
            }
        }

        public override void OnConnectedToMaster()
        {
            if (isConnecting)
            {
                Debug.Log("OnConnectedToMaster() was called by PUN");
                PhotonNetwork.JoinRandomRoom();
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

        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.LogWarningFormat("OnDisconnected() was called by PUN with reason {0}", cause);
            isConnecting = false;
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("OnJoinRandomFailed() was called by PUN. " +
                "No random room available" +
                ", so we create one by Calling: " +
                "PhotonNetwork.CreateRoom");

            PhotonNetwork.CreateRoom(null,
                new RoomOptions
                {
                    MaxPlayers = maxPlayersPerRoom
                });
        }

        public void ToMenu()
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
