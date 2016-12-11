using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class pauseScript : MonoBehaviour {

    public Canvas quitMenu;
    public Button resumeText;
    public Button exitText;
    [HideInInspector]
    public bool gamePaused = false;

    // Use this for initialization
    void Start()
    {
        quitMenu = quitMenu.GetComponent<Canvas>();
        resumeText = resumeText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();
        quitMenu.enabled = false;
    }

    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                showPaused();
            }
            else if (Time.timeScale == 0)
            {
                hidePaused();
            }
        }
    }

    void showPaused() 
    {
        quitMenu.enabled = true;
        resumeText.enabled = true;
        exitText.enabled = true;
        gamePaused = true;
        Time.timeScale = 0;
    }

    public void hidePaused()
    {
        quitMenu.enabled = false;
        resumeText.enabled = false;
        exitText.enabled = false;
        gamePaused = false;
        Time.timeScale = 1;
    }

    public void ExitMenu()
    {
        Time.timeScale = 1;
        gamePaused = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartMenu");
    }
}
