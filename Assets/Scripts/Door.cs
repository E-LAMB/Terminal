using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public bool player_is_close;
    public Transform self;
    public GameObject door;
    public LayerMask player_layer;
    public PlayerControls player_script;

    public GameObject active_button;
    public bool has_activated;

    // Start is called before the first frame update
    void Start()
    {
        player_script = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
    }

    // Update is called once per frame
    void Update()
    {
        player_is_close = Physics2D.OverlapCircle(self.position, 1f, player_layer);

        if (player_is_close && Input.GetKeyDown(player_script.key_interact) && !has_activated)
        {
            door.SetActive(false);
            has_activated = true;
            active_button.SetActive(false);
        }
    }
}
