using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell
{
    string name;
    List<bool> LevelsUnlocked;
    List<int> LevelsCost;
    float cooldown;
    int currentLvl;
    public float lastUse = 0f;
    public bool isActive = false;

    public Spell(string n, List<bool> LU, List<int> LC, float cd)
    {
        name = n;
        LevelsUnlocked = LU;
        LevelsCost = LC;
        cooldown = cd;
        currentLvl = 1;                                     // CHANGE THIS VALUE TO 0, 1 IS FOR TESTING
    }

    public float GetCooldown()
    {
        return cooldown;
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
