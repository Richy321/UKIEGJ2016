using UnityEngine;
using System.Collections;

public class Sign : MonoBehaviour {

    public float Radius;
    public string Text;

    private Assets.Scripts.Character[] players;
    private GUIController controller;
    private GameObject MyToolTip;
    private RectTransform ToolTipTransform;

	// Use this for initialization
	void Start () {
        players = GameObject.FindObjectsOfType<Assets.Scripts.Character>();
        controller = GameObject.FindObjectOfType<GUIController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (players.Length > 0)
        {
            if((players[0].transform.position - gameObject.transform.position).magnitude <= Radius && MyToolTip == null)
            {
                MyToolTip = controller.MakeToolTip(gameObject.transform.position, 100, 100, Text);
                ToolTipTransform = MyToolTip.GetComponent<RectTransform>();
                ToolTipTransform.localEulerAngles = new Vector3(0, 0, 0);
            }
            else if((players[0].transform.position - gameObject.transform.position).magnitude > Radius && MyToolTip != null)
            {
                Destroy(MyToolTip);
                MyToolTip = null;
            }
        }
	}
}
