﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //parameters
    float speed = 25.0f;
    float t2t = 1.4f;
    float nearRadius = 10f;
    float slowdownRadius = 15f;
    float meleeRadius = 7f;
    float rangedRadius = 30.0f;
    public bool inMeleeRange = false;
    float distanceFromTarget;
    Vector3 moveDirection;
    Quaternion lookWhereYoureGoing;
    public bool hasTarget = false;
    public bool moveToCursor = false;
    public Animator animator;

    GameObject playerGO;
    Rigidbody playerRB;

    // Public function that checks if player is in Melee Range of target
    public bool InMeleeRange(Vector3 target)
    {
        distanceFromTarget = (target - playerGO.transform.position).magnitude;
        if (distanceFromTarget < nearRadius) // meleeRadius == nearRadius (can be changed later on)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool InRangedRange(Vector3 target)
    {
        distanceFromTarget = (target - playerGO.transform.position).magnitude;
        if (distanceFromTarget < rangedRadius)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Arrive towards the position of the target
    public void Arrive(Vector3 target)
    {
        //Debug.Log("Distance:" + (target - playerGO.transform.position).magnitude);
        distanceFromTarget = (target - playerGO.transform.position).magnitude;
        moveDirection = (target - playerGO.transform.position).normalized;

        if (moveToCursor)
        {
            nearRadius = 6f;
            slowdownRadius = 6f;
        }
        else
        {
            nearRadius = 6.0f;
            slowdownRadius = 6.0f;
        }

        if (distanceFromTarget < nearRadius)
        {
            //Debug.Log("Distance:" + (target - player.transform.position).magnitude);
            //Vector3 goalFacing = new Vector3(playerRB.velocity.x, 0.0f, playerRB.velocity.z).normalized;
            //lookWhereYoureGoing = Quaternion.LookRotation(goalFacing, Vector3.up);
            //player.transform.rotation = player.transform.rotation;
            
            playerRB.velocity = new Vector3(0.0f, 0.0f, 0.0f);
            
            animator.SetFloat("Speed", new Vector3(0.0f, 0.0f, 0.0f).magnitude);
            Debug.Log("PlayerRBRot:" + playerRB.rotation);
            inMeleeRange = true;
            if (moveToCursor)
            {
                // Delete Cursor
                Destroy(GameObject.Find("Cursor(Clone)"));
            }
            //hasTarget = false;
        }

        else if (distanceFromTarget < slowdownRadius)
        {
            lookWhereYoureGoing = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z), Vector3.up);
            playerGO.transform.rotation = Quaternion.RotateTowards(playerGO.transform.rotation, lookWhereYoureGoing, 7.5f);
            //player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.LookRotation(moveDirection), 0.15F);
            float velocityMagnitude = Mathf.Min(speed, (Vector3.Distance(new Vector3(target.x,0f,target.z), new Vector3(playerGO.transform.position.x, 0f, playerGO.transform.position.z))) / t2t);
            playerRB.velocity = (new Vector3(moveDirection.x, 0f, moveDirection.z).normalized * velocityMagnitude);
            animator.SetFloat("Speed", playerRB.velocity.magnitude);
            Debug.Log("PlayerRBRot:" + playerRB.rotation);
        }
        else
        {
            lookWhereYoureGoing = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z), Vector3.up);
            playerGO.transform.rotation = Quaternion.RotateTowards(playerGO.transform.rotation, lookWhereYoureGoing, 7.5f);
            //player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.LookRotation(moveDirection), 0.15F);
            playerRB.velocity = (new Vector3(moveDirection.x ,0f, moveDirection.z).normalized * speed);
            animator.SetFloat("Speed", playerRB.velocity.magnitude);
            Debug.Log("PlayerRBRot:" + playerRB.rotation);
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

    public void Seek(Vector3 targetPosition, GameObject player)
    {
        distanceFromTarget = (targetPosition - player.transform.position).magnitude;
        Vector3 moveDirection = (targetPosition - player.transform.position).normalized;
        //transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.LookRotation(moveDirection), 0.15F);
        this.GetComponent<Rigidbody>().velocity = (moveDirection * speed * Time.deltaTime);
    }

    // Start is called before the first frame update
    void Start()
    {
        playerGO = this.gameObject;
        Debug.Log("PlayerGameObject: " + playerGO);
        playerRB = playerGO.GetComponentInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
