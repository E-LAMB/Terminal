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

    public Transform health_bar;
    public GameObject health_object;
    public Vector3 original_scale;
    public Vector3 new_scale;

    public float max_health_ever;

    public void TakeDamage(float amount_taken)
    {
        if (current_i_frames < 0f)
        {
            taken_damage = true;
            health -= amount_taken;
            current_i_frames = usual_i_frames;
        }
    }

    void Start()
    {
        original_scale = health_bar.localScale;
        new_scale = original_scale;
    }

    void Update()
    {
        if (health_bar != null)
        {
            new_scale.x = original_scale.x * (health / max_health_ever);
            if (new_scale.x > 5f)
            {
                new_scale.x = 5f;
            }
            health_bar.localScale = new_scale;
            if (is_dead)
            {
                health_object.SetActive(false);
            }
        }

        if (max_health_ever < health)
        {
            max_health_ever = health;
        }

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
