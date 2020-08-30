using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;
using System;

public class MainMenu : MonoBehaviourPunCallbacks
{
    public byte MaxPlayers = 4;
    public MenuPage[] Pages;
    public GameObject PlayPage;

    private void Awake()
    {
        foreach (var page in this.Pages)
        {
            page.SetupOnClick();
        }

        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void Connect()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = "0.0.0.1";
        }
        else
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master.");

        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log($"Disconnected. Reason: {cause}");

        this.DisableAllPages();
        this.PlayPage.SetActive(true);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No random room available. Creating a new room.");

        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = this.MaxPlayers });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room successfully.");
    }

    private void DisableAllPages()
    {
        foreach (var page in this.Pages)
        {
            page.Page.SetActive(false);
        }
    }
}

[Serializable]
public class MenuPage
{
    public GameObject Page;
    public MenuButton[] Buttons;   
    
    public void SetupOnClick()
    {
        foreach (var button in this.Buttons)
        {
            button.SetupListener();
            button.OnClick += this.ButtonPressed;
        }
    }

    private void ButtonPressed(GameObject pageToOpen)
    {
        pageToOpen.SetActive(true);
        this.Page.SetActive(false);
    }
}

[Serializable]
public class MenuButton
{
    public delegate void ButtonPress(GameObject pageToOpen);

    public Button Button;
    public event ButtonPress OnClick;
    public GameObject PageToOpen;

    public void SetupListener()
    {
        this.Button.onClick.AddListener(() => OnClick?.Invoke(this.PageToOpen));
    }
}