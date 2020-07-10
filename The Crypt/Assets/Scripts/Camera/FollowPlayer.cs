using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private GameObject player;
    private Vector3 cameraOffset;

    // Start is called before the first frame update
    void Start()
    {
        cameraOffset = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (player == null)
        {
            player = GameObject.Find("Player(Clone)");
        }
        transform.position = player.transform.position + cameraOffset;
    }
}
