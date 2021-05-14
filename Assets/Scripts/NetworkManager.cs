using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager networkManager = null;

    public InputField nickNameInput;
    public GameObject disconnectPanel;
    public GameObject respawnPanel;
    public GameObject howToPlayPanel;
    public GameObject panelBundle;
    public Transform mainCameraParent;

    public GameObject PlayerUI;
    public Image HpBarUI;
    public Text HpText;

    public int bulletSpeed;
    public float shotDelay;

    void Awake()
    {
        bulletSpeed = 8;
        shotDelay = 0.2f;

        if (networkManager == null)
        {
            networkManager = this;
        }

        Screen.SetResolution(960, 540, false);
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;

        panelBundle.SetActive(true);
        disconnectPanel.SetActive(true);
        respawnPanel.SetActive(false);
        howToPlayPanel.SetActive(false);
        PlayerUI.SetActive(false);
    }

    public void Connect() => PhotonNetwork.ConnectUsingSettings();

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.LocalPlayer.NickName = nickNameInput.text;
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 20 }, null);
    }

    public override void OnJoinedRoom()
    {
        disconnectPanel.SetActive(false);
        StartCoroutine(nameof(DestroyBullet));
        Spawn();
    }


    IEnumerator DestroyBullet()
    {
        yield return null;

        foreach (GameObject GO in GameObject.FindGameObjectsWithTag("Bullet"))
        {
            // GO.GetComponent<PhotonView>().RPC(nameof(Bullet.CallDestroyBullet), RpcTarget.All);
            ObjectManager.objectManager.DestroyBullet(GO);
        }

    }

    public void Spawn()
    {
        GameObject player = PhotonNetwork.Instantiate(nameof(Player), new Vector3(1.5f, 20, 0), Quaternion.identity);
        CameraMove.cameraMoveManager.player = player;

        respawnPanel.SetActive(false);
        PlayerUI.SetActive(true);
        HpBarUI.fillAmount = 1f;
    }

    void Update() {

        if (Input.GetKeyDown(KeyCode.Escape) && PhotonNetwork.IsConnected)
        {
            SettingObjectsBeforeDisconnect();
            PhotonNetwork.Disconnect();
        }
    }

    public void SettingObjectsBeforeDisconnect()
    {
        PlayerUI.SetActive(false);
        Camera.main.transform.localPosition = new Vector3(0, 0, -10);
        panelBundle.SetActive(true);
        respawnPanel.SetActive(true);
        disconnectPanel.SetActive(false);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        disconnectPanel.SetActive(true);
        respawnPanel.SetActive(false);
    }
    
    public void OpenHowToPlay()
    {
        howToPlayPanel.SetActive(true);
    }
    public void ExitHowToPlay()
    {
        howToPlayPanel.SetActive(false);
    }

    public void SetPlayerHp()
    {
        HpBarUI.fillAmount -= 0.1f;
        HpText.text = (int)(HpBarUI.fillAmount * 100) + " / 100";
    }
}
