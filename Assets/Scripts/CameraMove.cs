using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public static CameraMove cameraMoveManager = null;
    public GameObject player = null;

    private void Start()
    {
        if (cameraMoveManager == null)
            cameraMoveManager = this;
    }

    void Update()
    {
        if (player != null)
        {
            float x = player.transform.localPosition.x;
            float y = player.transform.localPosition.y;
            Camera.main.transform.localPosition = new Vector3(x, y, -10f);
        }
        else
        {
            Camera.main.transform.localPosition = new Vector3(0, 0, -10f);
        }
    }
}
