using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlanetInfo : MonoBehaviour
{
    public TextAsset planetInfoFile;
    public TextMeshProUGUI[] data;
    public GameObject uiCanvas;
    GameObject planetText;
    string[] planetFile;

    public void Start()
    {
      string planetInfo = planetInfoFile.text;
      planetFile = planetInfo.Split('#');
    }

    public void Update()
    {
      // Check for user clicks
      if (Input.GetKeyDown(KeyCode.Mouse0))
      {
        if (planetText != null)
        {
          planetText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 1f);
        }

        uiCanvas.SetActive(false);
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
          // If hit object is a planet, proceed to show UI
          if (hit.transform.name != "Sun")
          {
             PlanetClicked(hit.transform.name.Split('|')[0]);
          }
        }
      }
    }

    public void PlanetClicked(string planetName)
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
      planetText = GameObject.Find(planetName + "|Parent");
      planetText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(0.3905492f, 0.8962264f, 0.3255162f);
    }
}
