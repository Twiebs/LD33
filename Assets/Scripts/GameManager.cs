using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {
    public GameObject beginPanel;
    public GameObject victoryPanel;
    public GameObject defeatPanel;
    public GameObject spawner;

    public Text distanceText;
    public Transform warhead;
    public Transform target;

    void Update() {
        float dist = (target.position.x - warhead.position.x);
        distanceText.text = "KM to Target: " + dist;
        if (dist < 300) {
            distanceText.color = Color.red;
        }
    }

    public void Begin() {
        beginPanel.SetActive(false);
        //Start Music
    }

    public void Victory() {
        spawner.SetActive(false);
        victoryPanel.SetActive(true);
    }

    public void Defeat() {
        spawner.SetActive(false);
        defeatPanel.SetActive(true);
    }

    public void MainMenu() {
        Application.LoadLevel(1);
    }

    public void RestartLevel() {
        Application.LoadLevel(1);
    }

    public void Exit() {
        Application.LoadLevel(0);
    }
}
