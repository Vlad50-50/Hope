using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuCTRL : MonoBehaviour
{
    public void LoadGameSceane() {
        SceneManager.LoadScene("SampleScene");
    }
}
