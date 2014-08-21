using UnityEngine;
using System.Collections;

// Manages player health and health status bar
public class pHealthMgr : MonoBehaviour {

	[SerializeField] private int maxHP;
	[SerializeField] private GameObject deathEffect;
	[SerializeField] private StatusBar healthBar;

    public int MaxHP {
        get {
            return maxHP;
        }
        set {
            maxHP = value;
        }
    }

    private int currentHP;
    private bool healthChanged = false;
    private GameObject god;

	void Start(){
        god = GameObject.FindGameObjectWithTag("God");
        currentHP = maxHP;
	}

	void FixedUpdate(){
		currentHP = Mathf.Clamp (currentHP, 0, maxHP);
		if (healthChanged)
            CheckHealth ();
	}

	void CheckHealth(){
		if (currentHP <= 0){
			GameObject explosion = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
			Destroy (explosion, 2f);
			Destroy (this.gameObject);
			god.GetComponent<EventManager>().playerDead = true;
		}
        healthBar.SetFillPercentage((float)currentHP / maxHP);
        healthChanged = false;
    }

    public void ChangedHealth() {
        healthChanged = true;
    }

	public void Heal(int health){
		currentHP += health;
        healthChanged = true;
	}

	public void Hit(int damage){
		currentHP -= damage;
        healthChanged = true;
	}
}
