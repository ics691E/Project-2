using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = true;
    }
    public void ResetGame()
    {
        SceneManager.LoadScene("Map1");
    }
}
