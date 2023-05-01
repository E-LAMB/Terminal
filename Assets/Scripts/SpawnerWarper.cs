using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerWarper : MonoBehaviour
{

    public Transform the_spawner;

    // Start is called before the first frame update
    void Start()
    {
        the_spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Transform>();
        the_spawner.position = gameObject.transform.position;
        Destroy(gameObject.GetComponent<SpawnerWarper>());
    }

}
