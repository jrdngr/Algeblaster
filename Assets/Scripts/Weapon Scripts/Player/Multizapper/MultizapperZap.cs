using UnityEngine;
using System.Collections;

public class MultizapperZap : MonoBehaviour {

    private float zapSpeed;
    private float chainRange;
    private int numberOfChains = 1;
    private Transform myTarget;
    private WeaponHit myHit;

    void FixedUpdate() {
        if (myTarget != null)
            GetComponent<Rigidbody>().velocity = (myTarget.transform.position - transform.position) * zapSpeed;
        if (numberOfChains <= 0)
            Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<EnemyHealthManager>()) {
            other.gameObject.GetComponent<EnemyHealthManager>().Hit(myHit);
            myTarget = null;
            numberOfChains--;
            if (numberOfChains > 0) {
                Collider[] colliders = Physics.OverlapSphere(transform.position, chainRange);
                foreach (Collider c in colliders) {
                    if (c.GetComponent<Rigidbody>() && c.GetComponent<Rigidbody>().CompareTag("Fodder") && c.gameObject != other.gameObject) {
                        myTarget = c.transform;
                    }
                }
                if (myTarget == null)
                    Destroy(this.gameObject);
            }
        }

    }

    public void SetProperties(Transform target, float zSpeed, int zDamage, float cRange, int numChains, WeaponHit hit) {
        myTarget = target;
        zapSpeed = zSpeed;
        chainRange = cRange;
        numberOfChains = numChains;
        myHit = hit;
        myHit.damage = zDamage;
    }

}
