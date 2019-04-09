using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public float health = 400f;
    float shield = 0f;
    float blockProb = 0f;
    float meleeDmgMin, meleeDmgMax;
    float attackCooldown = 0.5f;
    float lastAttack = 0f;
    Combat enemy;

    public void SetEnemy(GameObject target)
    {
        enemy = target.GetComponent<Combat>();
    }

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

    public void AttackAttempt()
    {
        // add blocking
        enemy.TakesHit();
        lastAttack = Time.time + attackCooldown;
    }

    void TakesHit()
    {
        health -= 10.0f;
        Debug.Log(health);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (health == 0f)
        {
            this.gameObject.SetActive(false);
        }
    }
}
