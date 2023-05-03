using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Transform self;
    public Transform aim;

    public float directions;
    public float my_damage;

    public LayerMask wall_layer;
    public LayerMask player_layer;

    public PlayerControls player_control;

    // public ParticleSystem my_destruction;

    // Start is called before the first frame update
    void Start()
    {
        self.eulerAngles = new Vector3 (90f + Random.Range(-1.5f, 1.5f), 90f, directions);
        player_control = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Physics2D.OverlapCircle(self.position, 0.1f, player_layer))
        {
            // my_destruction.Play();
            player_control.PlayerTakesDamage(my_damage, 0.1f);
            Destroy(gameObject);
        }

        if (Physics2D.OverlapCircle(self.position, 0.1f, wall_layer))
        {
            // my_destruction.Play();
            Destroy(gameObject);
        }

        self.position = Vector3.MoveTowards(self.position, aim.position, 50f * Time.deltaTime);
    }
}
