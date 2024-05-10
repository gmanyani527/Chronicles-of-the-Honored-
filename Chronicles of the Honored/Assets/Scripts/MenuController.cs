using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MenuController : MonoBehaviour
{
    public GameObject endPanel;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LoserGame()
    {
        endPanel.SetActive(true);
        endPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Game over...";
    }

    public void WinGame()
    {
        endPanel.SetActive(true);
        endPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "You Win";
    }

    internal object Move(object value)
    {
        throw new NotImplementedException();
    }
}
