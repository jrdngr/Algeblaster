using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor (typeof(Level))]
public class LevelEditor : Editor {

    public override void OnInspectorGUI() {
        Level thisLevel = (Level)target;

        EditorGUILayout.LabelField("Level settings", EditorStyles.boldLabel);
        thisLevel.hasMothership = EditorGUILayout.Toggle("Mothership", thisLevel.hasMothership);
        if (thisLevel.hasMothership) {
            thisLevel.mothershipEquation = EditorGUILayout.TextField("Mothership Equation", thisLevel.mothershipEquation);
        }
        thisLevel.experienceReward = EditorGUILayout.IntField("Experience Reward", thisLevel.experienceReward);
        EditorGUILayout.LabelField("Number of Waves:  " + thisLevel.waveList.Count.ToString());
        EditorGUILayout.Separator();

        if (GUILayout.Button("Add Wave", GUILayout.Width(100f))) {
            thisLevel.waveList.Add(new Wave());
        }
        EditorGUILayout.Separator();

        EditorGUILayout.LabelField("Wave List", EditorStyles.boldLabel);
        for (int i = 0; i < thisLevel.waveList.Count; i++) {
            EditorGUILayout.LabelField("Wave " + (i+1).ToString());

            for (int j = 0; j < thisLevel.waveList[i].EnemyList.Count; j++) {
                thisLevel.waveList[i].EnemyList[j] = EditorGUILayout.TextArea(thisLevel.waveList[i].EnemyList[j]);
            }

                GUILayout.BeginHorizontal();
            if (GUILayout.Button("+", GUILayout.Width(40f))) {
                thisLevel.waveList[i].EnemyList.Add("");
            }
            if (GUILayout.Button("-", GUILayout.Width(40f))) {
                if (thisLevel.waveList[i].EnemyList.Count >= 1)
                    thisLevel.waveList[i].EnemyList.RemoveAt(thisLevel.waveList[i].EnemyList.Count-1);
            }
            if (GUILayout.Button("Up", GUILayout.Width(40f))) {
                if (i > 0){
                    thisLevel.waveList.Insert(i - 1, thisLevel.waveList[i]);
                    thisLevel.waveList.RemoveAt(i+1);
                }
            }
            if (GUILayout.Button("Dn", GUILayout.Width(40f))) {
                if (i < thisLevel.waveList.Count-1) {
                    thisLevel.waveList.Insert(i+2, thisLevel.waveList[i]);
                    thisLevel.waveList.RemoveAt(i);
                }
                else if (i == thisLevel.waveList.Count - 1) {
                    thisLevel.waveList.Add(thisLevel.waveList[i]);
                    thisLevel.waveList.RemoveAt(i);
                }
            }
            if (GUILayout.Button("Del", GUILayout.Width(40f))) {
                thisLevel.waveList.RemoveAt(i);
            }
            GUILayout.EndHorizontal();
         }
         EditorUtility.SetDirty(thisLevel);
    }

}
