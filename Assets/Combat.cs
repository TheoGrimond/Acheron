using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    // Character parameters
    public PlayerController PlayerController_script;
    public SpellBook SpellBook_script;
    // Character's Vitals
    public float health = 400f;
    public float max_health = 400f;  
    public float shield = 0f;
    public float max_shield = 0f;
    // Character's Combat probabilities
    public float dodgeProbability = 0f;
    public float blockProbability = 0f;
    public float critProbability = 0f;
    // Character's Max/Min damage range
    public float meleeDmgMin, meleeDmgMax = 0f;
    public float rangedDmgMin, rangedDmgMax = 0f;
    // Character's Single Attack parameters
    private float damage = 0f;
    private float attackCooldown = 0.5f;
    private float lastAttack = 0f;
    public Combat enemy;
    // Character's current weapon choice
    public string weaponToggle = "Melee";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0f)
        {
            //Debug.Log(enemy);
            enemy.ResetEnemy();
            this.gameObject.SetActive(false);
        }
    }

    // Get the equip currently equip
    public string GetCurrentWeapon()
    {
        return weaponToggle;
    }

    // Toggle between the character's 
    // Melee & Ranged weapons
    public void ToggleWeapon()
    {
        if (weaponToggle == "Melee")
        {
            weaponToggle = "Ranged";
        }
        else if (weaponToggle == "Ranged")
        {
            weaponToggle = "Melee";
        }
    }

    public Combat GetEnemy()
    {
        return enemy;
    }

    // Called when left clicking an enemy
    // Retrieves the Combat component of the enemy
    // To access combat parameters during combat
    public void SetEnemy(GameObject target)
    {
        enemy = target.GetComponent<Combat>();
        //Debug.Log(enemy);
    }

    // Called when clicking away from the enemy 
    // To reset the enemy parameter
    public void ResetEnemy()
    {
        enemy = null;
        
    }

    // Check if the character's attack cooldown is done
    public bool CanIAttack()
    {
        if (Time.time > lastAttack)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Apply damage taken to this character's health
    public void TakesHit(float dmg)
    {
        health -= dmg;
        Debug.Log(health);
    }

    // Apply damage taken to this character's shield
    public void TakesBlockedHit(float dmg)
    {
        shield -= dmg;
        Debug.Log(health);
    }

    public float RollDamage()
    {
        if (weaponToggle == "Melee")
        {
            return Random.Range(meleeDmgMin, meleeDmgMax);
        }
        else 
        {
            return Random.Range(rangedDmgMin, rangedDmgMax);
        }

    }

    public bool DodgeHit()
    {
        if (Random.Range(0f, dodgeProbability) == 0f)
        {
            Debug.Log("Hit dodged !!");
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool BlockHit()
    {
        if (Random.Range(0f, blockProbability) == 0f)
        {
            Debug.Log("Hit blocked !!");
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CriticalHit()
    {
        if (Random.Range(0f, critProbability) == 0f)
        {
            Debug.Log("Critical hit !!");
            return true;
        }
        else
        {
            return false;
        }
    }

    // Character tries to attack
    // Checks every possible outcome of a single attack:
    // 1. Enemy can Dodge or Block
    // 2. Character can deal Critical Damage
    public void AttackAttempt()
    {
        if (!enemy.DodgeHit()) // Checks enemy dodging
        {
            if (!enemy.BlockHit()) // Checks enemy blocking
            {
                // If hit isn't blocked
                // Check dmg amount and apply to enemy health

                // Check if this character deals critical damage
                if (SpellBook_script.InstaCrit.isActive == true && Time.time > SpellBook_script.InstaCrit.lastUse)
                {
                    if (weaponToggle == "Melee")
                    {
                        damage = meleeDmgMax + 5f;
                    }
                    else
                    {
                        damage = rangedDmgMax + 5f;
                    }
                    // Apply the damage to enemy
                    enemy.TakesHit(damage);
                    if (SpellBook_script.DoubleStrike.isActive == true && Time.time > SpellBook_script.DoubleStrike.lastUse)
                    {
                        enemy.TakesHit(damage);
                        Debug.Log("Double strike hit!");
                        SpellBook_script.DoubleStrike.isActive = false;
                        SpellBook_script.DoubleStrike.lastUse = Time.time + SpellBook_script.InstaCrit.GetCooldown();
                    }
                    lastAttack = Time.time + attackCooldown;
                    Debug.Log("InstaCrit hit! Damage dealt : "+ damage);
                    // Apply cooldown to Spell
                    SpellBook_script.InstaCrit.isActive = false;
                    SpellBook_script.InstaCrit.lastUse = Time.time + SpellBook_script.InstaCrit.GetCooldown();
                }
                else if (CriticalHit())                                                    // <---------------------------   Add Insta Crit Spell check
                {
                    // Check which weapon is equipped 
                    // & applies its Maximum Damage
                    if (weaponToggle == "Melee")
                    {
                        damage = meleeDmgMax + 5f;
                    }
                    else
                    {
                        damage = rangedDmgMax + 5f;
                    }
                    // Apply the damage to enemy
                    enemy.TakesHit(damage);
                    if (SpellBook_script.DoubleStrike.isActive == true && Time.time > SpellBook_script.DoubleStrike.lastUse)
                    {
                        enemy.TakesHit(damage);
                        Debug.Log("Double strike hit!");
                        SpellBook_script.DoubleStrike.isActive = false;
                        SpellBook_script.DoubleStrike.lastUse = Time.time + SpellBook_script.InstaCrit.GetCooldown();
                    }
                    lastAttack = Time.time + attackCooldown;
                }
                else
                {
                    // apply regular damage
                    damage = RollDamage();
                    if (SpellBook_script.DoubleStrike.isActive == true && Time.time > SpellBook_script.DoubleStrike.lastUse)
                    {
                        enemy.TakesHit(damage);
                        Debug.Log("Double strike hit!");
                        SpellBook_script.DoubleStrike.isActive = false;
                        SpellBook_script.DoubleStrike.lastUse = Time.time + SpellBook_script.InstaCrit.GetCooldown();
                    }
                    enemy.TakesHit(damage);
                    lastAttack = Time.time + attackCooldown;
                }
            }
            else
            {
                // Hit was blocked
                // Apply the damage to the opponent's shield
                damage = RollDamage();
                if (SpellBook_script.DoubleStrike.isActive == true && Time.time > SpellBook_script.DoubleStrike.lastUse)
                {
                    enemy.TakesHit(damage);
                    Debug.Log("Double strike hit!");
                    SpellBook_script.DoubleStrike.isActive = false;
                    SpellBook_script.DoubleStrike.lastUse = Time.time + SpellBook_script.InstaCrit.GetCooldown();
                }
                enemy.TakesBlockedHit(damage);
                lastAttack = Time.time + attackCooldown;
            }
        }

    }
}
