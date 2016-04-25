using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//I have no idea where this uses System.Linq.  I should figure that out
using System.Linq;

//Parent script for the mothership and all of its attached parts
//Creates a mothership based on an inputted equation
//In the future, it will also create a mothership based on initialization parameters
public class Mothership : MonoBehaviour {


    [SerializeField] private float factorOffsetValue;
    [SerializeField] private float distanceBetweenParts;
    [SerializeField] private bool hasDenominator;
    [SerializeField] private string equation;

    private int coreIndex = -1;
    private float centerX = -1;
    private List<MothershipPart.Type> parts = new List<MothershipPart.Type>();
    private List<GameObject> partList = new List<GameObject>();
    private char[] validCharacters = { 'x', 'n', '+', '-', 'f', 'd' };
    private PlayerManager playerManager;
    private bool colorMode = false;

    public string Equation {
        get {
            return equation;
        }
        set {
            equation = value;
        }
    }
    public List<MothershipPart.Type> Parts {
        get {
            return parts;
        }
    }
    public List<GameObject> PartList {
        get {
            return partList;
        }
    }
    public float CenterX {
        get {
            return centerX;
        }
    }

    void Start() {
        playerManager = GameObject.Find("Game Manager").GetComponent<PlayerManager>();
        if (playerManager.FrequencyMode == PlayerManager.FrequencyModes.Color)
            colorMode = true;
        LoadFromString();
        
        int i = 0;
        Vector3 pos;
        float factorOffset = 0;
        GameObject newPart = null;
        foreach (MothershipPart.Type part in parts) {
            pos = new Vector3(transform.position.x + i * distanceBetweenParts + factorOffset - centerX, transform.position.y, transform.position.z);
            switch (part) {
                case MothershipPart.Type.core:
                    newPart = (GameObject)Instantiate(Resources.Load("Enemies/Mothership/Number/MSCore", typeof(GameObject)), pos, Quaternion.Euler(90,0,0));
                    newPart.GetComponent<MSPartCore>().MyType = MothershipPart.Type.core;
                    newPart.GetComponent<MSPartCore>().HasFactor = false;
                    newPart.transform.parent = transform;
                    break;

                case MothershipPart.Type.divide:
                    newPart = (GameObject)Instantiate(Resources.Load("Enemies/Mothership/Number/MSCore", typeof(GameObject)), pos, Quaternion.Euler(90, 0, 0));
                    newPart.GetComponent<MSPartCore>().MyType = MothershipPart.Type.core;
                    newPart.GetComponent<MSPartCore>().HasFactor = true;
                    newPart.transform.parent = transform;
                    break;


                case MothershipPart.Type.minus:
                    newPart = (GameObject)Instantiate(Resources.Load("Enemies/Mothership/Number/MSPlusMinus", typeof(GameObject)), pos, Quaternion.Euler(90, 0, 0));
                    newPart.GetComponent<MSPartPlusMinus>().MyType = MothershipPart.Type.minus;
                    newPart.transform.parent = transform;
                    break;

                case MothershipPart.Type.plus:
                    newPart = (GameObject)Instantiate(Resources.Load("Enemies/Mothership/Number/MSPlusMinus", typeof(GameObject)), pos, Quaternion.Euler(90, 0, 0));
                    newPart.GetComponent<MSPartPlusMinus>().MyType = MothershipPart.Type.plus;
                    newPart.transform.parent = transform;
                    break;

                case MothershipPart.Type.numerator:
                    if (colorMode) {
                        newPart = (GameObject)Instantiate(Resources.Load("Enemies/Mothership/Color/ColorMSNumerator", typeof(GameObject)), pos, Quaternion.Euler(90, 0, 0));
                        newPart.GetComponent<ColorMSPartNumerator>().MyType = MothershipPart.Type.numerator;
                    }
                    else {
                        newPart = (GameObject)Instantiate(Resources.Load("Enemies/Mothership/Number/MSNumerator", typeof(GameObject)), pos, Quaternion.Euler(90, 0, 0));
                        newPart.GetComponent<MSPartNumerator>().MyType = MothershipPart.Type.numerator;
                    }
                    newPart.transform.parent = transform;
                    break;

                case MothershipPart.Type.factor:
                    if (colorMode)
                        newPart = (GameObject)Instantiate(Resources.Load("Enemies/Mothership/Color/ColorMSFactor", typeof(GameObject)), pos, Quaternion.Euler(90, 0, 0));
                    else
                        newPart = (GameObject)Instantiate(Resources.Load("Enemies/Mothership/Number/MSFactor", typeof(GameObject)), pos, Quaternion.Euler(90, 0, 0));
                    newPart.GetComponent<MSPartFactor>().MyType = MothershipPart.Type.factor;
                    newPart.transform.parent = transform;
                    factorOffset = factorOffsetValue;
                    break;

                default:
                    break;
            }
            if (i < coreIndex)
                newPart.GetComponent<MothershipPart>().OnLeft = true;
            newPart.GetComponent<MothershipPart>().MyIndex = i;
            partList.Add(newPart);
            i++;
        }
        if (hasDenominator){
            GameObject denom;
            if (colorMode)
                denom = (GameObject)Instantiate(Resources.Load("Enemies/Mothership/Color/MSDenominator"), new Vector3(transform.position.x + 0.5f, transform.position.y - 1.5f, 0), Quaternion.identity);
            else
                denom = (GameObject)Instantiate(Resources.Load("Enemies/Mothership/Number/MSDenominator"), new Vector3(transform.position.x + 0.5f, transform.position.y - 1.5f, 0), Quaternion.identity);
            denom.transform.parent = transform;
            denom.GetComponent<MSPartDenominator>().MyShield.transform.localScale = new Vector3(centerX + 1, 1, 2);
            denom.GetComponent<MSPartDenominator>().MyShield.GetComponent<ParticleSystem>().emissionRate *= centerX;
        }
        GetComponent<MSMove>().Center(centerX);
    }

