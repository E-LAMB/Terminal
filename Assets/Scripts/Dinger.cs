using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dinger : MonoBehaviour
{

    public GameObject[] objects; 

    public AnimationCurve spawn_rates;
    public SpawnRooms spawner;

    // Start is called before the first frame update
    void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnRooms>();

        if (Random.Range(1f,100f) > spawn_rates.Evaluate(spawner.current_room))
        {
            Destroy(gameObject.GetComponent<Dinger>());
        }
        
        GameObject selected;

        selected = objects[Random.Range(0, objects.Length)];

        if (selected != null)
        {
            selected.SetActive(true);
        }

        Destroy(gameObject.GetComponent<Dinger>());
    }

}
