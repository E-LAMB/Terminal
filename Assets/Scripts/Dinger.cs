using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dinger : MonoBehaviour
{

    public GameObject object_a; 
    public GameObject object_b; // Needed if determining between two

    // Start is called before the first frame update
    void Start()
    {
        if (object_b == null)
        {
            if (Random.Range(1,3) == 1)
            {
                object_a.SetActive(true);
            } else
            {
                object_a.SetActive(false);
            }
        } else
        {
            if (Random.Range(1,3) == 1)
            {
                object_a.SetActive(true);
                object_b.SetActive(false);
            } else
            {
                object_a.SetActive(false);
                object_b.SetActive(true);
            }
        }

        Destroy(gameObject.GetComponent<Dinger>());
    }

}
