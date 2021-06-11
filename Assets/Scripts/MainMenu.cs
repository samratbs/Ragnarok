using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string versionName = "0.1";


    [Header("UI")]
    [SerializeField] private GameObject firstPagePanel = null;
    [SerializeField] private GameObject lastPagePanel = null;
    [SerializeField] TMP_InputField usernameInput = null;
    [SerializeField] TMP_InputField createInput = null;
    [SerializeField] private Button Startbutton = null;
    [SerializeField] private Button createButton = null;
    [SerializeField] private Button joinButton = null;

    void Awake()
    {
        PhotonNetwork.ConnectUsingSettings(versionName);
    }

    void Start()
    {
        firstPagePanel.SetActive(true);
        usernameInput.text = "Player";
        createInput.text = "Room";
    }
    private void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }
    public void HostLobby()
    {
        PhotonNetwork.CreateRoom(createInput.text, new RoomOptions() { maxPlayers = 2 }, null);
    }

    public void JoinLobby()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.maxPlayers = 2;
        PhotonNetwork.JoinOrCreateRoom(createInput.text, roomOptions, TypedLobby.Default);
    }


    public void ValidateUsername()
    {
        if (usernameInput.text.Length < 1 )
        {
            Startbutton.interactable = false;
        }
        else
        {
            Startbutton.interactable = true;
        }
    }
    public void ValidateLobby()
    {
        if (createInput.text.Length < 1)
        {
            createButton.interactable = false;
            joinButton.interactable = false;
        }
        else
        {
            createButton.interactable = true;
            joinButton.interactable = true;
        }
    }

    public void Back()
    {
        firstPagePanel.SetActive(true);
        lastPagePanel.SetActive(false);
    }

    public void Setname()
    {
        firstPagePanel.SetActive(false);
        lastPagePanel.SetActive(true);

        PhotonNetwork.playerName = usernameInput.text;
    }

    public void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("PlayScene");
    }
}
