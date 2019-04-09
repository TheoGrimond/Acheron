using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Animate();
    }

    void Animate()
    {
        this.transform.position = parent.transform.position;
        this.transform.position -= new Vector3(0f, 0.5f, 0.2f);
        //this.transform.rotation = parent.transform.rotation;
    }
}
