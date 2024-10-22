using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int coinsCount;

    public static Inventory instance;
    public Text coinsCountText;

    private void Awake()
    {
        if (instance != null) 
        {
            Debug.LogWarning("Il y a plus d'une instance de Inventory dans le scène");
            return;
        }

        instance = this;
    }

    public void AddCoins(int count)
    {
        coinsCount += count;
        coinsCountText.text = coinsCount.ToString();
    }
}
