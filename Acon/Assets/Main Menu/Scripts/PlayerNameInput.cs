using System;

using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;

[RequireComponent(typeof(InputField))]
public class PlayerNameInput : MonoBehaviour
{
    private const string PlayerNamePrefKey = "playerName";

    private void Start()
    {
        var inputField = this.GetComponent<InputField>();
        if (inputField == null) throw new Exception("No player name input field found");

        var playerName = string.Empty;
        if (PlayerPrefs.HasKey(PlayerNamePrefKey))
        {
            playerName = PlayerPrefs.GetString(PlayerNamePrefKey);
            inputField.text = playerName;
        }

        PhotonNetwork.NickName = playerName;
    }

    public void SetPlayerName(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            Debug.LogError("Player Name is null or empty");
            return;
        }

        PhotonNetwork.NickName = value;

        PlayerPrefs.SetString(PlayerNamePrefKey, value);
    }
}
