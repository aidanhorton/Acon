using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject HomePage;
    public GameObject PlayPage;
    public GameObject OptionsPage;

    public void Start()
    {
        this.HomePage.SetActive(true);
        this.PlayPage.SetActive(false);
        this.OptionsPage.SetActive(false);
    }

    public void Play()
    {
        this.HomePage.SetActive(false);
        this.PlayPage.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void OpenOptions()
    {
        this.HomePage.SetActive(false);
        this.OptionsPage.SetActive(true);
    }
}
