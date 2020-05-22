using UnityEngine;
using Proyecto26;
/// <summary>
/// This Class Created to handle High Scores 
/// </summary>
public class RestApiManager : MonoBehaviour
{
    private string baseUrl = "https://cannon-ball.herokuapp.com";
    TransfaredObj[] allScores;



    public void getAllScores()
    {

        RestClient.GetArray<TransfaredObj>(this.baseUrl + "/themes").Then(Score =>
          {
              allScores = Score;
            //EditorUtility.DisplayDialog("JSON", JsonUtility.ToJson(themes, true), "Ok");            
          });
    }

}

public class TransfaredObj
{

}