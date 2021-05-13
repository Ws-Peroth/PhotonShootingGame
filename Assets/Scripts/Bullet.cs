using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Bullet : MonoBehaviour
{
    public GameObject MyPlayer;
    public int checkPlayer;
    int dir;

    void Update()
    {
        transform.Translate(Vector3.right * NetworkManager.networkManager.bulletSpeed * Time.deltaTime * dir);
    }

    void OnTriggerEnter2D(Collider2D col) // col을 RPC의 매개변수로 넘겨줄 수 없다
    {
        if (col.CompareTag("Ground")) {
            CallDestroyBullet();
        }

        if (col.CompareTag("Player") && col.GetComponent<PhotonView>().IsMine) // 느린쪽에 맞춰서 Hit판정
        {
            if (checkPlayer != col.GetComponent<Player>().id)
            {
                print("chectPlayer : " + checkPlayer + "\nHitPlayerID : " + col.GetComponent<Player>().id);
                col.GetComponent<Player>().Hit();
                CallDestroyBullet();
            }
        }
    }

    public void SetDir(int dir)
    {
        this.dir = dir;
    }

    public void CallDestroyBullet()
    {
        ObjectManager.objectManager.DestroyBullet(gameObject);
        // Destroy(this.gameObject);
    }
}
