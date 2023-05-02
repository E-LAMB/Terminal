using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Gunner : MonoBehaviour
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

    public Transform bat_sizer;

    public float bat_time;

    public Transform bat_holder;
    public Transform ideal_bat_position;

    public AnimationCurve bat_swing;

    public float directional_multiplier;

    float old_x_scale;

    public PlayerControls the_player_script;

    public float bat_damage;

    public EntityHealth my_health;
    public Consume my_consume;

    public GameObject bat_collective;

    public bool has_sight;

    public Vector3 sight_direction;
    public float sight_distance;
    public LayerMask sight_player;

    public Vector3 gun_size;

    public SpawnRooms spawner;

    public Bullet the_bullet_script;
    public GameObject the_bullet_gameobject;
    public Transform bullet_hole;
    public Transform bullet_faceaway;
    public GameObject bullet_prefab;

    // Start is called before the first frame update
    void Start()
    {
        current_mode = 1;

        player = GameObject.FindGameObjectWithTag("Player");
        playertrans = player.GetComponent<Transform>();
        the_player_script = player.GetComponent<PlayerControls>();
        bat_time = 4f;
        old_x_scale = scale.x;

        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnRooms>();
        bat_damage += spawner.current_room / 8f;
    }

    void FireBullet()
    {
        if (bat_time > 1f)
        {
            bat_time = 0f;

            the_bullet_gameobject = Instantiate(bullet_prefab, bullet_hole.position, bullet_hole.localRotation);

            the_bullet_script = the_bullet_gameobject.GetComponent<Bullet>();

            the_bullet_script.faceaway = bullet_faceaway;


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

        sight_direction = self.position - playertrans.position;
        sight_distance = Vector3.Distance(self.position, playertrans.position);

        has_sight = Physics2D.Raycast(transform.position, sight_direction, sight_distance, sight_player);
        has_sight = !has_sight;

        if (my_health.is_dead && current_mode != 3)
        {
            current_mode = 3;
            my_consume.can_be_consumed = true;
            my_body.constraints = RigidbodyConstraints2D.None;
            my_body.velocity = new Vector2(2f, 2f);
            my_body.angularVelocity = 20f;
            bat_collective.SetActive(false);
        }

        scale = new Vector3 (old_x_scale * directional_multiplier, scale.y, scale.z);

        bat_holder.position = ideal_bat_position.position;

        gun_size = new Vector3 (1f, 1f * directional_multiplier, 1f);

        bat_sizer.localScale = gun_size;

        if (current_mode == 2)
        {
            if (has_sight)
            {
                speed = 0f;

                FireBullet();

                walk_to.transform.position = playertrans.position;

            } else
            {
                speed = 1.5f;
                walk_to.transform.position = playertrans.position;
            }

            // Debug.Log(Vector3.Distance(self.position, playertrans.position));

        }

        bat_time += Time.deltaTime;

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

            if (has_sight)
            {
                current_mode = 2;
            }

        }

        self.localScale = scale;

    }
}
