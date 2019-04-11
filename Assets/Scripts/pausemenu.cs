using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pausemenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("space was pressed");
            if (!this.enabled)
                this.enabled = true;
            else
                this.enabled = false;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("space was pressed");
            if (!this.enabled)
                this.enabled = true;
            else
                this.enabled = false;

        }
    }
}
