using UnityEngine;
using System.Collections;

#pragma warning disable 0414

[ExecuteInEditMode]
public class LevelManager : MonoBehaviour {

    public Rect bounds;
    public Rect playerBounds;

    private Level currentLevel;

    void Update() {
        currentLevel = transform.GetComponentInChildren<Level>();
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(bounds.center, bounds.size);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(playerBounds.center, playerBounds.size);
    }

}

