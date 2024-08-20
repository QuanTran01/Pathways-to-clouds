using UnityEngine;
using TMPro;

public class Gem : MonoBehaviour
{
    public int gemCount;
    public TextMeshProUGUI gemText;
    public static Gem ist;
    private void Awake()
    {
        if (ist == null)
        {
            ist = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        gemText.text = gemCount.ToString();
    }
}
