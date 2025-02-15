using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour
{
    // vv Private Exposed vv //

    [SerializeField]
    private Animator cameraAnimator;

    [SerializeField]
    private Animator menuAnimator;

    [SerializeField]
    private Animator backButtonAnimator;

    [SerializeField]
    private TextMeshProUGUI startText;

    [SerializeField]
    private PlanetOrbit[] planets;

    // vv Public vv //

    public static MainMenu Instance { get; private set; }

    ////////////////////////////////////////

    #region Private Functions

    private void Start()
    {
        if (Instance && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // Intro menu animation
        menuAnimator.Play("MenuEnter");
    }

    private void ChangeText()
    {
        startText.text = "Resume Simulation";
    }
    #endregion

    #region Public Functions

    public void OnStartButtonClicked()
    {
        // Pan camera down to show planets
        cameraAnimator.Play("CamPanDown");
        menuAnimator.Play("MenuExit");
        backButtonAnimator.Play("BackButtonEnter");

        Invoke("ChangeText", 1.0f);
       
        foreach (PlanetOrbit planet in planets)
        {
            planet.EnableOrbit();
        }
    }

    public void OnBackButtonClicked()
    {
        // Pan camera back to main menu
        cameraAnimator.Play("CamPanUp");
        menuAnimator.Play("MenuEnter");
        backButtonAnimator.Play("BackButtonExit");
    }

    public void OnExitButtonClicked()
    {
        Application.Quit();
    }
    #endregion
}
