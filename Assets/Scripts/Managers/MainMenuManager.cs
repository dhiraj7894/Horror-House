using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public bool isGameStarting = false;
    private void Start()
    {
        isGameStarting = false;
    }
    public void StartGame(int id)
    {
       if(!isGameStarting)
        {
            LeanTween.delayedCall(.2f, () => SceneManager.LoadScene(id));
            isGameStarting = true;
        }
    }

    public void SetLowQuality()
    {
        QualitySettings.SetQualityLevel(0, true); //Fastest Quality
    }
    public void SetMidQuality()
    {
        QualitySettings.SetQualityLevel(1, true); //Simple Graphics
    }
    public void SetHighQuality()
    {
        QualitySettings.SetQualityLevel(2, true); //Fantastic Graphics
    }


    void NotHappening()
    {
        //use these for later if you want

        QualitySettings.SetQualityLevel(1, true); //Fast Quality

        QualitySettings.SetQualityLevel(3, true); //Good Graphics

        QualitySettings.SetQualityLevel(4, true); //Beautiful Graphics

    }
}
