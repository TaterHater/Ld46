using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class createProjectile : MonoBehaviour
{
    public GameObject projectile;
    public Transform spawnLocation;
    public GameObject SolarPanels;
    public GameObject RepairDepot;
    public float initialVelocityMultiplier;
    public MainPlanet MainVessel;
    PlanetaryBody planetaryBody;
    NBodySimulation controller;
    public AudioSource fireSound;
    public Slider powerMeter;

    public GameObject target;
    public enum types
    {
        cannon,
        repair,
        energy
    };
    public string type;
    public float power = 50;
    void Start()
    {
        planetaryBody = this.GetComponent<PlanetaryBody>();
        controller = FindObjectOfType<NBodySimulation>();
    }
    public void increasePower(float add)
    {
        if (power < 50)
            power += add;
    }
    public void UpdateTarget(GameObject t)
    {
        target = t;

    }
    public void SetType(string t)
    {
        type = t;
        if (t == "Energy Multiplier")
        {
            SolarPanels.SetActive(true);
        }
        if (t == "Repair Station")
        {
            RepairDepot.SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (powerMeter != null)
        {
            powerMeter.value = power / 5;
        }
        if (type == "Energy Multiplier")
            MainVessel.powerOutput = 6;

        if (Input.GetKeyDown(KeyCode.X) && type == "Laser Cannon")
        {
            Fire(target.transform);
        }
        if (Input.GetKeyDown(KeyCode.C) && type == "Repair Station")
        {
            HealPlanet();
        }

    }

    public void HealPlanet()
    {
        if (power > 20 && controller.speed > 0)
        {
            power -= 20;
            MainVessel.Heal(20);
        }
    }
    public void Fire(Transform t)
    {
        if (power > 5 && controller.speed > 0)
        {
            transform.LookAt(t);
            var temp = Instantiate(projectile, spawnLocation.position, transform.rotation);
            temp.transform.LookAt(t);
            temp.GetComponent<Rigidbody>().AddForce((spawnLocation.position - t.position).normalized * initialVelocityMultiplier * -100);
            power -= 5;
            fireSound.Play();
        }

    }
}
