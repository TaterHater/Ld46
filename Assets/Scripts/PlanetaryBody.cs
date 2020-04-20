using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetaryBody : MonoBehaviour
{
    public float mass;
    public float radius;
    public Vector3 initialVel;
    public Vector3 currentVel;
    public float rotationSpeed;
    public bool IsTrajectory;
    public LineRenderer trajectory;
    public int LineLength = 50;
    void Awake()
    {
        currentVel = initialVel;
        trajectory.positionCount = LineLength;
    }

    public void UpdateVel(PlanetaryBody[] planetaries, float timeStep)
    {
        foreach (var other in planetaries)
        {
            if (other != this)
            {
                float sqrDist = (other.GetComponent<Rigidbody>().position - this.GetComponent<Rigidbody>().position).sqrMagnitude;
                Vector3 forceDir = (other.GetComponent<Rigidbody>().position - this.GetComponent<Rigidbody>().position).normalized;
                Vector3 force = forceDir * 0.02f * mass * other.mass / sqrDist;
                Vector3 acceleration = force / mass;
                currentVel += acceleration * timeStep;
                if (IsTrajectory) drawLine(acceleration, timeStep);
            }
        }
    }
    public void UpdatePosition(float timeStep)
    {
        this.GetComponent<Rigidbody>().position += currentVel * timeStep;
        transform.Rotate(0, Time.deltaTime * rotationSpeed, 0, Space.World);

    }

    void drawLine(Vector3 force, float timeStep)
    {
        var pos = this.GetComponent<Rigidbody>().position;
        var vel = currentVel;
        for (var i = 0; i < LineLength; i++)
        {

            vel = vel + force * timeStep;
            pos = pos + vel * timeStep;
            trajectory.SetPosition(i, pos);
        }

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag != "projectile")
        {
            var controller = FindObjectOfType<NBodySimulation>();
            var colPlanet = col.gameObject.GetComponent<PlanetaryBody>();
            Debug.Log(colPlanet.mass);
            if (colPlanet.mass < mass)
            {
                var temp = col.gameObject.GetComponent<MeshRenderer>();
                controller.UpdateBodies(colPlanet);
            }

        }


    }

}
