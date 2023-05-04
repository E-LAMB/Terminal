using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    public bool is_active;

    public float activation_times;
    public float damage;
    float timer;

    public AnimationCurve damage_curve;
    public AnimationCurve activation_curve;

    public ParticleSystem parts;

    public SpawnRooms spawner;
    public PlayerControls player_script;

    public BoxCollider2D my_col;

    // Start is called before the first frame update
    void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnRooms>();
        activation_times = activation_curve.Evaluate(spawner.current_room);
        damage = damage_curve.Evaluate(spawner.current_room);
        player_script = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log(other.tag);
        if (other.tag == "Player")
        {
            // Debug.Log("Hit");
            player_script.PlayerTakesDamage(damage, 0.05f);
            is_active = false;
            timer = -5f;
            parts.Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        my_col.enabled = is_active;

        if (timer > activation_times && !is_active)
        {
            is_active = true;
            timer = 0f;
            parts.Play();
        }
        if (timer > 1f && is_active)
        {
            is_active = false;
            timer = 0f;
            parts.Stop();
        }
    }
}
