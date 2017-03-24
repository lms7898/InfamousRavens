using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class GridManager : MonoBehaviour {

    public GameObject floor_tile;
	public GameObject ceiling_tile;
	public GameObject door_tile;
	public GameObject rug_tile;
	public GameObject wall_tile;
	public GameObject wood_floor_tile;

    public GameObject level;

	// Use this for initialization
	void Start () {
        string[][] textFile = ReadLevel("Assets/level1.txt");
        float x = level.transform.position.x - level.GetComponent<Renderer>().bounds.size.x / 2;
        float y = level.transform.position.y - level.GetComponent<Renderer>().bounds.size.y / 2 - 0.7f;

        LayoutGrid(textFile, x, y);
        
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

    // Reads the text files in (upside down)
    void LayoutGrid(string[][] textFile, float startX, float startY)
    {
		Color tmp;

        for (int i = 0; i < textFile.Length; i++)
        {
            for (int j = 0; j < textFile[i].Length; j++)
            {
                // Creates the trap grid to go on top of the level
				Instantiate(floor_tile);
				floor_tile.transform.position = new Vector3(startX + j * 1.68f, startY + i * 1.68f, 0);

                // Checks what char is at that position and assigns the correct tile as well as if its an active trap placing area
				switch(textFile[i][j]) {
				case ("C"):
						Instantiate (ceiling_tile);
						ceiling_tile.transform.position = new Vector3(startX + j * 1.68f, startY + i * 1.68f, 2);

						floor_tile.GetComponent<SpriteRenderer>().color = Color.black;
						tmp = floor_tile.GetComponent<SpriteRenderer>().color;
						tmp.a = 0.0f;
						floor_tile.GetComponent<SpriteRenderer>().color = tmp;

						break;

					case ("F"):
						Instantiate(wood_floor_tile);
						wood_floor_tile.transform.position = new Vector3(startX + j * 1.68f, startY + i * 1.68f, 2);

						floor_tile.GetComponent<SpriteRenderer>().color = Color.gray;
						tmp = floor_tile.GetComponent<SpriteRenderer>().color;
						tmp.a = 0.2f;
						floor_tile.GetComponent<SpriteRenderer>().color = tmp;

						break;

					case ("R"):
						Instantiate(rug_tile);
						rug_tile.transform.position = new Vector3(startX + j * 1.68f, startY + i * 1.68f, 2);

						floor_tile.GetComponent<SpriteRenderer>().color = Color.gray;
						tmp = floor_tile.GetComponent<SpriteRenderer>().color;
						tmp.a = 0.2f;
						floor_tile.GetComponent<SpriteRenderer>().color = tmp;

						break;

					case ("W"):
						Instantiate(wall_tile);
						wall_tile.transform.position = new Vector3(startX + j * 1.68f, startY + i * 1.68f, 2);

						floor_tile.GetComponent<SpriteRenderer>().color = Color.black;
						tmp = floor_tile.GetComponent<SpriteRenderer>().color;
						tmp.a = 0.0f;
						floor_tile.GetComponent<SpriteRenderer>().color = tmp;

						break;

					case ("D"):
						Instantiate(door_tile);
						door_tile.transform.position = new Vector3(startX + j * 1.68f, startY + i * 1.68f, 2);

						floor_tile.GetComponent<SpriteRenderer>().color = Color.black;
						tmp = floor_tile.GetComponent<SpriteRenderer>().color;
						tmp.a = 0.0f;
						floor_tile.GetComponent<SpriteRenderer>().color = tmp;
 
						break;

					case ("N"):
						floor_tile.GetComponent<SpriteRenderer>().color = Color.black;
						tmp = floor_tile.GetComponent<SpriteRenderer>().color;
						tmp.a = 0.0f;
						floor_tile.GetComponent<SpriteRenderer>().color = tmp;

						break;

					default:
                        floor_tile.GetComponent<SpriteRenderer>().color = Color.black;
                        tmp = floor_tile.GetComponent<SpriteRenderer>().color;
                        tmp.a = 0.0f;
                        floor_tile.GetComponent<SpriteRenderer>().color = tmp;

                        break;
				}
            }
        }
    }
}
