using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hud : MonoBehaviour
{
    GameObject player;
    public Image clubicon;
    public Image gunicon;
    public Image healthbar;
    public Image shieldbar;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        healthbar.fillAmount = player.GetComponent<Combat>().health / player.GetComponent<Combat>().max_health;
        shieldbar.fillAmount = player.GetComponent<Combat>().shield / player.GetComponent<Combat>().max_shield;

        if (player.GetComponent<Combat>().weaponToggle == "Melee") {
            clubicon.enabled = true;
            gunicon.enabled = false;
        }
        else {
            gunicon.enabled = true;
            clubicon.enabled = false;
        }







    }
}
