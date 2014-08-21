using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Wave {

    public List<string> EnemyList = new List<string>();

    public Wave() {
        EnemyList.Add("");
    }

}
