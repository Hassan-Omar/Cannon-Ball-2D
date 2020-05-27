using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private float normalFixedDeltaTime;
    private void Start()
    {
        normalFixedDeltaTime = Time.fixedDeltaTime;
    }
    /// <summary>
    /// Function to apply smooth slow motion effect  
    /// </summary>
    /// <param name="slowMotionFactor">value to control on speed of slow motion </param>
    public void doSlowMotion(float slowMotionFactor)
    {
        Time.timeScale = slowMotionFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.03f;
    }
    /// <summary>
    /// function to reset timeScale. 
    /// this can be used to back from slow motion effect.
    /// </summary>
    public void backToNormal()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = normalFixedDeltaTime;
    }
}
