using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SaveData 
{
    public int sceneIndex;
    public PlayerData playerInfo;
}

[System.Serializable]
public class PlayerData
{
    public Vector3 position;
    
    public int health;
    
    //public int mana; //-------------------------------??
}


public class SaveLoadManager : MonoBehaviour
{
    private bool HasActiveSave = false;
    
    public GameObject player;
    
    public void SaveGame() 
    {
        SaveData data = new SaveData();
        
        data.sceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        var playerHealth = player.GetComponent<PlayerHealth>();
        
        data.playerInfo = new PlayerData() 
        {
            position = player.transform.position,
            health = playerHealth.health,
            //mana = pc.mana
        };

        string json = JsonUtility.ToJson(data, true);
        
        string path = Path.Combine(Application.persistentDataPath, "save.json");
        
        File.WriteAllText(path, json);
        
        HasActiveSave = true;
    }

    public void LoadGame() 
    {
        string path = Path.Combine(Application.persistentDataPath, "save.json");
        
        if (!File.Exists(path)) return;
        
        string json = File.ReadAllText(path);
        
        SaveData data = JsonUtility.FromJson<SaveData>(json);
        
        SceneManager.LoadScene(data.sceneIndex);
        
        StartCoroutine(LoadData(data));
    }

    IEnumerator LoadData(SaveData data) 
    {
        yield return new WaitForSeconds(0.1f);
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        if (player != null && data.playerInfo != null) 
        {
            player.transform.position = data.playerInfo.position;
            
            var pc = player.GetComponent<PlayerHealth>();
            
            pc.health = data.playerInfo.health;
            
            //pc.mana = data.playerInfo.mana;
        }
    }
    
    public void DeleteSaveFile() 
    {
        string path = Path.Combine(Application.persistentDataPath, "save.json");
        
        if (File.Exists(path)) 
        {
            File.Delete(path);
            
            HasActiveSave = false;
            
            Debug.Log("Save file deleted successfully.");
        } 
        else 
        {
            Debug.Log("No save file found to delete.");
        }
    }
}
