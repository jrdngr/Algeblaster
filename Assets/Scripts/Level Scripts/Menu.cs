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

        GUI.Box(new Rect(550, 50, 400, 460), "Instructions ");
        GUI.Label(new Rect(560, 100, 400, 20), "White enemies can be killed with anything");
        GUI.Label(new Rect(560, 140, 400, 20), "Enemies with red eyes require color/frequency changes");
        GUI.Label(new Rect(560, 160, 400, 20), "--Colors--");
        GUI.Label(new Rect(560, 180, 400, 20), "First kill them with one of the primary colors that make");
        GUI.Label(new Rect(560, 200, 400, 20), "up their current color, then kill them with their current color");
        GUI.Label(new Rect(560, 220, 400, 20), "--Numbers--");
        GUI.Label(new Rect(560, 240, 400, 20), "Same as colors but use their prime factors");
        GUI.Label(new Rect(560, 280, 400, 20), "Mothership");
        GUI.Label(new Rect(560, 300, 400, 20), "--Colors--");
        GUI.Label(new Rect(560, 320, 400, 20), "Use first two weapons on colored block along with colors");
        GUI.Label(new Rect(560, 340, 400, 20), "The idea is similar to the previous enemy");
        GUI.Label(new Rect(560, 360, 400, 20), "-Numbers--");
        GUI.Label(new Rect(560, 380, 400, 20), "Weapons are your operations");
        GUI.Label(new Rect(560, 400, 400, 20), "1 - Addition");
        GUI.Label(new Rect(560, 420, 400, 20), "2 - Subtraction");
        GUI.Label(new Rect(560, 440, 400, 20), "3 - Division");
        GUI.Label(new Rect(560, 460, 400, 20), "4 - Multiplication");
        GUI.Label(new Rect(560, 480, 400, 20), "5 - Factoring");

    }

}
