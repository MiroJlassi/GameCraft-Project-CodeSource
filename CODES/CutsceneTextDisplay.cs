using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CutsceneTextDisplay : MonoBehaviour
{
    public Text storyText;
    public string[] sentences;
    public float textSpeed = 0.1f;

    public bool test = false;
    private void Start()
    {
        StartCoroutine(DisplaySentences());
    }

    private void Update()
    {
        if (test == true && Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("next scene");
            SceneManager.LoadScene("Training");
        }
    }

    IEnumerator DisplaySentences()
    {
        foreach (string sentence in sentences)
        {
            for (int i = 0; i <= sentence.Length; i++)
            {
                storyText.text = sentence.Substring(0, i);
                yield return new WaitForSeconds(textSpeed);
            }
            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(1f);
        storyText.text = "Press ENTER to Start...";
        test = true;
    }
}
