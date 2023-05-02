using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScorer : MonoBehaviour
{

    public PlayerControls player_script;
    public bool cleared;

    // Start is called before the first frame update
    void Start()
    {
        player_script = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !cleared)
        {
            cleared = true;
            player_script.rooms_cleared += 1;
            Destroy(gameObject);
        }
    }
    
}
