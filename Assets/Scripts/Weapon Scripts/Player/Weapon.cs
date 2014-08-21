using UnityEngine;
using System.Collections;

// Contains variables common to all projectiles
public class Weapon : MonoBehaviour {

	public enum WeaponType {pos = 1, neg, mult, div, fac};

    [SerializeField] protected int damage;
    [SerializeField] protected int frequency;
    [SerializeField] protected WeaponType type;
    [SerializeField] protected GameObject gui;
    [SerializeField] protected GameObject hitEffect;

}
