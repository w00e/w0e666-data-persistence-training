using System;

[Serializable]
public class PlayerData
{
    public string PlayerName;
    public int TopScore;

    public PlayerData(string name, int score) 
    {
        PlayerName = name;
        TopScore = score;    
    }
}
