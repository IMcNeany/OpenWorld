using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class LoadInTile : MonoBehaviour {
    int row = 10;
    int col = 10;
    float scale = 10.0f;
    char[] tile_data;
    private int[] tileObject;
    public Terrain terrain;
    public GameObject[] edgePieces;
    public GameObject[] houses;
    GameObject instance;
    int SectionToLoad;
    // Use this for initialization
    void Start () {
        tile_data = new char[row * col];
        tileObject = new int[row * col];
        LoadFromFile(1);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadFromFile(int Load)
    {
        SectionToLoad = Load;
        DirectoryInfo dir = new DirectoryInfo(Application.streamingAssetsPath + "/" + "Section" + SectionToLoad + ".txt");
        print("Streaming Assets Path: " + Application.streamingAssetsPath);
        FileInfo[] allFiles = dir.GetFiles("*.*");
        foreach (FileInfo file in allFiles)
        {
           StartCoroutine("LoadSection", file);
        }
    }

    IEnumerator LoadSection(FileInfo file)
    {
        if (file.Name.Contains("meta"))
        {
            yield break;
        }
        else
        {
            string FileWithoutExtension = Path.GetFileNameWithoutExtension(file.ToString());
            string[] fileData = FileWithoutExtension.Split(" "[0]);
            string fileName = fileData[0];
                        
            string wwwFilePath = "file://" + file.FullName.ToString();
            WWW www = new WWW(wwwFilePath);
            yield return www;

            StreamReader reader = new StreamReader(Application.streamingAssetsPath +"/" + fileName + ".txt");
            int count = 0;
            for (int i = 0; i < col; i++)
            {
                string line;
                line = reader.ReadLine();
                char[] c = new char[line.Length];

                for (int j = 0; j < line.Length; j++)
                {
                    c[j] = line[j];
                    tile_data[count] = c[j];
                    Debug.Log("CJ" + c[j]);
                    count++;
                }
            }
            reader.Close();
        }
        CreateSection();
    }

    private void CreateSection()
    {
        int count = 0;
        GameObject parent = GameObject.Find("Section" + SectionToLoad);
        for (int i = 0; i < col; i++)
        {
            for (int j = 0; j < row; j++)
            {
                Debug.Log("data count " + tile_data[count]);
                if (tile_data[count] == '0')
                {
                    Debug.Log("hit0" );
                    
                    // instan.transform.position = new Vector3(instan.transform.position.x, 2.5f, instan.transform.position.z);
                }
                else if(tile_data[count] == '1')
                {
                    Debug.Log("hit1");
                    instance = Instantiate(edgePieces[0] , (new Vector3(j * scale, 0.1f, i * scale)), Quaternion.identity);
                    instance.name = "tile" + i + j;
                    //instance.transform.parent = 
                   // GameObject parent = GameObject.Find("Section" + SectionToLoad);
                    instance.transform.parent = parent.transform;
                }
                else if (tile_data[count] == '2')
                {
                   int k = Random.Range(0, 4);
                    Debug.Log("rand" + k);
                   instance = Instantiate(houses[k], (new Vector3(j * scale, 0.1f, i * scale)), Quaternion.identity);
                   instance.name = "tile" + i + j;
                   instance.transform.parent = parent.transform;
                }
                
                //instance.transform.parent = maze_holder.transform;
                count++;
            }
        }
    }
}
