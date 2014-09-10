using UnityEngine;
using System.Collections;

//Manages the minion's displayed number
public class MinPFNumberMgr : MonoBehaviour {

    [SerializeField] private int hpMultiplier;
    [SerializeField] private GameObject numberLabel;

    private int shipNumber;
    private ArrayList factorList = new ArrayList();
    private EnemyHealthManager healthMgr;

    public ArrayList FactorList {
        get {
            return factorList;
        }
    }

    void Awake() {
        healthMgr = GetComponent<EnemyHealthManager>();
        SetShipNumber(Random.Range(2, 20));
    }

    public int GetShipNumber() {
        return shipNumber;
    }

    public void SetShipNumber(int num) {
        shipNumber = num;
        healthMgr.MaxHP = shipNumber * hpMultiplier;
        factorList = MathTools.GetFactors(num);
        numberLabel.GetComponent<TextMesh>().text = shipNumber.ToString();
        float scale = ((shipNumber / 90f) + (7f / 90f));
        transform.localScale = new Vector3(scale, scale, scale);
        GetComponent<MinPFMoveMgr>().MaxSpeed = (shipNumber * (-1f / 6f) + (16f / 3f));
    }


}
