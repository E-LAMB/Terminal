using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerControls : MonoBehaviour
{

    public KeyCode key_attack;
    public KeyCode key_interact;
    public KeyCode key_power;

    public Transform self;
    public Transform sprite;

    public int rooms_cleared;

    public GameObject checker;

    public Rigidbody2D my_body;

    public SpriteRenderer my_renderer;

    public float last_direction_faced;

    public float movement_speed;
    float normal_movement_speed;
    public float climb_speed;
    float normal_climbing_speed;

    public Vector3 scale;

    public LayerMask ladder_layer;
    public bool on_ladder;

    public LayerMask capsule_layer;
    public bool in_capsule;

    public LayerMask ground_layer;
    public bool on_ground;

    public float scale_x;

    public float player_health;
    public float i_frames;

    public Transform health_bar;
    public Vector3 health_bar_size;
    public float initial_size;
    public float max_health;

    public float health_drain;

    public float attack_time;
    public float base_damage;
    public float player_damage;

    public bool is_cloaked;

    public TextMeshProUGUI score_text;

    public GameObject attack_barrier;

    public float ability_time;

    public float dash_direction;

    public bool can_attack;

    public GameObject pounce_attack;
    public GameObject screech_attack;

    public AbilityIcon my_icon;

    public PhysicsMaterial2D my_dead_mat;

    public float frenzy_restoration;

    public int ability_state;
    // 0 = No Ability
    // 1 = Ability Ready
    // 2 = Ability In Use (Ability time will go up)

    public int current_ability;
    // 0 = None
    // 1 = Dash
    // 2 = Pounce
    // 3 = Screech
    // 4 = Frenzy
    // 5 = Cloak
    // 6 = Sustinance
    // 7 = Harden
    // 8 = Conserve
    // 9 = ?

    public void ConsumedBeing(float to_restore)
    {
        player_health += to_restore;

        if (current_ability == 4 && ability_state == 2)
        {
            ability_time += frenzy_restoration;

            if (frenzy_restoration > 0f)
            {
                frenzy_restoration -= 0.5f;
            }

            if (ability_time > 9f)
            {
                ability_time = 9f;
            }
        }
    }

    public void PlayerTakesDamage(float damage_taken, float granted_i)
    {
        if (i_frames < 0f)
        {
            if (current_ability == 5 && ability_state == 2) {ability_time = -1f;}
            i_frames = granted_i;
            player_health -= damage_taken;
        }
    }

    public bool PickUpAbility(int ability_ID)
    {
        if (current_ability == 0 || ability_state == 0)
        {
            current_ability = ability_ID;
            ability_state = 1;
            return true;
        }

        if (ability_ID == current_ability)
        {
            return false;
        }

        if (ability_state == 1 && ability_ID != current_ability)
        {
            current_ability = ability_ID;
            return true;
        }

        return false;
    }

    void StartAttack()
    {
        attack_time = 0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        normal_movement_speed = movement_speed;
        normal_climbing_speed = climb_speed;
        attack_time = 5f;
        health_bar_size = health_bar.localScale;
        initial_size = health_bar_size.x;
        health_drain = 2f;
        can_attack = true;
        base_damage = player_damage;
    }

    // Update is called once per frame
    void Update()
    {

        if (is_cloaked)
        {
            my_renderer.color = new Vector4 (0f, 1f, 0.5f, 0.5f);
        } else
        {
            my_renderer.color = new Vector4 (0f, 1f, 0f, 1f);
        }
        
        if (ability_state == 2 && ability_time > -1f)
        {

            ability_time -= Time.deltaTime;

        }

        player_damage = base_damage;

        if (ability_state == 0 && current_ability != 0)
        {
            ability_state = 1;
        }

        if (ability_state == 1 && Input.GetKeyDown(key_power))
        {
            ability_state = 2;

            if (current_ability == 1) {ability_time = 0.5f;}
            if (current_ability == 1) {can_attack = false;}

            if (current_ability == 2) {ability_time = 0.2f;}
            if (current_ability == 2) {pounce_attack.SetActive(true);}
            if (current_ability == 2) {can_attack = false;}

            if (current_ability == 3) {ability_time = 0.5f;}
            if (current_ability == 3) {screech_attack.SetActive(true);}

            if (current_ability == 4) {ability_time = 9f; }
            if (current_ability == 4) {frenzy_restoration = 5.5f;}

            if (current_ability == 5) {ability_time = 20f;}
            if (current_ability == 5) {is_cloaked = true;}

            if (current_ability == 6) {ability_time = 0f;}
            if (current_ability == 6) {player_health += 255f;}

            // if (current_ability == 7) {ability_time = 3f;}

            // if (current_ability == 8) {ability_time = 5f;}

            my_icon.PowerBegin(current_ability, ability_time);

        }

        if (ability_state == 2 && current_ability == 7)
        {
            i_frames = 0.1f;
        }

        if (ability_state == 2 && current_ability == 4)
        {
            player_damage = base_damage * 3f;
        }

        if (ability_state == 2 && 0f > ability_time)
        {

            if (current_ability == 1) {can_attack = true;}
            if (current_ability == 2) {can_attack = true;}
            if (current_ability == 2) {pounce_attack.SetActive(false);}
            if (current_ability == 3) {screech_attack.SetActive(false);}
            if (current_ability == 5) {is_cloaked = false;}

            ability_state = 0;
            current_ability = 0;
        }

        if (attack_time > 1f && can_attack)
        {
            if (Input.GetKeyDown(key_attack))
            {
                StartAttack();
            }
        }

        if (attack_time < 3f)
        {
            attack_time += Time.deltaTime * 1.5f;
        }

        if (current_ability == 4 && ability_state == 2)
        {

            if (attack_time < 0.65f)
            {
                movement_speed = normal_movement_speed / 2f;
                climb_speed = normal_climbing_speed / 2f;
            } else
            {
                movement_speed = normal_movement_speed * 1.4f;
                climb_speed = normal_climbing_speed * 1.4f;
            }

        } else if (current_ability == 5 && ability_state == 2)
        {

            if (attack_time < 0.65f)
            {
                movement_speed = normal_movement_speed / 4f;
                climb_speed = normal_climbing_speed / 4f;
            } else
            {
                movement_speed = normal_movement_speed * 1.25f;
                climb_speed = normal_climbing_speed * 1.25f;
            }

        } else
        {

            if (attack_time < 0.65f)
            {
                movement_speed = normal_movement_speed / 5f;
                climb_speed = normal_climbing_speed / 5f;
            } else
            {
                movement_speed = normal_movement_speed * 1.1f; 
                climb_speed = normal_climbing_speed * 1.1f;
            }

        }


        if (attack_time > 0.4f && attack_time < 0.6f)
        {
            attack_barrier.SetActive(true);
            if (current_ability == 5 && ability_state == 2) {ability_time = -1f;}

        } else
        {
            attack_barrier.SetActive(false);
        }

        score_text.text = "SCORE: " + rooms_cleared.ToString();

        if (ability_state == 2 && current_ability == 8)
        {
            player_health -= Time.deltaTime * 0.5f;
        } else
        {
            player_health -= Time.deltaTime * health_drain;
        }

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

        in_capsule = Physics2D.OverlapCircle(checker.transform.position, 0.3f, capsule_layer);

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

        if (current_ability == 1 && ability_state == 2) // Dash Ability
        {
            i_frames = 0.05f;
            my_body.velocity = new Vector3 (movement_speed * 3.5f * dash_direction, 0f, 0f);
        } else if (current_ability == 2 && ability_state == 2) // Pounce Ability
        {
            my_body.velocity = new Vector3 (movement_speed * 4.5f * dash_direction, 0f, 0f);
        } else
        {
            dash_direction = last_direction_faced;
        }

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            scale.x = Input.GetAxisRaw("Horizontal") * scale_x;
            last_direction_faced = Input.GetAxisRaw("Horizontal");
        } 

        sprite.localScale = scale;

        if (in_capsule)
        {
            player_health += 25f * Time.deltaTime;
        }
        if (player_health > max_health)
        {
            player_health = max_health;
        }

        if (player_health <= 0f)
        {
            my_body.sharedMaterial = my_dead_mat;
            my_body.constraints = RigidbodyConstraints2D.None;
            is_cloaked = true;
            my_body.gravityScale = 1.5f;
            my_renderer.color = new Vector4 (0.5f, 0.5f, 0.5f, 1f);
            screech_attack.SetActive(false);
            pounce_attack.SetActive(false);
            health_bar.localScale = new Vector3 (0f, 0f, 0f);
            attack_barrier.SetActive(false);
            gameObject.GetComponent<PlayerControls>().enabled = false;
        }
    }
}
