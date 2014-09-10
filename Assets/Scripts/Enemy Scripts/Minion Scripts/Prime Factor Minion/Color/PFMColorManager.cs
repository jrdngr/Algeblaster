using UnityEngine;
using System.Collections;

public class PFMColorManager : MonoBehaviour {

    private GameObject numberLabel;

    private bool isPrimary;
    private WeaponHit.WeaponColor currentColor;
    private Renderer myRenderer;

    //Public properties
    public WeaponHit.WeaponColor CurrentColor {
        get { return currentColor; }
        set { currentColor = value; }
    }
    public bool IsPrimary {
        get { return isPrimary; }
        set { isPrimary = value; }
    }

    void Awake() {
        myRenderer = transform.Find("PFM Mesh").GetComponent<Renderer>();
        switch (Random.Range(0, 3)) {
            case 0:
                currentColor = WeaponHit.WeaponColor.purple;
                break;
            case 1:
                currentColor = WeaponHit.WeaponColor.green;
                break;
            case 2:
                currentColor = WeaponHit.WeaponColor.orange;
                break;
            default:
                currentColor = WeaponHit.WeaponColor.purple;
                break;
        }
        isPrimary = false;
    }

    void Start() {
        switch (currentColor) {
            case WeaponHit.WeaponColor.blue:
                myRenderer.material.color = new Color(0, 0, 1, 1);
                break;
            case WeaponHit.WeaponColor.red:
                myRenderer.material.color = new Color(1, 0, 0, 1);
                break;
            case WeaponHit.WeaponColor.yellow:
                myRenderer.material.color = new Color(1, 1, 0, 1);
                break;
            case WeaponHit.WeaponColor.purple:
                myRenderer.material.color = new Color(1, 0, 1, 1);
                break;
            case WeaponHit.WeaponColor.green:
                myRenderer.material.color = new Color(0, 1, 0, 1);
                break;
            case WeaponHit.WeaponColor.orange:
                myRenderer.material.color = new Color(1, 0.5f, 0, 1);
                break;

        }
    }
}
