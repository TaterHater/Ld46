using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoonController : MonoBehaviour
{

    public float value;
    public List<Dropdown> moonValues;
    public List<createProjectile> moons;
    NBodySimulation sim;
    public Button submit;
    public GameObject startWindow;
    public List<Sprite> moonIcons;
    public List<Button> moonButtons;
    public enum types
    {
        cannon,
        repair,
        energy
    };
    void Start()
    {

        sim = FindObjectOfType<NBodySimulation>();
        sim.speed = 0;
    }

    public void OnSubmit()
    {
        var i = 0;
        foreach (var m in moons)
        {
            m.SetType(moonValues[i].options[moonValues[i].value].text);
            if (m.type == "Laser Cannon")
            {
                moonButtons[i].image.sprite = moonIcons[0];
            }
            else if (m.type == "Repair Station")
            {
                moonButtons[i].image.sprite = moonIcons[1];
            }
            else
            {
                moonButtons[i].image.sprite = moonIcons[2];
            }

            i++;
        }

        sim.speed = 20;
    }
}
