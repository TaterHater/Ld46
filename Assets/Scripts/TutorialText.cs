using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialText : MonoBehaviour
{

    public List<string> messages;
    public Text text;
    int iter;
    void Start()
    {
        iter = 1;
        StartCoroutine(DisplayMessages());
        text.text = messages[0];
    }

    IEnumerator DisplayMessages()
    {

        while (iter < messages.Count)
        {
            yield return new WaitForSeconds(5);
            text.text = messages[iter];
             iter++;
        }


    }
}
