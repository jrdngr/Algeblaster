using UnityEngine;
using System.Collections;

public class EnemyWeapon : MonoBehaviour {

    [SerializeField] protected float travelDistance;
    [SerializeField] protected GameObject hitEffect;

    protected int damage = 0;
    protected Vector3 velocity = new Vector3(0,0,0);

    protected Vector3 startPos;

    public int Damage {
        get{
            return damage;
        }
        set{
            damage = value;
        }
    }
    public Vector3 Velocity {
        get{
            return velocity;
        }
        set{
            velocity = value;
        }
    }

    protected void Start() {
        startPos = transform.position;
    }

    protected void Update() {
        if ((transform.position - startPos).magnitude >= travelDistance)
            Destroy(this.gameObject);
    }

}
