using UnityEngine;
using System.Collections;

// Contains variables common to all projectiles
public class oldWeapon : MonoBehaviour {

	public enum WeaponType {pos = 1, neg, mult, div, fac};

    [SerializeField] protected int damage;
    [SerializeField] protected int frequency;
    [SerializeField] protected float fireDelay;
    [SerializeField] protected WeaponType type;
    [SerializeField] protected GameObject hitEffect;

    public void Fire() {

    }

}
