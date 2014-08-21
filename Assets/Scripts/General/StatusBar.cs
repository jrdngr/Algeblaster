using UnityEngine;
using System.Collections;

public class StatusBar : MonoBehaviour {

    [SerializeField] float height;
    [SerializeField] float width;
    [SerializeField] Color fullColor;
    [SerializeField] Color emptyColor;
  
    private float fillPercentage; //Fill percentage should be divided by 100 before sending it to this object
    private bool changedValue = false;
    private GameObject fullBar;
    private GameObject emptyBar;

    void OnDrawGizmosSelected() {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = fullColor;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(width, height, 0));
    }

    void Awake() {
        fullBar = transform.FindChild("FullBar").gameObject;
        fullBar.GetComponent<SpriteRenderer>().color = fullColor;
        emptyBar = transform.FindChild("EmptyBar").gameObject;
        emptyBar.GetComponent<SpriteRenderer>().color = emptyColor;
        fillPercentage = 1f;
    }

    void Start() {
        ChangeValue();
    }

    void Update() {
        fillPercentage = Mathf.Clamp(fillPercentage, 0f, 1f);
        if (changedValue)
            ChangeValue();
    }

    void ChangeValue() {
        //100 converts the 1x1 pixel sprite used for the bars into Unity's coordinates
        float fullWidth = width * fillPercentage * 100;
        float emptyWidth = width * (1 - fillPercentage) * 100;
        fullBar.transform.localScale = new Vector3(fullWidth, height * 100, 0);
        emptyBar.transform.localScale = new Vector3(emptyWidth, height * 100, 0);
        fullBar.transform.localPosition = new Vector3(-width/2*(1-fillPercentage), 0, 0);
        emptyBar.transform.localPosition = new Vector3(width / 2 * fillPercentage, 0, 0);
        changedValue = false;
    }

    public void SetFillPercentage(float percent) {
        fillPercentage = percent;
        changedValue = true;
    }

}
