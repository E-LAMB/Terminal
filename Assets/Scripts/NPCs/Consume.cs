using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consume : MonoBehaviour
{

    public GameObject player_object;
    public PlayerControls player_script;
    public Transform player_trans;

    public GameObject unconsumed_effects;
    public SpriteRenderer unconsumed_renderer;

    public float to_restore;
    public bool consumed;
    public bool can_be_consumed;

    public ParticleSystem to_play;

    public float player_distance;

    public GameObject to_destroy;

    public Transform self;

    // Start is called before the first frame update
    void Start()
    {
        player_object = GameObject.FindGameObjectWithTag("Player");
        player_script = player_object.GetComponent<PlayerControls>();
        player_trans = player_object.GetComponent<Transform>();
    }

    public void Consumed()
    {
        if (unconsumed_effects != null)
        {
            unconsumed_effects.SetActive(false);
        }
        if (unconsumed_renderer != null)
        {
            unconsumed_renderer.enabled = false;
        }

        consumed = true;
        to_play.Play();
        player_script.ConsumedBeing(to_restore);
        Destroy(to_destroy, 1f);
    }

    // Update is called once per frame
    void Update()
    {

        player_distance = Vector3.Distance(player_trans.position, self.position);

        if (can_be_consumed && player_distance < 1f && Input.GetKeyDown(player_script.key_interact) && !consumed)
        {
            Consumed();
        }

    }
}
