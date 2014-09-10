using UnityEngine;
using System.Collections;

public class Squadron : MonoBehaviour {

    public enum SquadronPattern { straight = 0, curve, zigzag }

    [SerializeField] private int squadronDirection; //Should be 0 or 1
    [SerializeField] private SquadronPattern pattern;
    //[SerializeField] private float thrustForce;
    [SerializeField] private float maxSpeed;
    [SerializeField] GameObject deathEffect;

    //public properties
    public int SquadronDirection {
        get { return squadronDirection; }
        set { squadronDirection = value; }
    }
    public int Pattern {
        set {
            switch (value) {
                case 0:
                    pattern = SquadronPattern.straight;
                    break;
                case 1:
                    pattern = SquadronPattern.curve;
                    break;
                case 2:
                    pattern = SquadronPattern.zigzag;
                    break;
                default:
                    break;
            }
        }
    }
    public SquadronPattern MyPattern {
        get { return pattern; }
    }
    /*public float ThrustForce {
        get {
            return thrustForce;
        }
        set {
            thrustForce = value;
        }
    }*/
    public float MaxSpeed {
        get {
            return maxSpeed;
        }
        set {
            maxSpeed = value;
        }
    }
    public GameObject DeathEffect {
        get {
            return deathEffect;
        }
    }

}
