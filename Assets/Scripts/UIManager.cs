using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

    public void LoadWorld() {
        Application.LoadLevel(1);
    }

    public void Exit() {
        Application.Quit();
    }

}
