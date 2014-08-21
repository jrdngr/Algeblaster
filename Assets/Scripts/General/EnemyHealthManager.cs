using UnityEngine;
using System.Collections;

#pragma warning disable 414

//Basic health manager for all enemies.  
public class EnemyHealthManager : MonoBehaviour {

    [SerializeField] private int maxHP;
    [SerializeField] private bool isMinion;
    [SerializeField] private bool isFodder;
    
    private int currentHP;
    private int lastHitFreq;
    private Weapon.WeaponType lastHitType;

    public int MaxHP {
        get {
            return maxHP;
        }
        set {
            maxHP = value;
            currentHP = value;
        }
    }
    public int CurrentHP {
        get {
            return currentHP;
        }
        set {
            currentHP = value;
        }
    }
    public bool IsMinion {
        get {
            return isMinion;
        }        
    }
    public bool IsFodder {
        get {
            return isFodder;
        }
        set {
            isFodder = value;
        }
    }

    void Awake() {
        currentHP = maxHP;
    }

    void Update() {
        if (currentHP < 0)
            currentHP = 0;
    }

    public void AddHP(int hp) {
        currentHP += hp;
    }

    public void SubtractHP(int hp) {
        currentHP -= hp;
    }

    public int GetLastHitFreq() {
        return lastHitFreq;
    }

    public void Hit(WeaponHit weaponHit) {
        if (isMinion || isFodder)
            currentHP -= weaponHit.damage;
        else
            this.gameObject.SendMessage("GotHit", weaponHit);
        lastHitFreq = weaponHit.frequency;
        lastHitType = weaponHit.type;
    }

}
