using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextButton : MonoBehaviour
{
    [SerializeField] string levelName;
    public void Next()
    {
        SceneManager.LoadScene(levelName);
    }
}
