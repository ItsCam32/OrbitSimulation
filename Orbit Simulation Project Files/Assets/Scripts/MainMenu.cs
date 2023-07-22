using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public Animator cameraAnim, menuAnim, backButtonAnim;
    public TextMeshProUGUI startText;
    public static bool simulationActive = false;

    private void Start()
    {
      // Intro menu animation
      menuAnim.Play("MenuEnter");
    }

    public void StartButtonClicked()
    {
      // Pan camera down to show planets
      cameraAnim.Play("CamPanDown");
      menuAnim.Play("MenuExit");
      backButtonAnim.Play("BackButtonEnter");

      StartCoroutine(ChangeText());
      simulationActive = true;
    }

    private IEnumerator ChangeText()
    {
        yield return new WaitForSeconds(1);
        startText.text = "Resume Simulation";
    }

    public void BackToMenuButtonClicked()
    {
      // Pan camera back to main menu
      cameraAnim.Play("CamPanUp");
      menuAnim.Play("MenuEnter");
      backButtonAnim.Play("BackButtonExit");
    }

    public void ExitButtonClicked()
    {
      Application.Quit();
    }
}
