using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIController : MonoBehaviour {

    public Text EnemyCountText;
    public Text AreaNameText;
    public LevelSection CurSection;

	// Use this for initialization
	void Start () {
    }

	// Update is called once per frame
	void Update () {
        EnemyCountText.text = "Enemies remaining: " + CurSection.EnemiesLeft;
        AreaNameText.text = "Area: " + CurSection.Name;
	}
}
