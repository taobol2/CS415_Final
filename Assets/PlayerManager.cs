using UnityEngine;

public class PlayerManager : MonoBehaviour, ISaveManager
{
    public static PlayerManager instance;
    public Player player;

    private void Awake()
    {
        if (instance != null) { 
            Destroy(instance);
        }else
            instance = this;
    }

    public void LoadData(GameData _data)
    {


    }

    public void SaveData(GameData _data)
    {


    }



}
