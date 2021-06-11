using UnityEngine.UI;
using UnityEngine;
using System.IO;
using TMPro;

public class GameManager : Photon.MonoBehaviour
{

    [SerializeField] private GameObject controls;
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject sceneCamera;
    [SerializeField] private TextMeshProUGUI pingtext;
    [SerializeField] private GameObject Pausemenu;
    [SerializeField] private GameObject win;
    [SerializeField] private GameObject lose;


    public GameObject PlayerPrefab;

    // Start is called before the first frame update


    void Start()
    {
        controls.SetActive(false);
        sceneCamera.SetActive(true);
    }

    void FixedUpdate()
    {
        pingtext.text = "PING: " + PhotonNetwork.GetPing();
    }

    public void EnablePause()
    {
        Pausemenu.SetActive(true);
        controls.SetActive(false);
    }

    public void DisablePause()
    {
        Pausemenu.SetActive(false);
        controls.SetActive(true);
    }
    public void PlayGame()
    {
        controls.SetActive(true);
        sceneCamera.SetActive(false);
        startPanel.SetActive(false);
        SpawnPlayer();
    }
    public void SpawnPlayer()
    {
        float randomValue = Random.Range(-1f, 1f);
        PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Player"), new Vector3(this.transform.position.x * randomValue, 1f, this.transform.position.z * randomValue), Quaternion.identity, 0);
    }
    public void Disconnect()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("Menu");
    }

    public void DeadControls()
    {
        controls.SetActive(false);
    }

    public void DeadCamera()
    {
        sceneCamera.SetActive(true);
    }

    public void wintext()
    {
       win.SetActive(true);
    }

    public void losetext()
    {
        lose.SetActive(true);
    }

    
}
