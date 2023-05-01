using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    public Transform self;
    public Transform sprite;

    public GameObject checker;

    public Rigidbody2D my_body;

    public float movement_speed;
    public float climb_speed;

    public Vector3 scale;

    public LayerMask ladder_layer;
    public bool on_ladder;

    public LayerMask ground_layer;
    public bool on_ground;

    public float scale_x;

    public float player_health;
    public float i_frames;

    public Transform health_bar;
    public Vector3 health_bar_size;
    public float initial_size;
    public float max_health;

    public bool is_cloaked;

    public void PlayerTakesDamage(float damage_taken, float granted_i)
    {
        if (i_frames < 0f)
        {
            i_frames = granted_i;
            player_health -= damage_taken;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        health_bar_size = health_bar.localScale;
        initial_size = health_bar_size.x;
    }

    // Update is called once per frame
    void Update()
    {

        player_health -= Time.deltaTime / 1.2f;

        if (player_health > max_health)
        {
            player_health = max_health;
        }

        health_bar_size.x = initial_size * (player_health / max_health);

        health_bar.localScale = health_bar_size;

        /*
        emission_distance = Vector3.Distance(self.position, emission_location);
        if (emission_distance > 0f)
        {
            emission_location = self.position;
            my_emitter.Play();
        }
        */

        i_frames -= Time.deltaTime;
    
        on_ladder = Physics2D.OverlapCircle(checker.transform.position, 0.1f, ladder_layer);
        on_ground = Physics2D.OverlapCircle(checker.transform.position, 0.3f, ground_layer);

        if (!on_ladder) {on_ladder = Physics2D.OverlapCircle(self.transform.position, 0.3f, ladder_layer);}

        my_body.velocity = new Vector3 (movement_speed * Input.GetAxis("Horizontal"), my_body.velocity.y, 0f);

        if (on_ladder)
        {
            if (on_ground)
            {
                my_body.velocity = new Vector3 (movement_speed * Input.GetAxis("Horizontal"), climb_speed * Input.GetAxis("Vertical"), 0f);
            } else
            {
                my_body.velocity = new Vector3 ((movement_speed / 1.2f) * Input.GetAxis("Horizontal"), climb_speed * Input.GetAxis("Vertical"), 0f);
            }

            my_body.gravityScale = 0;
        } else
        {
            my_body.gravityScale = 1.5f;
        }

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            scale.x = Input.GetAxisRaw("Horizontal") * scale_x;
        } 

        sprite.localScale = scale;

    }
}
