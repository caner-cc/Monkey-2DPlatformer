using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    public void ChangeToGameScene()
    {
        SceneManager.LoadScene("GameScene2");
        Time.timeScale = 1;
    }

}
