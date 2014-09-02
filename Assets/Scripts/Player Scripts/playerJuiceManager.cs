using UnityEngine;
using System.Collections;

public class playerJuiceManager : MonoBehaviour {

    private int maxJuice;
    private float juiceRefillSpeed;
    private Timer juiceTick;
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
    }

    void RefillJuice() {
        currentJuice++;
        currentJuice = Mathf.Clamp(currentJuice, 0, maxJuice);
        juiceTick.Go(juiceRefillSpeed);
        juiceBar.SetFillPercentage((float)currentJuice / maxJuice);
    }

    public void UseJuice(int amount) {
        currentJuice -= amount;
        juiceBar.SetFillPercentage((float)currentJuice / maxJuice);
    }

}
