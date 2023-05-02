using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerWarper : MonoBehaviour
{

    public Transform the_spawner;

    public int to_destroy_at;
    public SpawnRooms the_spawner_component;

    public GameObject my_room_head;

    // Start is called before the first frame update
    void Start()
    {
        the_spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Transform>();
        the_spawner_component = GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnRooms>();
        the_spawner.position = gameObject.transform.position;
        to_destroy_at = the_spawner_component.current_room + 45;
    }

    void Update()
    {
        if (the_spawner_component.current_room > to_destroy_at)
        {
            Destroy(my_room_head);
        }
    }

}
