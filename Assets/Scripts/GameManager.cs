using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    public PlayerActions player = null;
    private void Awake()
    {
        Instance = this;
    }
}
