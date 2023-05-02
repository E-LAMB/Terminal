using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Transform self;
    public Transform aim;

    public Transform faceaway;

    // Start is called before the first frame update
    void Start()
    {
        self.LookAt(faceaway);
        self.localRotation = Quaternion.Euler(self.localRotation.x + 180f, self.localRotation.y + 180f, self.localRotation.z); 
    }

    // Update is called once per frame
    void Update()
    {
        self.position = Vector3.MoveTowards(self.position, aim.position, 10f * Time.deltaTime);
    }
}
