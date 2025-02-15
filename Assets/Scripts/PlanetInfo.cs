using UnityEngine;
using TMPro;

public class PlanetInfo : MonoBehaviour
{
    // vv Private Exposed vv //

    [SerializeField]
    private TextAsset planetInfoFile;

    [SerializeField]
    private TextMeshProUGUI[] data;

    [SerializeField]
    private GameObject uiCanvas;

    // vv Private vv //

    private GameObject planetText;
    private string[] planetFile;

    ////////////////////////////////////////

    #region Private Functions

    private void Start()
    {
        string planetInfo = planetInfoFile.text;
        planetFile = planetInfo.Split('#');
    }

    private void Update()
    {
        // Check for user clicks
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (planetText)
            {
                planetText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.white;
            }

            uiCanvas.SetActive(false);
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                PlanetClicked(hit.transform.name);
            }
        }
    }

    private void PlanetClicked(string planetName)
    {
        // Display information about planet from file
        uiCanvas.SetActive(true);

        foreach (string line in planetFile)
        {
            string[] sections = line.Split(':');
            if (sections[0].Contains(planetName))
            {
                // Planet found, read info
                for (int i = 0; i < data.Length; i++)
                {
                    data[i].text = sections[i];
                }
            }
        }

        // Highlight selected planet's text
        planetText = GameObject.Find(planetName);
        planetText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(0.3905492f, 0.8962264f, 0.3255162f);
    }
    #endregion
}
