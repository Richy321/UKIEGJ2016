using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelSection : MonoBehaviour
{
    //public List<Enemy> Enemies;
    public int EnemiesLeft;
    public string Name;
    public Bounds BoundingBox;

    private bool sectionWon;
    public float blendTime;
    private GameObject playerChar;
    private GUIController sceneCont;

	// Use this for initialization
	void Start()
	{
        playerChar = GameObject.FindObjectOfType<Assets.Scripts.Character>().gameObject;
	    Enemy[] Enemies = GameObject.FindObjectsOfType<Enemy>();
        sceneCont = GameObject.FindObjectOfType<GUIController>();
        foreach (Enemy enemy in Enemies)
        {
            if(BoundingBox.Contains(enemy.transform.position))
            {
                enemy.mySection = this;
                EnemiesLeft++;
            }
        }
	}
	
	// Update is called once per frame
	void Update()
    {
        if (EnemiesLeft <= 0 && !sectionWon)
	    {
            //blend the houses in if enemies left is zero
            //find anything inside bounding box (in section)
            BlendableItem[] blendItems = GameObject.FindObjectsOfType<BlendableItem>();
            foreach (BlendableItem curItem in blendItems)
            {
                curItem.duration = blendTime;
                curItem.StartLerp();
            }
            sectionWon = true;
        }
        if(BoundingBox.Contains(playerChar.transform.position))
        {
            sceneCont.CurSection = this;
        }

	}
}
