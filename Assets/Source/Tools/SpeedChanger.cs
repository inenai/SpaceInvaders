using UnityEngine;

public class SpeedChanger : MonoBehaviour
{
    public void MultiplySpeed(bool fast)
    {
        if (fast)
            Time.timeScale = 5f;
        else
            Time.timeScale = 1f;

    }
}
