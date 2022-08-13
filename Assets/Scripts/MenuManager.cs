using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MenuManager : MonoBehaviour
{
    public InputField PlayerNameField;
    public void StartGame()
    {
        DataPersistance.dataPersistance.PlayerName = PlayerNameField.text;
        SceneManager.LoadScene(1, LoadSceneMode.Single);   
    }
}
