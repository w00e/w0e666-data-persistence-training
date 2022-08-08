using UnityEngine;

public class DataPersistance : MonoBehaviour
{
    public static DataPersistance dataPersistance {get; private set;}
    private string _playerName = "nameTest";
    public string PlayerName
    {
        get => _playerName;
        set
        {
            _playerName = value;
        }
    } 
    private int _topScore = 0;
    public int TopScore 
    {
        get => _topScore; 
        set 
        {
            if( value >= 0 ) _topScore = value;
        }
    }

    private void Awake()
    {
     if(dataPersistance == null)
     {
        dataPersistance = this;
        DontDestroyOnLoad(dataPersistance);
     } 
     else
     {
        Destroy(gameObject);
     }
    }

    // public void setTopScorePlayer(int score, string player)
    // {
    //     if(score >= _topScore)
    //     {
    //         _topScore = score;
    //         playerWithTopScore = player;
    //     }
    // }
    public void setPlayerName(string name)
    {
        _playerName = name;
    }
}
