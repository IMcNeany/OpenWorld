using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.AI;


public class LoadInTile : MonoBehaviour {
    int width =3;
    int height = 4;
    int row = 10;
    int col = 10;
    float scale = 10.0f;
    char[] tile_data;
    private int[] tileObject;
    Terrain terrain;
    public GameObject[] edgePieces;
    public GameObject[] houses;
    public GameObject[] AI;
    GameObject instance;
    List<GameObject> sections;
    int SectionToLoad;
    public Texture2D[] grassTexture;
    Transform camera;
    bool stopDistance = false;

    // Use this for initialization
    void Start () {
        tile_data = new char[row * col];
        tileObject = new int[row * col];
        sections = new List<GameObject>();
        CreateGrid();
        camera = Camera.main.transform;
       
      //  LoadFromFile(2);
      //  LoadFromFile(1);
        
    }
	
	// Update is called once per frame
	void Update () {
        if (!stopDistance)
        {
           CheckDistance();
          
        }
	}

    void CreateGrid()
    {
        int sectionNo = 1;
        int x = 0;
        int z = 0;

        TerrainData terrain_Data = new TerrainData();

        // terrain_Data.size = new Vector3(100, 100, 50);
        terrain_Data.size = new Vector3(6.25f, 1, 6.25f);
        terrain_Data.heightmapResolution = 512;
        terrain_Data.baseMapResolution = 1024;
        terrain_Data.SetDetailResolution(1024, 16);
        SplatPrototype[] terrainTexture = new SplatPrototype[1];
        
        terrainTexture[0] = new SplatPrototype();
        terrainTexture[0].texture = grassTexture[0];
        terrain_Data.splatPrototypes = terrainTexture;
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                GameObject section = new GameObject("Section" + sectionNo);
                section.transform.position= new Vector3(x, 0, z);
                section.AddComponent<Terrain>();
                section.AddComponent<TerrainCollider>();
                terrain = section.GetComponent<Terrain>();
                terrain.terrainData = terrain_Data;
                TerrainCollider terrainCollider = section.GetComponent<TerrainCollider>();
                terrainCollider.terrainData = terrain_Data;
                sections.Add(section);
               
                z += 100;
                sectionNo++;
            }

            x += 100;
            z = 0;
        }
    }

    void CheckDistance()
    {
        for(int i = 0; i < sections.Count;i++)
        {
          
            if(Vector3.Distance(camera.position, sections[i].transform.position) < 150)
            {
              
                if(sections[i].transform.childCount == 0)
                {
                   stopDistance = true;
                   LoadFromFile(i + 1);
                    SetUpAI(i);
                    // i--;
                }
            }
            else
            {
                destroy(i+1);
               // i--;
            }
        }
        //for each section check if they have children
        //then check how far away the camera is
        //either destory or load
    }

    void destroy(int destory)
    {
        GameObject sectionToDestroy = GameObject.Find("Section" + destory);

        for(int i =0; i < sectionToDestroy.transform.childCount; i++)
        {
           Transform child = sectionToDestroy.transform.GetChild(i);
            Destroy(child.gameObject);
        }
    }
    void SetUpAI(int i)
    { 
      
        sections[i].AddComponent<CreateGrid>();
        
    }

    void LoadFromFile(int Load)
    {
        SectionToLoad = Load;
        DirectoryInfo dir = new DirectoryInfo(Application.streamingAssetsPath + "/" + "Section" + SectionToLoad + ".txt");
       
        //FileInfo[] file = dir.GetFiles("Section" + SectionToLoad + ".txt");
        FileInfo[] allFiles = dir.GetFiles("*.*");
     
        foreach (FileInfo file in allFiles)
       {

            // StartCoroutine("LoadSection", file);
            LoadSection(file);
        }
    }

    void LoadSection(FileInfo file)
    {
        Debug.Log(SectionToLoad + "Section yo load");
        if (file.Name.Contains("meta"))
        {
           // yield break;
        }
        else if(file.Name.Contains("Section" + SectionToLoad + ".txt"))
        {
            string FileWithoutExtension = Path.GetFileNameWithoutExtension(file.ToString());
            string[] fileData = FileWithoutExtension.Split(" "[0]);
            string fileName = fileData[0];
            print(fileName);            
            string wwwFilePath = "file://" + file.FullName.ToString();
            WWW www = new WWW(wwwFilePath);
            //yield return www;

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
                    count++;
                }
            }
            reader.Close();
            CreateSection();
        }
        else
        {
           // yield break;
        }
        
    }

    private void CreateSection()
    {
        int count = 0;
        GameObject parent = GameObject.Find("Section" + SectionToLoad);
        parent.isStatic = true;
       // Debug.Log("Section" + SectionToLoad + ".txt");
        for (int i = 0; i < col; i++)
        {
            for (int j = 0; j < row; j++)
            {
                
                if (tile_data[count] == '0')
                {
      
                    // instan.transform.position = new Vector3(instan.transform.position.x, 2.5f, instan.transform.position.z);
                }
                else if(tile_data[count] == '1')
                {
                    
                    instance = Instantiate(edgePieces[0] , (new Vector3((j * scale) + parent.transform.position.x, 0.5f, (i * scale) + parent.transform.position.z)), Quaternion.identity);
                    instance.name = "tile" + i + j;
                    //instance.transform.parent = 
                   // GameObject parent = GameObject.Find("Section" + SectionToLoad);
                    instance.transform.parent = parent.transform;
                }
                else if (tile_data[count] == '2')
                {
                   int k = Random.Range(0, 4);
                   instance = Instantiate(houses[k], (new Vector3((j * scale) + parent.transform.position.x, 0.0f, (i * scale) + parent.transform.position.z)), Quaternion.identity);
                   instance.name = "tile" + i + j;
                   instance.transform.parent = parent.transform;
                }
                else if (tile_data[count] == '3')
                { 
                    instance = Instantiate(AI[0], (new Vector3((j * scale) + parent.transform.position.x, 0.0f, (i * scale) + parent.transform.position.z)), Quaternion.identity);
                    instance.name = "AI";
                    instance.AddComponent<AI>();
                    instance.transform.parent = parent.transform;
                }
                count++;
            }
        }

        //Nav mesh Baking

       
        stopDistance = false;
    }
    
}
