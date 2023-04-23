using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] protected bool isEnabled = false;
    [SerializeField] protected float launchDelay = 3f;
    [SerializeField] private TimerUI timerUI;
    
    protected bool _timeLeft = false;

    protected void TimerUpdate()
    {
        if (isEnabled)
        {
            if (timerUI != null) timerUI.SetTimerValue(launchDelay);

            if (launchDelay > 1)
            {
                launchDelay -= Time.deltaTime;
            }
            else
            {
                _timeLeft = true;
            }
        }
    }

    protected void StartTimer()
    {
        isEnabled = true;
    }
}
