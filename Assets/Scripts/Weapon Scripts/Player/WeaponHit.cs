using UnityEngine;
using System.Collections;

public class WeaponHit {

    public enum WeaponType { pos = 1, neg, mult, div, fac };
    public enum WeaponColor { blue, red, yellow, purple, green, orange }

    public int damage;
    public int frequency;
    public WeaponHit.WeaponColor color;
    public WeaponHit.WeaponType type;

}
