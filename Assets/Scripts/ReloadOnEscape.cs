using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadOnEscape : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
