using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{

    public Image CoolDownImage;
    public GameEvent playerEvents;
    public TextMeshProUGUI timerui;


    // Start is called before the first frame update
    void Start()
    {
        playerEvents.OnChargCoolDown += Cooldown;
        playerEvents.onTimerChange += DisplayTime;
    }

    // Update is called once per frame
    void Update()
    {
     
    }
    public void Cooldown(float cooldown)
    {
        CoolDownImage.fillAmount= cooldown;
    }
    public void DisplayTime(float timer)
    {
        float minutes = Mathf.FloorToInt(timer / 60);
        float seconds = Mathf.FloorToInt(timer% 60);
        timerui.text = string.Format("{1:00}", minutes, seconds);
    }
}
