using UnityEngine;
using System.Collections;

// Handles the mothership code at the bottom right of the GUI
public class CodeManager : MonoBehaviour {

    private int myValue = 1;
    private int myDenominator = 1;
    private TextMesh numberLabel;

    public int MyValue {
        get {
            return myValue;
        }
        set {
            myValue = value;
            myDenominator = 1;
            numberLabel.text = value.ToString();
        }
    }
    public int MyDenominator {
        get {
            return myDenominator;
        }
        set {
            myDenominator = value;
            if (myValue % myDenominator == 0) {
                MyValue = myValue / myDenominator;
                myDenominator = 1;
                numberLabel.text = value.ToString();
            }
            else {
                ReduceFraction();
                numberLabel.text = myValue.ToString() + "/" + myDenominator.ToString();
            }
        }
    }

	void Start () {
		renderer.sortingLayerName = "GUI Top";
		renderer.sortingOrder = 1;
        numberLabel = GetComponent<TextMesh>();
	}

    void ReduceFraction() {
        for (int i = 1; i < 10; i++) {
            if ((float)myValue % i == 0f && (float)myDenominator % i == 0) {
                myValue /= i;
                myDenominator /= i; 
            }
        }
    }

    public void AddValue(int num) {
            myValue += num * myDenominator;
            if ((float)myValue % myDenominator == 0f) {
                MyValue = myValue / myDenominator;
                myDenominator = 1;
                numberLabel.text = myValue.ToString();
            }
            else {
                ReduceFraction();
                numberLabel.text = myValue.ToString() + "/" + myDenominator.ToString();
            }
    }

    public void MultiplyValue(int num) {
        myValue *= num;
        if ((float)myValue % myDenominator == 0f) {
            MyValue = myValue / myDenominator;
            myDenominator = 1;
            numberLabel.text = myValue.ToString();
        }
        else {
            ReduceFraction();
            numberLabel.text = myValue.ToString() + "/" + myDenominator.ToString();
        }
    }


}
