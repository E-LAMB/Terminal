using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRooms : MonoBehaviour
{

    public LayerMask player_layer;
    public bool player_is_close;
    public float spawn_range;

    public Transform self;

    public float spawn_cooldown;

    public int current_room;

    public string[] room_to_spawn;

    public GameObject safe_room;
    public GameObject[] empty_rooms;
    public GameObject[] filler_rooms;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void SpawnNewRoom()
    {
        if (spawn_cooldown < 0f)
        {

            if (room_to_spawn[current_room].Contains("E"))
            {
                Instantiate(empty_rooms[Random.Range(0, empty_rooms.Length)], self.position, self.localRotation);
            }

            if (room_to_spawn[current_room].Contains("F"))
            {
                Instantiate(filler_rooms[Random.Range(0, filler_rooms.Length)], self.position, self.localRotation);
            }

            if (room_to_spawn[current_room].Contains("S"))
            {
                Instantiate(safe_room, self.position, self.localRotation);
            }

            spawn_cooldown = 1f;

            current_room += 1;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (spawn_cooldown > -3f)
        {
            spawn_cooldown -= Time.deltaTime;
        }

        if (player_is_close)
        {
            SpawnNewRoom();
        }

        player_is_close = Physics2D.OverlapCircle(self.position, spawn_range, player_layer);

    }
}
