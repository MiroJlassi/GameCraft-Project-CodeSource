using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuCode : MonoBehaviour
{
    public Text[] menuItems;
    private int currentIndex = 0;


    public AudioClip nav;
    public AudioClip select;
    public AudioSource sout;


    private void Start()
    {
        menuItems[0].fontStyle = FontStyle.Bold;
        menuItems[0].fontSize = 80;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangeSelection(-1);
            sout.clip = select;
            sout.Play();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangeSelection(1);
            sout.clip = select;
            sout.Play();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            SelectMenuItem();
        }
    }

    void ChangeSelection(int direction)
    {
        currentIndex += direction;
        if (currentIndex < 0)
        {
            currentIndex = menuItems.Length - 1;
        }
        else if (currentIndex >= menuItems.Length)
        {
            currentIndex = 0;
        }
        for (int i = 0; i < menuItems.Length; i++)
        {
            if (i == currentIndex)
            {
                menuItems[i].fontStyle = FontStyle.Bold;
                menuItems[i].fontSize = 80;

            }
            else
            {
                menuItems[i].fontStyle = FontStyle.Normal;
                menuItems[i].fontSize = 60;
            }
        }
    }

    void SelectMenuItem()
    {
        switch (currentIndex)
        {
            case 0:
                if (menuItems[currentIndex].name == "New Game (5)")
                {
                    SceneManager.LoadScene("Main Menu");
                }
                else
                {
                    Debug.Log("Start Game");
                    SceneManager.LoadScene("CutScene");
                }
                break;
            case 1:
                Debug.Log("About");
                SceneManager.LoadScene("About");
                break;
            case 2:
                Application.Quit();
                break;
        }
    }
}
