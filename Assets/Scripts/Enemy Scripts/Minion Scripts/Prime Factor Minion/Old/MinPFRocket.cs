using UnityEngine;
using System.Collections;

//Spawns a rocket if the player splits the minion with a non-factor
public class MinPFRocket : MonoBehaviour{

    [SerializeField] private int damage;
    [SerializeField] private GameObject hitEffect;

    void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Shield")) {
            if (collision.gameObject.CompareTag("Player")) 
                collision.transform.parent.GetComponent<playerHealthManager>().Hit(damage);
            Destroy(this.gameObject);
            GameObject pop = (GameObject)Instantiate(hitEffect, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            Destroy(pop, 2f);
        }
    }

}
