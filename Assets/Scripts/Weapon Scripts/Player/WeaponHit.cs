using UnityEngine;
using System.Collections;

public class WeaponHit {

    public int damage;
    public int frequency;
    public Weapon.WeaponType type;
    
    
    public WeaponHit(int dam, int freq, Weapon.WeaponType t) {
        damage = dam;
        frequency = freq;
        type = t;
    }
}
