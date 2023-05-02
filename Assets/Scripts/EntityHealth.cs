using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    
    public float health;

    public float current_i_frames;
    public float usual_i_frames;

    public bool is_dead;
    public bool taken_damage;

    public void TakeDamage(float amount_taken)
    {
        if (current_i_frames < 0f)
        {
            taken_damage = true;
            health -= amount_taken;
            current_i_frames = usual_i_frames;
        }
    }

    void Update()
    {
        if (0f >= health)
        {
            is_dead = true;
        }

        if (current_i_frames > -2f)
        {
            current_i_frames -= Time.deltaTime;
        }
    }

}
