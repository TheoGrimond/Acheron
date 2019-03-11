using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
       player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position;
    }
}
