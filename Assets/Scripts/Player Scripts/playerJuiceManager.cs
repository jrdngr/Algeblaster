using UnityEngine;
using System.Collections;

public class playerJuiceManager : MonoBehaviour {

    private const float juiceWaitTime = 0.1f;

    private int maxJuice;
    private float juiceRefillSpeed;
    private bool canRefill = true;
    private Timer juiceTick;
    private Timer juiceWaitTimer;
    private PlayerManager playerManager;
    private StatusBar juiceBar;

    private int currentJuice;
    public int CurrentJuice {
        get { return currentJuice; }
    }


    void Awake() {
        playerManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<PlayerManager>();
        juiceBar = playerManager.JuiceBar.GetComponent<StatusBar>();
        maxJuice = playerManager.MaxJuice;
        currentJuice = maxJuice;
        juiceRefillSpeed = playerManager.JuiceRefillSpeed;
        juiceTick = gameObject.AddComponent<Timer>();
        juiceTick.Trigger += RefillJuice;
        juiceTick.Go(juiceRefillSpeed);
        juiceWaitTimer = gameObject.AddComponent<Timer>();
        juiceWaitTimer.Trigger += ResetJuiceWait;
    }

    void Update() {

    }

    void RefillJuice() {
        if (canRefill) {
            currentJuice++;
            currentJuice = Mathf.Clamp(currentJuice, 0, maxJuice);
        }
        juiceTick.Go(juiceRefillSpeed);
        juiceBar.SetFillPercentage((float)currentJuice / maxJuice);
    }

    void ResetJuiceWait() {
        canRefill = true;
    }

    public void UseJuice(int amount) {
        currentJuice -= amount;
        juiceBar.SetFillPercentage((float)currentJuice / maxJuice);
        canRefill = false;
        juiceWaitTimer.Go(juiceWaitTime);
    }

}
