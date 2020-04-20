using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPlanet : MonoBehaviour
{

    public float health = 100;
    public GameObject Particles;

    PlanetaryBody planetaryBody;
    GameController controller;
    CameraLookAt cameraController;
    public GameObject Enemy;
    public List<GameObject> moons;
    public float powerOutput = 2;
    public Slider slider = null;
    public bool isPlayer;

    public int toggleEnemy = 0;
    public bool active = true;
    void Start()
    {
        controller = FindObjectOfType<GameController>();
        planetaryBody = this.GetComponent<PlanetaryBody>();
        cameraController = FindObjectOfType<CameraLookAt>();
        if (Enemy != null && !isPlayer)
        {
            StartCoroutine(AIFire());
        }
    }
    IEnumerator AIFire()
    {
        while (true)
        {
            yield return new WaitForSeconds(2 + (int)Random.Range(0, 4));
            foreach (var moon in moons)
            {
                moon.GetComponent<createProjectile>().Fire(Enemy.transform);
            }
        }
    }
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            UpdateTarget();
        }
        foreach (var m in moons)
        {
            m.gameObject.GetComponent<createProjectile>().increasePower(Time.deltaTime * powerOutput);
        }
          slider.value = health;
        if(health <= 0){
            planetaryBody.mass = 100;
        }
    }
    void UpdateTarget()
    {
        var targets = controller.GetEnemies();
        if (toggleEnemy == targets.Count-1)
        {
            toggleEnemy = 0;
            cameraController.target = targets.ToArray()[targets.Count - 1].gameObject;
            foreach(var m in moons){
                m.GetComponent<createProjectile>().UpdateTarget(targets.ToArray()[targets.Count - 1].gameObject);
            }
        }

        else
        {
            cameraController.target = targets.ToArray()[toggleEnemy].gameObject;
            foreach(var m in moons){
                m.GetComponent<createProjectile>().UpdateTarget(targets.ToArray()[toggleEnemy].gameObject);

            }
            toggleEnemy++;
        }

    }
    public void Heal(int amount){
        if (health < 100){
            health+=amount;
            if(health > 100){
                health = 100;
            }
              slider.value = health;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag != "projectile")
        {
            var controller = FindObjectOfType<NBodySimulation>();
            var colPlanet = col.gameObject.GetComponent<PlanetaryBody>();
         
            if (colPlanet.mass < planetaryBody.mass)
            {
                var temp = col.gameObject.GetComponent<MeshRenderer>();
                controller.UpdateBodies(colPlanet);
            }

        }
        else
        {
            Instantiate(Particles, col.gameObject.transform.position, Quaternion.identity);
            health -= col.gameObject.GetComponent<Rigidbody>().velocity.magnitude / 1000;
            slider.value = health;
        }

    }
}
