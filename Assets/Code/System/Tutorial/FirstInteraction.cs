using UnityEngine;

public class FirstInteraction : MonoBehaviour
{
    private const string _key = "new user";

    private void Awake()
    {
        if (PlayerPrefs.GetInt(_key) == 0) return;
        CompleteTask();
    }
    public void CompleteTask()
    {
        PlayerPrefs.SetInt(_key, 1);
        Destroy(gameObject);
    }
}