﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Movement Move_player;
    public Combat Combat_player;
    public SpellBook SpellBook_script;
    public Vector3 targetPosition;
    public GameObject targetObject;
    bool hasTarget = false;
    bool moveToCursor = false;
    bool singleSelect = false;
    //player parameters
    float max_health;
    float current_health;
    public GameObject cursor;
    

    // Start is called before the first frame update
    void Start()
    {
        //Move_player = new Movement();
        //Move_player.animator = this.gameObject.GetComponentInChildren<Animator>();
        //Combat_player = new Combat();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForInput();
        HandleInput();

        // Death Check
        if (targetPosition != Vector3.zero)
        {
            // Lava is below 5.0f
            if (transform.position.y < 5.0f)
            {
                Dead();
            }
        }
    }

    // Detects User Input
    void CheckForInput()
    {
        if (Input.GetMouseButton(0))
        {
            LeftClick();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("InstaCrit called !");
            if (SpellBook_script.InstaCrit.isActive == false && Time.time > SpellBook_script.InstaCrit.lastUse)
            {
                SpellBook_script.InstaCrit.isActive = true;
                Debug.Log("InstaCrit activated");
            }
            else
            {
                Debug.Log("InstaCrit called but cooldown refused activation");
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("DoubleStrike called !");
            if (SpellBook_script.DoubleStrike.isActive == false && Time.time > SpellBook_script.DoubleStrike.lastUse)
            {
                SpellBook_script.DoubleStrike.isActive = true;
                Debug.Log("DoubleStrike activated");
            }
            else
            {
                Debug.Log("DoubleStrike called but cooldown refused activation");
            }
        }
    }

    // Handles User Input
    void HandleInput()
    {
        //Debug.Log(hasTarget);
        //Debug.Log(Move_player.hasTarget);
        if (hasTarget)
        {
            if (!targetObject)
            {
                Move_player.Arrive(targetPosition);
                hasTarget = Move_player.hasTarget;
                // If hasTarget == false that means the Player has reached the Cursor
                // We now Reset the target parameters
                if (!hasTarget)
                {
                    ResetTarget();
                }
            }
            else
            {
                if (targetObject.tag == "AI")
                {
                    Move_player.Arrive(targetObject.transform.position);
                    if (Move_player.InRangedRange(targetObject.transform.position) == true && Combat_player.CanIAttack() == true && Combat_player.GetCurrentWeapon() == "Ranged")
                    {
                        Move_player.animator.SetBool("CanAttack", true);
                        Combat_player.AttackAttempt();
                    }
                    else if (Move_player.InMeleeRange(targetObject.transform.position) == true && Combat_player.CanIAttack() == true && Combat_player.GetCurrentWeapon() == "Melee")
                    {
                        Move_player.animator.SetBool("CanAttack", true);
                        Combat_player.AttackAttempt();
                    }
                    else if (hasTarget == true && Combat_player.GetEnemy() != null)
                    {
                        Move_player.Arrive(targetObject.transform.position);
                    }
                    else
                    {
                        ResetTarget();
                    }
                }
                /*if (targetObject.tag == "Loot")
                {
                    if ((targetObject.transform.position - this.transform.position).magnitude < closeLootRange)
                    {
                        Inventory_player.Loot();
                    }
                    else
                    {
                        Move_player.Arrive(targetObject.transform.position, gameObject);
                    }
                }*/
            }
        }
    }

    void LeftClick()
    {
        // Reset the click parameters
        ResetTarget();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f))
        {
            // If the player clicked on the ground
            if (hit.collider.tag == "Terrain")
            {
                targetPosition = hit.point;
                hasTarget = true;
                Move_player.hasTarget = true;
                moveToCursor = true;
                Move_player.moveToCursor = true;
                if (singleSelect == false)
                {
                    cursor.transform.position = new Vector3(targetPosition.x, targetPosition.y + 1.0f, targetPosition.z);
                    //Instantiate(cursor, new Vector3(targetPosition.x, targetPosition.y + 1.0f, targetPosition.z), cursor.transform.rotation);
                    singleSelect = true;
                }

            }
            // If the player clicked on an enemy
            else if (hit.collider.tag == "AI")
            {
                targetObject = hit.collider.gameObject;
                Combat_player.SetEnemy(targetObject);
                //Debug.Log(Combat_player.enemy);
                hasTarget = true;
                Move_player.hasTarget = true;
            }

        }
        // If hit.point.collider == Enemy + CloseRange, move and attack

        // If hit.point.collider == Enemy + LongRange, move then attack

        // If hit.point.collider == Enemy + Dead + CloseRange, loot

        // If hit.point.collider == Enemy + Dead + LongRange, move then loot

        // If hit.point.collider == Chest + CloseRange, move and loot (!= LongRange)
    }

    // Reset target parameters
    public void ResetTarget()
    {
        hasTarget = false;
        moveToCursor = false;
        singleSelect = false;
        cursor.transform.position = new Vector3(-200f, 0f, -200f);
        Move_player.moveToCursor = false;
        Move_player.hasTarget = false;
        Move_player.animator.SetBool("CanAttack", false);
        Combat_player.ResetEnemy();
        targetPosition = Vector3.zero;
        targetObject = null;
    }

    //player died, probs need something fancier
    void Dead()
    {
        Destroy(gameObject);
    }
}
