using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Movement move_player;
    Vector3 target;
    //player parameters
    float max_health;
    float current_health;

    // Start is called before the first frame update
    void Start()
    {
        move_player = new Movement();
    }

    // Update is called once per frame
    void Update()
    {
        
        CheckForInput();

        //move char
        if (target != Vector3.zero)
        {
            //check if on the lava
            if (transform.position.y < 10.0f)
            {
                Dead();
            }
            //TODO detect what kind of object is the target (loot,enemy,terrain)
            move_player.Arrive(target, gameObject);
            //if enemy, can use the Pursue()
        }
    }

    //detect any inputs
    void CheckForInput()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000))
            {
                target = hit.point;
            }
        }
    }

    //player died, probs need something fancier
    void Dead()
    {
        Destroy(gameObject);
    }
}
