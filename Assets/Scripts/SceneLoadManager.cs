using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
public class SceneLoadManager : MonoBehaviour
{
    [SerializeField] private Slider loadbar;
    [SerializeField] private GameObject loadPanel;

    public void SceneLoad()
    {
        loadPanel.SetActive(true);
        GameManager.Instance.SelectLevel();
        StartCoroutine(LoadAsync(GameManager.Instance._CurrentLevel.scene_number));
    }
    IEnumerator LoadAsync(int sceneIndex)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!asyncOperation.isDone)
        {
            Debug.Log(asyncOperation.progress);
            loadbar.value = asyncOperation.progress / 0.9f;
            yield return null;
        }
        if(loadbar.value >= 0.99)
            loadPanel.SetActive(false);
    }
}
