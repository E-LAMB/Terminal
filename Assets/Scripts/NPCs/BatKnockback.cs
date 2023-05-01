using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatKnockback : MonoBehaviour
{
   
    public Enemy_Batter my_master;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            my_master.DoKnockback();
        }
    }

}
