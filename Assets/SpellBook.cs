using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBook : MonoBehaviour
{
    List<bool> LvlUnlocked = new List<bool>();
    List<int> LvlCost = new List<int>();
    public Spell InstaCrit;
    public Spell DoubleStrike;
    
    // Start is called before the first frame update
    void Start()
    {
        LvlUnlocked.Add(false);
        LvlUnlocked.Add(false);
        LvlUnlocked.Add(false);
        LvlCost.Add(5);
        LvlCost.Add(10);
        LvlCost.Add(20);
        InstaCrit = new Spell("InstaCrit", LvlUnlocked, LvlCost, 2f);
        DoubleStrike = new Spell("DoubleStrike", LvlUnlocked, LvlCost, 10f);
        //Debug.Log("SpellBook instantiated");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
