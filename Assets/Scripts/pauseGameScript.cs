using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseGameScript : MonoBehaviour
{

 public GameObject PauseCanvas;
 private bool pauseGame;



    // Start is called before the first frame update
    void Start()
    {
        pauseGame = false;
        PauseCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

	 if (Input.GetKeyDown(KeyCode.Escape))
	 {
        Time.timeScale = 0;
            pauseGame = true;
            PauseCanvas.SetActive(true);
        
        }


	}



	public void ResumeGame()
	{
            pauseGame = false;
            Time.timeScale = 1;
            PauseCanvas.SetActive(false);
        
	}

	  public void MainMenuExit()
    {
   SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }

	 public void ExitGame()
    {
  Application.Quit();
    }

	public void RestartGame()
	{
	   SceneManager.LoadScene("MainScene");

	}
        
 
}
