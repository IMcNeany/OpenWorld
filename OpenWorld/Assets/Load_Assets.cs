using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Load_Assets : MonoBehaviour {
    public string SectionToLoad;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {

        DirectoryInfo dir = new DirectoryInfo(Application.streamingAssetsPath + "/" + SectionToLoad);
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
            //2            
            string wwwFilePath = "file://" + file.FullName.ToString();
            WWW www = new WWW(wwwFilePath);
            yield return www;
            //Texture2D newTankSkin = www.texture;
            //tankSkins.Add(newTankSkin);
            //tankSkinNames.Add(skinName);
            GameObject LoadSection = www.

           // Instantiate(file.);
        }
    }
        private void OnTriggerExit(Collider collision)
    {
        
    }
}
