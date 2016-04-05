using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIController : MonoBehaviour {

    public Text EnemyCountText;
    public Text AreaNameText;
    public Text HealthText;
    public Text HealthText2;
    public Assets.Scripts.Character player;
    public Assets.Scripts.Character player2;
    public LevelSection CurSection;
    public Sprite ToolTipBackground;
    public Font ToolTipFont;
    public Camera ToolTipCamera;
    public GameObject toolTipCanvas;

	// Use this for initialization
	void Start () {
        //MakeToolTip(new Vector3(0,0,0), 200, 150, "This is a tooltip\n\nTest");
    }

	// Update is called once per frame
	void Update () {
        EnemyCountText.text = "Enemies remaining: " + CurSection.EnemiesLeft;
        AreaNameText.text = "Area: " + CurSection.Name;
        HealthText.text = "P1 HP: " + player.health;
        if (player2 != null) HealthText2.text =  "P2 HP: " + player2.health;
	}

    public GameObject MakeToolTip(Vector3 position, float width, float height, string text)
    {
        //Setup
        Vector3 screenPoint = ToolTipCamera.WorldToScreenPoint(position);
        Canvas toolCanvas = toolTipCanvas.GetComponent<Canvas>();
        
        //Container for tooltip
        GameObject stuffContainer = new GameObject();
        stuffContainer.transform.parent = toolTipCanvas.transform;
        RectTransform stuffTransform = stuffContainer.AddComponent<RectTransform>();
        stuffTransform.localPosition = screenPoint;
        stuffTransform.localPosition.Set(stuffTransform.localPosition.x - (ToolTipCamera.pixelWidth / 2),
            stuffTransform.localPosition.y - (ToolTipCamera.pixelHeight / 2),
            0) ;
        stuffTransform.localPosition = new Vector3(stuffTransform.localPosition.x - (ToolTipCamera.pixelWidth / 2), 
            stuffTransform.localPosition.y - (ToolTipCamera.pixelHeight / 2),
            stuffTransform.localPosition.z);
        stuffTransform.localScale = new Vector3(1, 1, 1);
        stuffTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        stuffTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);


        //Image
        GameObject toolTipImage = new GameObject();
        toolTipImage.transform.parent = stuffContainer.transform;
        toolTipImage.AddComponent<CanvasRenderer>();
        RectTransform imageTransform = toolTipImage.AddComponent<RectTransform>();
        imageTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        imageTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        imageTransform.localScale = new Vector3(1, 1, 1);
        imageTransform.localPosition = new Vector3(0, 0, 0);
        Image curImage = toolTipImage.AddComponent<Image>();
        curImage.sprite = ToolTipBackground;
        curImage.type = Image.Type.Sliced;

        //Text
        GameObject textItem = new GameObject();
        textItem.transform.parent = stuffContainer.transform;
        textItem.AddComponent<CanvasRenderer>();
        Text toolTipText = textItem.AddComponent<Text>();
        toolTipText.alignment = TextAnchor.MiddleLeft;
        toolTipText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        toolTipText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        toolTipText.rectTransform.localScale = new Vector3(1,1,1);
        toolTipText.rectTransform.localPosition = new Vector3(0, 0, 0);
        toolTipText.font = ToolTipFont;
        toolTipText.color = new Color(0, 0, 0);
        toolTipText.text = text;
        return toolTipCanvas;

    }
}
