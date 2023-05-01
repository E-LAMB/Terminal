using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemiSolidInsider : MonoBehaviour
{

    public SemiSolid my_solid;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            my_solid.player_inside = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            my_solid.player_inside = false;
        }
    }
}
