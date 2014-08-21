using UnityEngine;
using System.Collections;

//This is a special class that attaches to a core object if it can be divided
public class MSPartDivide : MonoBehaviour {

    [SerializeField] private TextMesh numberLabel;

    private int myFactor;
    private CodeMgr codeMgr;

    public int MyFactor {
        get{
            return myFactor;
        }
        set{
            myFactor = value;
        }
    }

    void Awake() {
        codeMgr = GameObject.FindGameObjectWithTag("SecretCode").GetComponent<CodeMgr>();
        myFactor = Random.Range(2, 9);
        numberLabel.fontSize = 80;
    }

    void Start() {
        numberLabel.text = myFactor + "x";
    }

    void Kill() {
        GetComponent<MSPartCore>().HasFactor = false;
        numberLabel.text = "x";
        Destroy(this);
    }

    public void ChangedValue() {
        numberLabel.text = myFactor.ToString() + "x";
        if (myFactor == 1)
            Kill();
    }

    public void GotHit(WeaponHit weaponHit) {
        if (weaponHit.type == Weapon.WeaponType.div && weaponHit.frequency == myFactor) {
            if (codeMgr.MyDenominator == 1)
                codeMgr.MyDenominator = myFactor;
            else
                codeMgr.MyDenominator *= myFactor;
            Kill();
        }
    }
}
