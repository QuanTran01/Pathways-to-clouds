using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelButton : MonoBehaviour
{
    [SerializeField] public int sceneIndex;
    public void OnButtonClick() {
        SceneManager.LoadScene(sceneIndex);
    }

    public void SetIndex(int _index) {
        sceneIndex = _index;
        GetComponentInChildren<TextMeshProUGUI>().text = sceneIndex.ToString();
    }
}
