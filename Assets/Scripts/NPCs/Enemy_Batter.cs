using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Batter : MonoBehaviour
{

    public int current_mode;
    // 1 = Patrolling
    // 2 = Chasing

    public Rigidbody2D my_body;

    public Vector3 scale;

    public bool should_move_left;

    public float speed;

    public GameObject walk_to;

    public GameObject patrol_position_a;
    public GameObject patrol_position_b;

    public Transform self;

    public bool patrolling_to_a;

    public GameObject player;
    public Transform playertrans;

    public float bat_time;

    public Transform bat_holder;
    public Transform ideal_bat_position;

    public AnimationCurve bat_swing;

    public float directional_multiplier;

    float old_x_scale;

    public GameObject bat_collider;

    public PlayerControls the_player_script;

    public float bat_damage;

    // Start is called before the first frame update
    void Start()
    {
        current_mode = 1;

        player = GameObject.FindGameObjectWithTag("Player");
        playertrans = player.GetComponent<Transform>();
        the_player_script = player.GetComponent<PlayerControls>();
        bat_time = 4f;
        old_x_scale = scale.x;
    }

    void SwingBat()
    {
        if (bat_time > 1.6f)
        {
            bat_time = 0f;
        }
    }

    public void DoKnockback()
    {
        if (bat_time > 0f && bat_time < 1f)
        {
            the_player_script.PlayerTakesDamage(bat_damage, 0.2f);
        }
    }

    public void PlayerInSight()
    {
        if (!the_player_script.is_cloaked && current_mode == 1)
        {
            current_mode = 2;
            speed += 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (bat_time < 5f)
        {
            bat_time += Time.deltaTime * 3f;
            bat_collider.SetActive(true);

        } else
        {
            bat_collider.SetActive(false);
        }

        scale = new Vector3 (old_x_scale * directional_multiplier, scale.y, scale.z);

        bat_holder.localRotation = Quaternion.Euler(0f, 0f, directional_multiplier * bat_swing.Evaluate(bat_time));

        bat_holder.position = ideal_bat_position.position;

        if (walk_to.transform.position.x > self.position.x)
        {
            should_move_left = false;
            my_body.velocity = new Vector3 (speed, my_body.velocity.y, 0f);
        } else
        {
            should_move_left = true;
            my_body.velocity = new Vector3 (speed * -1f, my_body.velocity.y, 0f);
        }

        if (should_move_left)
        {
            directional_multiplier = -1f;
        } else
        {
            directional_multiplier = 1f;
        }

        if (current_mode == 2)
        {
            walk_to.transform.position = playertrans.position;

            // Debug.Log(Vector3.Distance(self.position, playertrans.position));

            if (Vector3.Distance(self.position, playertrans.position) < 0.7f)
            {
                SwingBat();
            }
        }

        if (current_mode == 1)
        {

            if (patrolling_to_a)
            {
                walk_to.transform.position = patrol_position_a.transform.position;
            } else
            {
                walk_to.transform.position = patrol_position_b.transform.position;
            }

            // Debug.Log(Mathf.Abs(walk_to.transform.position.x - self.position.x));

            if (Mathf.Abs(walk_to.transform.position.x - self.position.x) < 0.1f)
            {
                patrolling_to_a = !patrolling_to_a;
            }

        }

        self.localScale = scale;

    }
}