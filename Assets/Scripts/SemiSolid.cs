using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemiSolid : MonoBehaviour
{

    public PlayerControls player_script;
    public GameObject player_object;
    public Transform self;
    public GameObject my_solid;

    public bool player_inside;

    // Start is called before the first frame update
    void Start()
    {
        player_object = GameObject.FindGameObjectWithTag("Player");
        player_script = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player_object.transform.position.y > self.position.y)
        {
            if (Input.GetKey(KeyCode.S))
            {
                my_solid.SetActive(false); 
            } else
            {
                if (!player_inside)
                {
                    my_solid.SetActive(true);
                }
            }
        }

        if (player_object.transform.position.y < self.position.y)
        {
            if (Input.GetKey(KeyCode.W) || player_script.on_ladder)
            {
                my_solid.SetActive(false); 
            } else
            {
                if (!player_inside)
                {
                    my_solid.SetActive(true);
                }
            }
        }
    }
}
