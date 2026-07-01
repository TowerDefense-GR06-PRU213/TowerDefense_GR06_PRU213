using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;

    public int gold;
    public int lives;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            Debug.Log("PLAYER DATA CREATED");
        }
    }
}