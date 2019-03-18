using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //parameters
    float speed = 15.0f;
    float t2t = 2.0f;
    float nearRadius = 3.0f;
    float slowdownRadius = 7.0f;
    float meleeRange = 5.0f;
    float rangedRange = 20.0f;

    float distanceFromTarget;
    Vector3 moveDirection;

    //arrive at cursor
    public void Arrive(Vector3 target, GameObject player)
    {
       
        distanceFromTarget = (target - player.transform.position).magnitude;
        moveDirection = (target - player.transform.position).normalized;
        if (distanceFromTarget < nearRadius)
        {
            player.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
        }
        
        else if (distanceFromTarget < slowdownRadius)
        {
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.LookRotation(moveDirection), 0.15F);
            float velocityMagnitude = Mathf.Min(speed, (Vector3.Distance(target, player.transform.position)) / t2t);
            player.GetComponent<Rigidbody>().velocity = ((target - player.transform.position).normalized * velocityMagnitude);
        }
        else
        {
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.LookRotation(moveDirection), 0.15F);
            player.GetComponent<Rigidbody>().velocity = ((target - player.transform.position).normalized * speed);
        }
    }

    void Pursue(GameObject target, GameObject player)
    {
        distanceFromTarget = (target.transform.position - player.transform.position).magnitude;
        if (distanceFromTarget < meleeRange)
        {
            //attack with melee
        }
        else if (distanceFromTarget < rangedRange)//&& ranged weapon chosen
        {
            //attack with ranged weapon
        }
        //if too far, move towards target
        else
        {
            float timeToTarget = distanceFromTarget / speed;
            Vector3 predictedTargetPoint = target.transform.position + target.GetComponent<Rigidbody>().velocity * timeToTarget;
            Seek(predictedTargetPoint, player);
        }
    }

    void Seek(Vector3 targetPosition, GameObject player)
    {
        distanceFromTarget = (targetPosition - player.transform.position).magnitude;
        Vector3 moveDirection = (targetPosition - player.transform.position).normalized;
        transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.LookRotation(moveDirection), 0.15F);
        GetComponent<Rigidbody>().velocity = ((targetPosition - player.transform.position).normalized * speed);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
