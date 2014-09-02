using UnityEngine;
using System.Collections;

public class WeaponHit {

    public enum WeaponType { pos = 1, neg, mult, div, fac };

    public int damage;
    public int frequency;
    public WeaponHit.WeaponType type;

}
