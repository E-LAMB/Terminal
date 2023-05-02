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
        self.localRotation = new Quaternion (self.localRotation.x * -1f, 0f, 0f, self.localRotation.w);
    }

    // Update is called once per frame
    void Update()
    {
        self.position = Vector3.MoveTowards(self.position, aim.position, 15f * Time.deltaTime);
    }
}
