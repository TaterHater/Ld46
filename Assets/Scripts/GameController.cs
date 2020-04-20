using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    public GameObject player;
    NBodySimulation nBody;
    public Text message;
    public List<MainPlanet> planets;
    int victoryPoints;
    public int SecondsToEnd;
    public Text TimeLeft;
    public GameObject EndPanel;
    public string SceneName;
    void Start()
    {
        nBody = this.GetComponent<NBodySimulation>();
        var temp = FindObjectsOfType<MainPlanet>();
        TimeLeft.text = "Time Remaining: " + SecondsToEnd;
        foreach (var i in temp)
        {
            if (!i.isPlayer)
            {
                planets.Add(i);
                  Debug.Log(i.gameObject.name);
            }

        }
        Debug.Log(planets.Count);
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        while (SecondsToEnd >= 0)
        {
            if (nBody.speed > 0)
            {
                SecondsToEnd--;
                TimeLeft.text = "Time Remaining: " + SecondsToEnd;
            }
            yield return new WaitForSeconds(1);

        }

    }
    public List<MainPlanet> GetEnemies()
    {
        return planets;
    }
    public void LoadNextScene(){

        SceneManager.LoadScene(SceneName);
    }

    void Update()
    {
        foreach (var i in planets)
        {
            if (i.health <= 0 && i.active)
            {
                victoryPoints++;
                i.active =false;
            }
        }
        if (victoryPoints >= planets.Count && SceneName !="Main")
        {
            //player wins, pause game and load next level
            nBody.speed = 0;
            message.text = "Enemy Ship has been disabled!";
            EndPanel.SetActive(true);
        }
        if (SecondsToEnd < 0)
        {
            nBody.speed = 0;
            message.text = "Time has Expired. Please press F3 to Restart.";
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

}
