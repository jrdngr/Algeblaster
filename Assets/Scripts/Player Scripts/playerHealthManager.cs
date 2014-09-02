using UnityEngine;
using System.Collections;

public class playerHealthManager : MonoBehaviour {

    private int maxHitpoints;
    private int currentHitpoints;
    private EventManager eventManager;
    private PlayerManager playerManager;
    private StatusBar healthBar;

    void Awake() {
        playerManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<PlayerManager>();
        eventManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<EventManager>();
        maxHitpoints = playerManager.MaxHitpoints;
        healthBar = playerManager.HealthBar.GetComponent<StatusBar>();
        currentHitpoints = maxHitpoints;
    }

    public void Hit(int damage) {
        currentHitpoints -= damage;
        healthBar.SetFillPercentage((float)currentHitpoints / maxHitpoints);
        if (currentHitpoints <= 0) {
            eventManager.playerDead = true;
            GameObject explosion = (GameObject)Instantiate(playerManager.PlayerDeathEffect, transform.position, Quaternion.identity);
            Destroy(explosion, 2f);
            Destroy(this.gameObject);
        }
    }

    public void Heal(int health) {
        currentHitpoints += health;
        healthBar.SetFillPercentage((float)currentHitpoints / maxHitpoints);
    }

}
