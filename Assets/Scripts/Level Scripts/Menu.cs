using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {


    void OnGUI() {

        if (GUI.Button(new Rect(50, 50, 100, 30), "Color Demo")) {
            Application.LoadLevel("ColorTest");
        }

        if (GUI.Button(new Rect(50, 100, 100, 30), "Number Demo")) {
            Application.LoadLevel("DemoLevel");
        }

        GUI.Box(new Rect(200, 50, 250, 200), "Controls ");
        GUI.Label(new Rect(210, 100, 300, 20), "WSAD - Move");
        GUI.Label(new Rect(210, 120, 300, 20), "Left Click - Shoot");
        GUI.Label(new Rect(210, 140, 300, 20), "Right Click - Shield");
        GUI.Label(new Rect(210, 160, 300, 20), "Mouse Wheel - Change Color/Frequency");
        GUI.Label(new Rect(210, 180, 300, 20), "1-5 - Choose Weapon");
        GUI.Label(new Rect(210, 220, 300, 20), "Double-tap A/D - Dash");

    }

}
