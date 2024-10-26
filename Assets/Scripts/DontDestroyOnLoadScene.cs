using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoadScene : MonoBehaviour
{
    public GameObject[] objects;

    public static DontDestroyOnLoadScene instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de DontDestroyOnLoadScene dans le scène");
            return;
        }

        instance = this;

        foreach (var obj in objects)
        {
            DontDestroyOnLoad(obj);
        }
    }

    public void RemoveFromDontDestroyOnLoad()
    {
        foreach (var obj in objects)
        {
            SceneManager.MoveGameObjectToScene(obj, SceneManager.GetActiveScene());
        }
    }
}
