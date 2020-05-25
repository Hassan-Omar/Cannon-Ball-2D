using UnityEngine;
/// <summary>
/// This Class Created by H.Omar to Init Result Panel 
/// </summary>
public class ResultPanel : MonoBehaviour
{   
    [SerializeField] private RestApiManager restAPI; 
    // Start is called before the first frame update
    void Start()
    {
        restAPI.getMyScores();
    }
}
