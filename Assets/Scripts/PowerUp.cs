using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    public GameObject player_object;
    public PlayerControls player_script;
    public Transform player_trans;

    public bool consumed;
    public bool can_be_consumed;

    public ParticleSystem to_play;

    public float player_distance;

    public GameObject to_destroy;

    public Transform self;

    public int power_ID;

    // Start is called before the first frame update
    void Start()
    {
        power_ID = Random.Range(1, 7);
        player_object = GameObject.FindGameObjectWithTag("Player");
        player_script = player_object.GetComponent<PlayerControls>();
        player_trans = player_object.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        player_distance = Vector3.Distance(player_trans.position, self.position);

        if (can_be_consumed && player_distance < 1f && Input.GetKeyDown(player_script.key_interact) && !consumed)
        {
            consumed = player_script.PickUpAbility(power_ID);
            if (consumed)
            {
                consumed = true;
                to_play.Play();
                Destroy(to_destroy, 1f);
            }
        }

    }
}
