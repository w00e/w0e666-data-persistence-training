using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text NameScoreText;
    public PlayerData playerData;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    private static int m_HighScore;
    
    private bool m_GameOver = false;

    private static string SAVE_DIRECTORY;
    private const string SAVE_FILE_NAME = "/savefile.json";

    
    // Start is called before the first frame update
    private void Awake() 
    {
        SAVE_DIRECTORY = Application.persistentDataPath;
        try{
            LoadData();
            Debug.Log(playerData.PlayerName);
        } catch (IOException e){
            Debug.LogError(e);
        }
        
        if(playerData.PlayerName == "") {
            playerData = new PlayerData(DataPersistance.dataPersistance.PlayerName, 0);
        } else 
        {
            NameScoreText.text = $"Best Player : {playerData.PlayerName} : Top Score : {playerData.TopScore}";
        }
       
    }
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        if(m_Points > playerData.TopScore)
        {
            m_HighScore = m_Points;
            playerData = new PlayerData(DataPersistance.dataPersistance.PlayerName, m_HighScore);
            SaveData();
        }
        m_GameOver = true;
        GameOverText.SetActive(true);
        NameScoreText.text = $"Best Player : {playerData.PlayerName} : Top Score : {playerData.TopScore}";
        
    }

    public void SaveData()
    {
        DataSave data = new DataSave();
        data.playerData = playerData;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(SAVE_DIRECTORY + SAVE_FILE_NAME, json);
    }

    public void LoadData()
    {
        string path = SAVE_DIRECTORY + SAVE_FILE_NAME;
        Debug.Log(path);
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            DataSave data = JsonUtility.FromJson<DataSave>(json);
            playerData = data.playerData;
        }


    }

    public void ReturnToMenu() {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
    [System.Serializable]
    private class DataSave 
    {
        public PlayerData playerData;

    }

}
