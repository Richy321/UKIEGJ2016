using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelSection : MonoBehaviour
{
    //public List<Enemy> Enemies;
    public int EnemiesLeft;
    private int startEnemies;
    public string Name;
    private Bounds BoundingBox;

    private int prevEnemies;
    private bool sectionWon;
    public float blendTime;
    private GameObject playerChar;
    private GUIController sceneCont;
    private List<BlendableItem> myBlendables;

	// Use this for initialization
	void Start()
	{
        myBlendables = new List<BlendableItem>();
        playerChar = GameObject.FindObjectOfType<Assets.Scripts.Character>().gameObject;
	    Enemy[] Enemies = GameObject.FindObjectsOfType<Enemy>();
        sceneCont = GameObject.FindObjectOfType<GUIController>();
        BoundingBox.center = gameObject.transform.position;
        BoundingBox.size = gameObject.transform.localScale;
        if (BoundingBox.Contains(playerChar.transform.position))
        {
            sceneCont.CurSection = this;
        }

        foreach (Enemy enemy in Enemies)
        {
            if(BoundingBox.Contains(enemy.transform.position))
            {
                enemy.mySection = this;
                EnemiesLeft++;
            }
        }
        startEnemies = EnemiesLeft;

        BlendableItem[] blendables = GameObject.FindObjectsOfType<BlendableItem>();
        foreach (BlendableItem blendable in blendables)
        {
            print("test");
            if (BoundingBox.Contains(blendable.gameObject.transform.position))
            {
                myBlendables.Add(blendable);
            }
        }
    }
	
	// Update is called once per frame
	void Update()
    {

        if(EnemiesLeft < prevEnemies)
        {
            foreach (BlendableItem curItem in myBlendables)
            {
                curItem.duration = blendTime;
                curItem.LerpTo(1 - (EnemiesLeft / startEnemies));
            }
            prevEnemies = EnemiesLeft;
        }

        if (EnemiesLeft <= 0 && !sectionWon)
	    {
            sectionWon = true;
            foreach (BlendableItem curItem in myBlendables)
            {
                curItem.duration = blendTime;
                curItem.LerpTo(1.0f);
            }
        }
        if(BoundingBox.Contains(playerChar.transform.position))
        {
            sceneCont.CurSection = this;
        }
        else if(EnemiesLeft > 0)
        {
            prevEnemies = EnemiesLeft;
        }

	}
}
