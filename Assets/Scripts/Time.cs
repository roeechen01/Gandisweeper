using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Time : MonoBehaviour {

    private int time = 0, bestTime;
    public Text timeText, bestTimeText;
    private string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
    private string path;
    public bool blichMode = false;

    // Use this for initialization
    void Start () {
        time = 0;
        timeText.text = "Time: " + this.time;

        if (blichMode)
        {
            Directory.CreateDirectory(@"W:\hs");
            path = @"W:\hs\highscore.txt";
        }
        else
        {
            Directory.CreateDirectory(documentsPath + "\\Gandisweeper");
            path = documentsPath + "\\Gandisweeper\\highscore.txt";
        }
        print(path);
        try
        {
            bestTime = int.Parse(File.ReadAllText(path));
            bestTimeText.text = "Best time: " + bestTime;
        }
        catch
        {
            bestTime = int.MaxValue;
            bestTimeText.text = "No wins yet, set the best time";
        }
    }

    public void StartTime()
    {
        InvokeRepeating("UpdateTime", 1f, 1f);
    }


    /*public void StartTime()
    {
        CancelInvoke("UpdateTime");
        InvokeRepeating("UpdateTime", 1f, 1f);
        time = 0;
        timeText.text = "Time: " + this.time;
    }*/

    // Update is called once per frame
    void Update () {
		
	}

    void UpdateTime()
    {
        if (!BoxManager.OVER && !BoxManager.FIRST_MOVE)
        {
            time++;
            timeText.text = "Time: " + this.time;
        }

    }

    public void UpdateHighScore()
    {
        if(time < bestTime && BoxManager.length == 10)
        {
            File.WriteAllText(path, time.ToString());
        }
    }
}
