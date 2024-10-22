using UnityEngine;

public class DontDestroyOnLoadScene : MonoBehaviour
{
    public GameObject[] objects;

    private void Awake()
    {
        foreach (var obj in objects) 
        { 
            DontDestroyOnLoad(obj);
        }
    }
}
