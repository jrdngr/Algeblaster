using UnityEngine;
using System.Collections;

#pragma warning disable 414
#pragma warning disable 219

// Senses enemies around the Multizapper orb and spawns a zap if it finds one
public class oldMultizapperSensor : MonoBehaviour {

	[SerializeField] private float zapDelay = 1f;
	[SerializeField] private GameObject zap;

	private int damage;
	private int frequency;
    private bool canZap = true;
	private GameObject myParent;
    private Timer zapTimer;

	void Start(){
		myParent = transform.parent.gameObject;
		damage = myParent.GetComponent<Multizapper>().Damage;
		frequency = myParent.GetComponent<Multizapper>().Frequency;
        zapTimer = gameObject.AddComponent<Timer>();
        zapTimer.Trigger += ResetTimer;
	}

	void OnTriggerStay(Collider collision){
        EnemyHealthManager enemyHealthMgr = collision.GetComponent<EnemyHealthManager>();
        if ((collision.CompareTag("Minion") || collision.gameObject.CompareTag("Mothership") || collision.gameObject.CompareTag("Fodder")) && canZap) {
			GameObject newZap = (GameObject)Instantiate(zap, transform.position, Quaternion.identity);
			newZap.GetComponent<Zap>().StartPos = transform.position;
			newZap.GetComponent<Zap>().Target = collision.transform.position;

            WeaponHit weaponHit = new WeaponHit(damage, frequency, Weapon.WeaponType.mult);
            collision.GetComponent<EnemyHealthManager>().Hit(weaponHit);
            canZap = false;
            zapTimer.Go(zapDelay);
		}
	}

    void ResetTimer() {
        canZap = true;
    }

}
