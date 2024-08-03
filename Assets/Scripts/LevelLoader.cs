using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] GameObject loadingScreen;
    [SerializeField] Slider slider;
    [SerializeField] Text progressText;
    [SerializeField] int nextSceneIndex;
    [SerializeField] float loadDuration = 5f;
    private float elapsedTime = 0f;
    void Start()
    {
        loadingScreen.SetActive(true);
        StartCoroutine(LoadLevel());
    }
    IEnumerator LoadLevel()
    {
        while (elapsedTime < loadDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsedTime / loadDuration);
            slider.value = progress;
            progressText.text = (progress * 100).ToString("F0") + "%";

            yield return null;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