    public void LoadFromString() {
        //Check for valid characters
        if (equation != null) {
            char lastChar = 'a';
            int i = 0;
            foreach (char c in equation) {
                if (!validCharacters.Contains(c)) {
                    string errorMessage = c.ToString() + " is not a valid character";
                    Debug.LogError(errorMessage);
                    Debug.Break();
                }
                if ((c == 'x' || c == 'f' || c == 'd') && coreIndex == -1) {
                    coreIndex = i;
                }
                else if ((c == 'x' || c == 'f' || c == 'd') && coreIndex != -1) {
                    Debug.LogError("There can only be one x, f, or d character");
                    Debug.Break();
                }
                if ((c == '+' || c == '-') && (lastChar == '+' || lastChar == '-')) {
                    Debug.LogError("You can't have two operators next to each other");
                    Debug.Break();
                }
                if (c == 'n' && lastChar == 'n') {
                    Debug.LogError("You can't have two numbers next to each other");
                    Debug.Break();
                }
                lastChar = c;
                i++;
            }
        }

        //Clear parts list
        parts.Clear();

        MothershipPart.Type currentPart = MothershipPart.Type.blank;
        for (int i = 0; i < equation.Length; i++) {
            centerX += 1;
            switch (equation[i]) {
                case 'x':
                    currentPart = MothershipPart.Type.core;
                    break;
 
                case 'd':
                    currentPart = MothershipPart.Type.divide;
                    break;

                case '+':
                    currentPart = MothershipPart.Type.plus;
                    break;
                
                case '-':
                    currentPart = MothershipPart.Type.minus;
                    break;
                
                case 'n':
                    currentPart = MothershipPart.Type.numerator;
                    break;
                
                case 'f':
                    currentPart = MothershipPart.Type.factor;
                    centerX += factorOffsetValue - 1;
                    break;
                
                default:
                    break;
            }
            parts.Add(currentPart);
        }
        centerX /= 2;
    }
}
