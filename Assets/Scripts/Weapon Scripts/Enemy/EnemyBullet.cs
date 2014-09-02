using UnityEngine;
using System.Collections;

public class EnemyBullet : EnemyWeapon {

    void FixedUpdate() {
        rigidbody.velocity = Velocity;
    }

    void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Shield")) {
            if (collision.gameObject.CompareTag("Player"))
                collision.transform.parent.gameObject.GetComponent<playerHealthManager>().Hit(damage);
            Destroy(this.gameObject);
            GameObject pop = (GameObject)Instantiate(hitEffect, new Vector3(transform.position.x, transform.position.y + 0.5f, 0), Quaternion.identity);
            Destroy(pop, 0.5f);
        }
    }

}
