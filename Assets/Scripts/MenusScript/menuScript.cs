using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class menuScript : MonoBehaviour {

    public Canvas quitMenu;
    public Image Title;
    public Image MonitorOff;
    public Button startText;
    public Button instructions;
    public Button options;
    public Button exitText;
    public Button PCbutton;
    public Image GreenDot;
    public Image RedDot;


    private bool ScreenOff;
    private bool WasEnabled;
    public AudioClip Clack;
    public AudioClip Click;
    // Use this for initialization
    void Start () 
    {
        Title = Title.GetComponent<Image>();
        MonitorOff = MonitorOff.GetComponent<Image>();
        quitMenu = quitMenu.GetComponent<Canvas>();
        startText = startText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();
        instructions = instructions.GetComponent<Button>();
        options = options.GetComponent<Button>();
        PCbutton = PCbutton.GetComponent<Button>();
        GreenDot = GreenDot.GetComponent<Image>();
        RedDot = RedDot.GetComponent<Image>();
        quitMenu.enabled = false;
        ScreenOff = false;
        MonitorOff.enabled = false;
        RedDot.enabled = false;

        WasEnabled = false;

	}

    public void ExitPress()
    {
        quitMenu.enabled = true;
        startText.enabled = false;
        instructions.enabled = false;
        options.enabled = false;
        exitText.enabled = false;
        ClickSound();
    }

    public void NoPress()
    {
        quitMenu.enabled = false;
        startText.enabled = true;
        instructions.enabled = true;
        options.enabled = true;
        exitText.enabled = true;
        ClickSound();
    }

    public void StartLevel()
    {
        ClickSound();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Test It! Level1Rizo");
    }

    public void ExitGame() 
    {
        ClickSound();
        Application.Quit();
    }

    public void Shutdown()
    {
        if (ScreenOff) //enciendo monitor
        {
            Title.enabled = true;
            startText.enabled = true;
            instructions.enabled = true;
            options.enabled = true;
            exitText.enabled = true;
            ScreenOff = false;
            MonitorOff.enabled = false;
            RedDot.enabled = false;
            GreenDot.enabled = true;
            ClackSound();
            print("encendido");

            if (WasEnabled)
            {
                quitMenu.enabled = true;
                WasEnabled = false;
            }
        }
        else//apago monitor
        {
            Title.enabled = false;
            startText.enabled = false;
            instructions.enabled = false;
            options.enabled = false;
            exitText.enabled = false;
            ScreenOff = true;
            MonitorOff.enabled = true;
            RedDot.enabled = true;
            GreenDot.enabled = false;
            ClackSound();
            print("Apagado");
            if (quitMenu.enabled)
            {
                quitMenu.enabled = false;
                WasEnabled = true;
            }
        }
    }
    public void ClickSound()
    {
        AudioSource.PlayClipAtPoint(Click, Camera.main.transform.position);
    }

    public void ClackSound()
    {
        AudioSource.PlayClipAtPoint(Clack, Camera.main.transform.position);
    }
}
