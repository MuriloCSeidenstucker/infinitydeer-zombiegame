using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = -1;
    }
}
