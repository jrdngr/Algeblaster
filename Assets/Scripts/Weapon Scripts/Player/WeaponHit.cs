using UnityEngine;
using System.Collections;

public class WeaponHit {

    public enum WeaponType { pos = 1, neg, mult, div, fac };
    public enum WeaponColor { blue, red, yellow }

    public int damage;
    public int frequency;
    public WeaponHit.WeaponColor color;
    public WeaponHit.WeaponType type;

}
