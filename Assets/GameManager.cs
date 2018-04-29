using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    void GoBackToTitle()
    {
        if (Input.GetButton("Select"))
            SceneManager.LoadScene(0);
    }

    private void Update()
    {
        GoBackToTitle();
    }
}
