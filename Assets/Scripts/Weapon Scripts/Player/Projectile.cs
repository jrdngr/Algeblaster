using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    protected float mySpeed;
    protected Vector3 myVelocity = Vector3.zero;
    protected Rect myBounds;
    protected WeaponHit hit = new WeaponHit();
    protected GameObject hitEffect;

    protected virtual void FixedUpdate() {
        rigidbody.velocity = myVelocity;
        if (transform.position.y > myBounds.yMax)
            Destroy(this.gameObject);
    }

    protected virtual void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<EnemyHealthManager>() != null) {
            other.gameObject.GetComponent<EnemyHealthManager>().Hit(hit);
            GameObject myEffect = (GameObject)Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(myEffect, 5f);
            Destroy(this.gameObject);
        }
    }

    public virtual void SetProperties(float speed, Rect bounds, Vector3 velocity, int damage, int frequency, WeaponHit.WeaponType type, GameObject effect) {
        mySpeed = speed;
        myBounds = bounds;
        myVelocity = velocity;
        hit.damage = damage;
        hit.frequency = frequency;
        hit.type = type;
        hitEffect = effect;
    }


}
