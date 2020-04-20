using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NBodySimulation : MonoBehaviour
{
    List<PlanetaryBody> bodies;
    public float speed = 20;

    void Awake()
    {
        bodies = FindObjectsOfType<PlanetaryBody>().ToList();
        //Time.fixedDeltaTime = 0.04f;
    }
    public void UpdateBodies(PlanetaryBody b)
    {
        bodies.Remove(b);

    }
    public void AddBodies(PlanetaryBody b)
    {
        bodies.Add(b);

    }


    void FixedUpdate()
    {
        foreach (var body in bodies)
        {
            try
            {
                body.UpdateVel(bodies.ToArray(), Time.deltaTime * speed);
            }
            catch (UnityException e)
            {
                Debug.Log(e.Message);
            }

        }
        foreach (var body in bodies)
        {

            try
            {
                body.UpdatePosition(Time.deltaTime * speed);
            }
            catch (UnityException e)
            {
                Debug.Log(e.Message);
            }
        }
    }
}
