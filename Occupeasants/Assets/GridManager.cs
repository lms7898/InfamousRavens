using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class GridManager : MonoBehaviour {

    public GameObject floor_tile;

	// Use this for initialization
	void Start () {

        //string[][] textFile = ReadLevel();

        for (int i = 0; i < 5; i++)
        {
            Instantiate(floor_tile);
            floor_tile.transform.position = new Vector3(0 + i*2, 0, -4);
        }
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /*
     * Reads in the level from a text file
     * Code from http://answers.unity3d.com/questions/577889/create-level-based-on-xmltxt-file.html
     * */

    string[][] ReadLevel(string file)
    {
        string levelText = System.IO.File.ReadAllText(file);
        string[] fileLines = Regex.Split(levelText, "\r\n");
        int rows = fileLines.Length;

        string[][] level = new string[rows][];
        for (int i = 0; i < fileLines.Length; i++)
        {
            string[] lineInfo = Regex.Split(fileLines[i], " ");
            level[i] = lineInfo;
        }
        return level;
    }
}
