using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour
{
    PlanetaryBody planetaryBody;
    public GameObject target;

    void Start()
    {
     //   planetaryBody = this.GetComponent<PlanetaryBody>();
        StartCoroutine(tillDeath());
    }
    IEnumerator tillDeath()
    {
     
        while (true)
        {
            yield return new WaitForSeconds(6);
            var controller = FindObjectOfType<NBodySimulation>();
            controller.UpdateBodies(this.GetComponent<PlanetaryBody>());
            Destroy(this.gameObject);
        }
    }
    // void FixedUpdate()
    // {
    //     transform.LookAt(target.transform);
    //     this.GetComponent<Rigidbody>().AddForce(Vector3.forward*Time.deltaTime*30);
    // }
}
