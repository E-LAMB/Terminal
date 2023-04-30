using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    public Transform self;
    public Transform sprite;

    public SpriteRenderer my_renderer;

    public Sprite sprite_sideways;
    public Sprite sprite_upways;
    public Sprite sprite_downways;

    public Rigidbody2D my_body;

    public float movement_speed;

    public Vector3 scale;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        /*
        emission_distance = Vector3.Distance(self.position, emission_location);
        if (emission_distance > 0f)
        {
            emission_location = self.position;
            my_emitter.Play();
        }
        */

        my_body.velocity = new Vector3 (movement_speed * Input.GetAxis("Horizontal"), movement_speed * Input.GetAxis("Vertical"), 0f);
    
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            scale.x = Input.GetAxisRaw("Horizontal");
        } 

        sprite.localScale = scale;

    }
}
