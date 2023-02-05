using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;
    public float cameraHeight = 0f;
    private void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(player.position.x, player.position.y + cameraHeight, -10f);
    }
}
