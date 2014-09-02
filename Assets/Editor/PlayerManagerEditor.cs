using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor (typeof(PlayerManager))]
public class PlayerManagerEditor : Editor {
   
    public override void OnInspectorGUI() {
        PlayerManager thisPlayerManager = (PlayerManager)target;

        EditorGUILayout.LabelField("Movement", EditorStyles.boldLabel);
        thisPlayerManager.MaxSpeed = EditorGUILayout.FloatField("Max Speed", thisPlayerManager.MaxSpeed);
        thisPlayerManager.DashForce = EditorGUILayout.FloatField("Dash Force", thisPlayerManager.DashForce);
        thisPlayerManager.DashTime = EditorGUILayout.FloatField("Dash Time", thisPlayerManager.DashTime);
        thisPlayerManager.DashDelay = EditorGUILayout.FloatField("Dash Delay", thisPlayerManager.DashDelay);
        thisPlayerManager.ThrustForce = EditorGUILayout.FloatField("Thrust Force", thisPlayerManager.ThrustForce);
        thisPlayerManager.BumpForce = EditorGUILayout.FloatField("Bump Force", thisPlayerManager.BumpForce);
        thisPlayerManager.BumpTime = EditorGUILayout.FloatField("Bump Time", thisPlayerManager.BumpTime);
        thisPlayerManager.DashEffect = (GameObject)EditorGUILayout.ObjectField("Dash Effect", thisPlayerManager.DashEffect, typeof(GameObject), false);
        thisPlayerManager.BumpEffect = (GameObject)EditorGUILayout.ObjectField("Bump Effect", thisPlayerManager.BumpEffect, typeof(GameObject), false);
        EditorGUILayout.Separator();

        EditorGUILayout.LabelField("Shield", EditorStyles.boldLabel);
        thisPlayerManager.JuiceCost = EditorGUILayout.IntField("Juice Cost", thisPlayerManager.JuiceCost);
        thisPlayerManager.JuiceDrainTickTime = EditorGUILayout.FloatField("Juice Drain Delay", thisPlayerManager.JuiceDrainTickTime);
        thisPlayerManager.EmptyJuiceDelay = EditorGUILayout.FloatField("Empty Juice Delay", thisPlayerManager.EmptyJuiceDelay);
        EditorGUILayout.Separator();

        EditorGUILayout.LabelField("Health", EditorStyles.boldLabel);
        thisPlayerManager.MaxHitpoints = EditorGUILayout.IntField("Maximum Hitpoints", thisPlayerManager.MaxHitpoints);
        thisPlayerManager.BumpDamage = EditorGUILayout.IntField("Bump Damage", thisPlayerManager.BumpDamage);
        thisPlayerManager.PlayerDeathEffect = (GameObject)EditorGUILayout.ObjectField("Player Death Effect", thisPlayerManager.PlayerDeathEffect, typeof(GameObject), false);
        thisPlayerManager.HealthBar = (GameObject)EditorGUILayout.ObjectField("Health Bar", thisPlayerManager.HealthBar, typeof(GameObject), true);
        EditorGUILayout.Separator();

        EditorGUILayout.LabelField("Juice", EditorStyles.boldLabel);
        thisPlayerManager.MaxJuice = EditorGUILayout.IntField("Maximum Juice", thisPlayerManager.MaxJuice);
        thisPlayerManager.JuiceRefillSpeed = EditorGUILayout.FloatField("Juice Refill Speed", thisPlayerManager.JuiceRefillSpeed);
        thisPlayerManager.JuiceBar = (GameObject)EditorGUILayout.ObjectField("Juice Bar", thisPlayerManager.JuiceBar, typeof(GameObject), true);
        EditorGUILayout.Separator();

        EditorGUILayout.LabelField("Weapon Management", EditorStyles.boldLabel);
        thisPlayerManager.FrequencyDisplay = (GameObject)EditorGUILayout.ObjectField("Frequency Display", thisPlayerManager.FrequencyDisplay, typeof(GameObject), true);
        thisPlayerManager.WeaponDisplay = (GameObject)EditorGUILayout.ObjectField("Weapon Display", thisPlayerManager.WeaponDisplay, typeof(GameObject), true);
        EditorGUILayout.Separator();

        EditorGUILayout.LabelField("Unlocked Weapons", EditorStyles.boldLabel);
        thisPlayerManager.HasPositron = EditorGUILayout.Toggle("Positron Gun", thisPlayerManager.HasPositron);
        thisPlayerManager.HasNegatron = EditorGUILayout.Toggle("Negatron Gun", thisPlayerManager.HasNegatron);
        thisPlayerManager.HasDisintegrator = EditorGUILayout.Toggle("Disintegrator", thisPlayerManager.HasDisintegrator);
        thisPlayerManager.HasMultizapper = EditorGUILayout.Toggle("Multizapper", thisPlayerManager.HasMultizapper);
        thisPlayerManager.HasFactorBeam = EditorGUILayout.Toggle("Factor Beam", thisPlayerManager.HasFactorBeam);
        EditorGUILayout.Separator();

        EditorGUILayout.LabelField("Player Progression", EditorStyles.boldLabel);
        thisPlayerManager.FrequencyMode = (PlayerManager.FrequencyModes)EditorGUILayout.EnumPopup("Frequency Mode", thisPlayerManager.FrequencyMode);
        thisPlayerManager.Credits = EditorGUILayout.IntField("Credits", thisPlayerManager.Credits);
        EditorGUILayout.Separator();

        EditorGUILayout.LabelField("Movement Upgrades", EditorStyles.boldLabel);
        //Movement upgrades
        EditorGUILayout.Separator();

        EditorGUILayout.LabelField("Shield Upgrades", EditorStyles.boldLabel);
        thisPlayerManager.HasShieldMagnet = EditorGUILayout.Toggle("Shield Magnet", thisPlayerManager.HasShieldMagnet);
        thisPlayerManager.ShieldMagnetForce = EditorGUILayout.FloatField("Shield Magnet Force", thisPlayerManager.ShieldMagnetForce);
        thisPlayerManager.ShieldMagnetRadius = EditorGUILayout.FloatField("Shield Magnet Radius", thisPlayerManager.ShieldMagnetRadius);
        EditorGUILayout.Separator();

        EditorUtility.SetDirty(thisPlayerManager);
    }

}
