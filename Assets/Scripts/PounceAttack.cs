using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PounceAttack : MonoBehaviour
{

    public PlayerControls player_script;
    public float my_damage;

    public bool manual_damage;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            //Debug.Log("In Trigger " + other);
            other.GetComponent<EntityHealth>().TakeDamage(my_damage);
            other.GetComponent<Consume>().Consumed();
            player_script.ability_time = -0.5f;
        }
    }

    void Update()
    {
        if (!manual_damage)
        {
            my_damage = player_script.player_damage;
        }
    }

}
