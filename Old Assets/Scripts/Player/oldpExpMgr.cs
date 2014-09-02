using UnityEngine;
using System.Collections;

// Manages player experience
public class oldpExpMgr : MonoBehaviour {


    [SerializeField] private StatusBar expBar;

    private int level = 1;
	private int experience = 0;
	private int nextLevel;
    private bool expChanged = true;
    private oldpHealthMgr healthMgr;

    public int Experience {
        get {
            return experience;
        }
        set {
            experience = value;
        }    
    }
    public int Level {
        get {
            return level;
        }
    }
    public int NextLevel {
        get {
            return nextLevel;
        }
    }
    
	void Start(){
		nextLevel = 100 * level;
        healthMgr = GetComponent<oldpHealthMgr>();
	}

	void Update(){
		if (experience >= nextLevel){
			experience = experience - nextLevel;
			level++;
			nextLevel = 100 * level;
            healthMgr.MaxHP += 10 * level;
            healthMgr.ChangedHealth();
		}
        if (expChanged) {
            expBar.SetFillPercentage((float)experience / nextLevel);
            expChanged = false;
        }
	}

	public void AddXP(int xp){
		experience += xp;
        expChanged = true;
	}

}
