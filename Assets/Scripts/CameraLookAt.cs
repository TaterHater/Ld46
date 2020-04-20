using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAt : MonoBehaviour
{
    public GameObject target;

    void Update()
    {
      
        transform.LookAt((target.GetComponent<Rigidbody>().position));
    }
}
