using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class God : MonoBehaviour {

    public Rect bounds;

    private Level currentLevel;

    void Update() {
        currentLevel = transform.GetComponentInChildren<Level>();
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(bounds.center, bounds.size);
    }

}

