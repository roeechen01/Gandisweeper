using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BoxManager : MonoBehaviour
{
    public static int length = 10;
    public static int width = length;
    public static int height = length;
    private int gandisToPut_NOT_CHANGED = 20;
    private int gandisToPut;
    public static bool OVER = false;
    public static bool FIRST_MOVE = true;
    private GameText txt;
    private Time score;
    public bool questionMode = true;

    public Text flagsText;
    private int flagsLeft;

    private Box[,] boxes = new Box[width, height];
    private Box[] boxes1Darr = new Box[width * height];

    // Use this for initialization
    void Start()
    {
        gandisToPut = gandisToPut_NOT_CHANGED;
        flagsLeft = gandisToPut_NOT_CHANGED;
        txt = FindObjectOfType<GameText>();
        score = FindObjectOfType<Time>();
        flagsText.text = "Gandis: " + flagsLeft;
        SetBoxes1dArray();
        SetBoxes2dArray();
        SetBoxesLocation();

    }

    void SetBoxes1dArray()
    {
        boxes1Darr[0] = FindObjectOfType<Box>();
        for(int i = 1; i < boxes1Darr.Length; i++)
            boxes1Darr[i] = Instantiate(boxes1Darr[0]);
    }



    public void SetGandis(int[] position)
    {
        SetGandisInBoxes(gandisToPut, position);
        SetNumGandis();
    }

    //Setting the boxes in a 2D array by the board order.
    void SetBoxes2dArray()
    {
        int index = 0;
        for (int i = 0; i < boxes.GetLength(0); i++)
        {
            for (int j = 0; j < boxes.GetLength(1); j++)
            {
                int[] position = { i, j };
                boxes[i, j] = boxes1Darr[index];
                boxes[i, j].SetBox(boxes1Darr[0], position);
                index++;
            }
        }
    }

    //Setting the boxes X&Y vectors location on the screen.
    void SetBoxesLocation()
    {
        for (int i = 0; i < boxes.GetLength(0); i++)
        {
            for (int j = 0; j < boxes.GetLength(1); j++)
            {
                if (boxes[i, j] != boxes[0, 0])
                {
                    float x = boxes[0, 0].GetPosotion().x + i * 0.82f;
                    float y = boxes[0, 0].GetPosotion().y - j * 0.82f;
                    boxes[i, j].SetPosition(x, y);
                }

            }
        }
    }

    public void WinCheck()
    {
        bool win = true;
        foreach (Box box in boxes1Darr)
        {
            if (box.IsHidden() && !box.IsGandi())
                win = false;
        }
        if (win)
        {
            score.UpdateHighScore();
            OVER = true;
            txt.SetOverMode(true);
            foreach (Box box in boxes1Darr)
            {
                if (box.IsGandi())
                    box.WinFlag();
                else box.Show();
            }
        }
    }

    public void Lose()
    {
        OVER = true;
        txt.SetOverMode(false);
        foreach (Box box in boxes1Darr)
            box.LoseGandi();
    }

    public void FlagsLeftChange(bool more)
    {
        if (more)
            flagsLeft++;
        else flagsLeft--;
        flagsText.text = "Gandis: " + this.flagsLeft;
    }

    public void UncoverEmptyBoxes(int[] position)
    {
        int i = position[0];
        int j = position[1];
        if (i == 0 && j == 0)
        {
            if (boxes[0, 1].IsClosed())
                boxes[0, 1].Uncover();
            if (boxes[1, 0].IsClosed())
                boxes[1, 0].Uncover();
            if (boxes[1, 1].IsClosed())
                boxes[1, 1].Uncover();
        }

        else if (i == height - 1 && j == width - 1)
        {
            if (boxes[height - 1, width - 2].IsClosed())
                boxes[height - 1, width - 2].Uncover();
            if (boxes[height - 2, width - 1].IsClosed())
                boxes[height - 2, width - 1].Uncover();
            if (boxes[height - 2, width - 2].IsClosed())
                boxes[height - 2, width - 2].Uncover();
        }

        else if (i == height - 1 && j == 0)
        {
            if (boxes[height - 2, 0].IsClosed())
                boxes[height - 2, 0].Uncover();
            if (boxes[height - 1, 1].IsClosed())
                boxes[height - 1, 1].Uncover();
            if (boxes[height - 2, 1].IsClosed())
                boxes[height - 2, 1].Uncover();
        }

        else if (i == 0 && j == width - 1)
        {
            if (boxes[1, width - 1].IsClosed())
                boxes[1, width - 1].Uncover();
            if (boxes[0, width - 2].IsClosed())
                boxes[0, width - 2].Uncover();
            if (boxes[1, width - 2].IsClosed())
                boxes[1, width - 2].Uncover();
        }

        else if (i == 0)
        {

            if (boxes[0, j - 1].IsClosed())
                boxes[0, j - 1].Uncover();
            if (boxes[0, j + 1].IsClosed())
                boxes[0, j + 1].Uncover();
            if (boxes[1, j - 1].IsClosed())
                boxes[1, j - 1].Uncover();
            if (boxes[1, j].IsClosed())
                boxes[1, j].Uncover();
            if (boxes[1, j + 1].IsClosed())
                boxes[1, j + 1].Uncover();
        }

        else if (i == height - 1)
        {

            if (boxes[height - 1, j - 1].IsClosed())
                boxes[height - 1, j - 1].Uncover();
            if (boxes[height - 1, j + 1].IsClosed())
                boxes[height - 1, j + 1].Uncover();
            if (boxes[height - 2, j - 1].IsClosed())
                boxes[height - 2, j - 1].Uncover();
            if (boxes[height - 2, j].IsClosed())
                boxes[height - 2, j].Uncover();
            if (boxes[height - 2, j + 1].IsClosed())
                boxes[height - 2, j + 1].Uncover();
        }

        else if (j == 0)
        {

            if (boxes[i - 1, 0].IsClosed())
                boxes[i - 1, 0].Uncover();
            if (boxes[i + 1, 0].IsClosed())
                boxes[i + 1, 0].Uncover();
            if (boxes[i - 1, 1].IsClosed())
                boxes[i - 1, 1].Uncover();
            if (boxes[i, 1].IsClosed())
                boxes[i, 1].Uncover();
            if (boxes[i + 1, 1].IsClosed())
                boxes[i + 1, 1].Uncover();
        }

        else if (j == width - 1)
        {

            if (boxes[i - 1, width - 1].IsClosed())
                boxes[i - 1, width - 1].Uncover();
            if (boxes[i + 1, width - 1].IsClosed())
                boxes[i + 1, width - 1].Uncover();
            if (boxes[i - 1, width - 2].IsClosed())
                boxes[i - 1, width - 2].Uncover();
            if (boxes[i, width - 2].IsClosed())
                boxes[i, width - 2].Uncover();
            if (boxes[i + 1, width - 2].IsClosed())
                boxes[i + 1, width - 2].Uncover();
        }

        else
        {
            if (boxes[i - 1, j - 1].IsClosed())
                boxes[i - 1, j - 1].Uncover();
            if (boxes[i - 1, j].IsClosed())
                boxes[i - 1, j].Uncover();
            if (boxes[i - 1, j + 1].IsClosed())
                boxes[i - 1, j + 1].Uncover();
            if (boxes[i, j - 1].IsClosed())
                boxes[i, j - 1].Uncover();
            if (boxes[i, j].IsClosed())
                boxes[i, j].Uncover();
            if (boxes[i, j + 1].IsClosed())
                boxes[i, j + 1].Uncover();
            if (boxes[i + 1, j - 1].IsClosed())
                boxes[i + 1, j - 1].Uncover();
            if (boxes[i + 1, j].IsClosed())
                boxes[i + 1, j].Uncover();
            if (boxes[i + 1, j + 1].IsClosed())
                boxes[i + 1, j + 1].Uncover();
        }
    }

    public void UncoverByFlags(int[] position, int numGandis)
    {
        int i = position[0];
        int j = position[1];
        int flags = 0;
        if (i == 0 && j == 0)
        {
            if (boxes[0, 1].IsFlag())
                flags++;
            if (boxes[1, 0].IsFlag())
                flags++;
            if (boxes[1, 1].IsFlag())
                flags++;
        }

        else if (i == height - 1 && j == width - 1)
        {
            if (boxes[height - 1, width - 2].IsFlag())
                flags++;
            if (boxes[height - 2, width - 1].IsFlag())
                flags++;
            if (boxes[height - 2, width - 2].IsFlag())
                flags++;
        }

        else if (i == height - 1 && j == 0)
        {
            if (boxes[height - 2, 0].IsFlag())
                flags++;
            if (boxes[height - 1, 1].IsFlag())
                flags++;
            if (boxes[height - 2, 1].IsFlag())
                flags++;
        }

        else if (i == 0 && j == width - 1)
        {
            if (boxes[1, width - 1].IsFlag())
                flags++;
            if (boxes[0, width - 2].IsFlag())
                flags++;
            if (boxes[1, width - 2].IsFlag())
                flags++;
        }

        else if (i == 0)
        {

            if (boxes[0, j - 1].IsFlag())
                flags++;
            if (boxes[0, j + 1].IsFlag())
                flags++;
            if (boxes[1, j - 1].IsFlag())
                flags++;
            if (boxes[1, j].IsFlag())
                flags++;
            if (boxes[1, j + 1].IsFlag())
                flags++;
        }

        else if (i == height - 1)
        {

            if (boxes[height - 1, j - 1].IsFlag())
                flags++;
            if (boxes[height - 1, j + 1].IsFlag())
                flags++;
            if (boxes[height - 2, j - 1].IsFlag())
                flags++;
            if (boxes[height - 2, j].IsFlag())
                flags++;
            if (boxes[height - 2, j + 1].IsFlag())
                flags++;
        }

        else if (j == 0)
        {

            if (boxes[i - 1, 0].IsFlag())
                flags++;
            if (boxes[i + 1, 0].IsFlag())
                flags++;
            if (boxes[i - 1, 1].IsFlag())
                flags++;
            if (boxes[i, 1].IsFlag())
                flags++;
            if (boxes[i + 1, 1].IsFlag())
                flags++;
        }

        else if (j == width - 1)
        {

            if (boxes[i - 1, width - 1].IsFlag())
                flags++;
            if (boxes[i + 1, width - 1].IsFlag())
                flags++;
            if (boxes[i - 1, width - 2].IsFlag())
                flags++;
            if (boxes[i, width - 2].IsFlag())
                flags++;
            if (boxes[i + 1, width - 2].IsFlag())
                flags++;
        }

        else
        {
            if (boxes[i - 1, j - 1].IsFlag())
                flags++;
            if (boxes[i - 1, j].IsFlag())
                flags++;
            if (boxes[i - 1, j + 1].IsFlag())
                flags++;
            if (boxes[i, j - 1].IsFlag())
                flags++;
            if (boxes[i, j].IsFlag())
                flags++;
            if (boxes[i, j + 1].IsFlag())
                flags++;
            if (boxes[i + 1, j - 1].IsFlag())
                flags++;
            if (boxes[i + 1, j].IsFlag())
                flags++;
            if (boxes[i + 1, j + 1].IsFlag())
                flags++;
        }
        if (flags == numGandis)
        {
            if (i == 0 && j == 0)
            {
                if (boxes[0, 1].IsFlag() == false)
                    boxes[0, 1].Uncover();
                if (boxes[1, 0].IsFlag() == false)
                    boxes[1, 0].Uncover();
                if (boxes[1, 1].IsFlag() == false)
                    boxes[1, 1].Uncover();
            }

            else if (i == height - 1 && j == width - 1)
            {
                if (boxes[height - 1, width - 2].IsFlag() == false)
                    boxes[height - 1, width - 2].Uncover();
                if (boxes[height - 2, width - 1].IsFlag() == false)
                    boxes[height - 2, width - 1].Uncover();
                if (boxes[height - 2, width - 2].IsFlag() == false)
                    boxes[height - 2, width - 2].Uncover();
            }

            else if (i == height - 1 && j == 0)
            {
                if (boxes[height - 2, 0].IsFlag() == false)
                    boxes[height - 2, 0].Uncover();
                if (boxes[height - 1, 1].IsFlag() == false)
                    boxes[height - 1, 1].Uncover();
                if (boxes[height - 2, 1].IsFlag() == false)
                    boxes[height - 2, 1].Uncover();
            }

            else if (i == 0 && j == width - 1)
            {
                if (boxes[1, width - 1].IsFlag() == false)
                    boxes[1, width - 1].Uncover();
                if (boxes[0, width - 2].IsFlag() == false)
                    boxes[0, width - 2].Uncover();
                if (boxes[1, width - 2].IsFlag() == false)
                    boxes[1, width - 2].Uncover();
            }

            else if (i == 0)
            {

                if (boxes[0, j - 1].IsFlag() == false)
                    boxes[0, j - 1].Uncover();
                if (boxes[0, j + 1].IsFlag() == false)
                    boxes[0, j + 1].Uncover();
                if (boxes[1, j - 1].IsFlag() == false)
                    boxes[1, j - 1].Uncover();
                if (boxes[1, j].IsFlag() == false)
                    boxes[1, j].Uncover();
                if (boxes[1, j + 1].IsFlag() == false)
                    boxes[1, j + 1].Uncover();
            }

            else if (i == height - 1)
            {

                if (boxes[height - 1, j - 1].IsFlag() == false)
                    boxes[height - 1, j - 1].Uncover();
                if (boxes[height - 1, j + 1].IsFlag() == false)
                    boxes[height - 1, j + 1].Uncover();
                if (boxes[height - 2, j - 1].IsFlag() == false)
                    boxes[height - 2, j - 1].Uncover();
                if (boxes[height - 2, j].IsFlag() == false)
                    boxes[height - 2, j].Uncover();
                if (boxes[height - 2, j + 1].IsFlag() == false)
                    boxes[height - 2, j + 1].Uncover();
            }

            else if (j == 0)
            {

                if (boxes[i - 1, 0].IsFlag() == false)
                    boxes[i - 1, 0].Uncover();
                if (boxes[i + 1, 0].IsFlag() == false)
                    boxes[i + 1, 0].Uncover();
                if (boxes[i - 1, 1].IsFlag() == false)
                    boxes[i - 1, 1].Uncover();
                if (boxes[i, 1].IsFlag() == false)
                    boxes[i, 1].Uncover();
                if (boxes[i + 1, 1].IsFlag() == false)
                    boxes[i + 1, 1].Uncover();
            }

            else if (j == width - 1)
            {

                if (boxes[i - 1, width - 1].IsFlag() == false)
                    boxes[i - 1, width - 1].Uncover();
                if (boxes[i + 1, width - 1].IsFlag() == false)
                    boxes[i + 1, width - 1].Uncover();
                if (boxes[i - 1, width - 2].IsFlag() == false)
                    boxes[i - 1, width - 2].Uncover();
                if (boxes[i, width - 2].IsFlag() == false)
                    boxes[i, width - 2].Uncover();
                if (boxes[i + 1, width - 2].IsFlag() == false)
                    boxes[i + 1, width - 2].Uncover();
            }

            else
            {
                if (boxes[i - 1, j - 1].IsFlag() == false)
                    boxes[i - 1, j - 1].Uncover();
                if (boxes[i - 1, j].IsFlag() == false)
                    boxes[i - 1, j].Uncover();
                if (boxes[i - 1, j + 1].IsFlag() == false)
                    boxes[i - 1, j + 1].Uncover();
                if (boxes[i, j - 1].IsFlag() == false)
                    boxes[i, j - 1].Uncover();
                if (boxes[i, j].IsFlag() == false)
                    boxes[i, j].Uncover();
                if (boxes[i, j + 1].IsFlag() == false)
                    boxes[i, j + 1].Uncover();
                if (boxes[i + 1, j - 1].IsFlag() == false)
                    boxes[i + 1, j - 1].Uncover();
                if (boxes[i + 1, j].IsFlag() == false)
                    boxes[i + 1, j].Uncover();
                if (boxes[i + 1, j + 1].IsFlag() == false)
                    boxes[i + 1, j + 1].Uncover();
            }
        }
    }

    //Setting the gandis number around the box to each box.
    void SetNumGandis()
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                int gandis = 0;
                if (i == 0 && j == 0)
                {
                    if (boxes[0, 1].IsGandi())
                        gandis++;
                    if (boxes[1, 0].IsGandi())
                        gandis++;
                    if (boxes[1, 1].IsGandi())
                        gandis++;
                }

                else if (i == height - 1 && j == width - 1)
                {
                    if (boxes[height - 1, width - 2].IsGandi())
                        gandis++;
                    if (boxes[height - 2, width - 1].IsGandi())
                        gandis++;
                    if (boxes[height - 2, width - 2].IsGandi())
                        gandis++;
                }

                else if (i == height - 1 && j == 0)
                {
                    if (boxes[height - 2, 0].IsGandi())
                        gandis++;
                    if (boxes[height - 1, 1].IsGandi())
                        gandis++;
                    if (boxes[height - 2, 1].IsGandi())
                        gandis++;
                }

                else if (i == 0 && j == width - 1)
                {
                    if (boxes[1, width - 1].IsGandi())
                        gandis++;
                    if (boxes[0, width - 2].IsGandi())
                        gandis++;
                    if (boxes[1, width - 2].IsGandi())
                        gandis++;
                }

                else if (i == 0)
                {

                    if (boxes[0, j - 1].IsGandi())
                        gandis++;
                    if (boxes[0, j + 1].IsGandi())
                        gandis++;
                    if (boxes[1, j - 1].IsGandi())
                        gandis++;
                    if (boxes[1, j].IsGandi())
                        gandis++;
                    if (boxes[1, j + 1].IsGandi())
                        gandis++;
                }

                else if (i == height - 1)
                {

                    if (boxes[height - 1, j - 1].IsGandi())
                        gandis++;
                    if (boxes[height - 1, j + 1].IsGandi())
                        gandis++;
                    if (boxes[height - 2, j - 1].IsGandi())
                        gandis++;
                    if (boxes[height - 2, j].IsGandi())
                        gandis++;
                    if (boxes[height - 2, j + 1].IsGandi())
                        gandis++;
                }

                else if (j == 0)
                {

                    if (boxes[i - 1, 0].IsGandi())
                        gandis++;
                    if (boxes[i + 1, 0].IsGandi())
                        gandis++;
                    if (boxes[i - 1, 1].IsGandi())
                        gandis++;
                    if (boxes[i, 1].IsGandi())
                        gandis++;
                    if (boxes[i + 1, 1].IsGandi())
                        gandis++;
                }

                else if (j == width - 1)
                {

                    if (boxes[i - 1, width - 1].IsGandi())
                        gandis++;
                    if (boxes[i + 1, width - 1].IsGandi())
                        gandis++;
                    if (boxes[i - 1, width - 2].IsGandi())
                        gandis++;
                    if (boxes[i, width - 2].IsGandi())
                        gandis++;
                    if (boxes[i + 1, width - 2].IsGandi())
                        gandis++;
                }

                else
                {
                    if (boxes[i - 1, j - 1].IsGandi())
                        gandis++;
                    if (boxes[i - 1, j].IsGandi())
                        gandis++;
                    if (boxes[i - 1, j + 1].IsGandi())
                        gandis++;
                    if (boxes[i, j - 1].IsGandi())
                        gandis++;
                    if (boxes[i, j].IsGandi())
                        gandis++;
                    if (boxes[i, j + 1].IsGandi())
                        gandis++;
                    if (boxes[i + 1, j - 1].IsGandi())
                        gandis++;
                    if (boxes[i + 1, j].IsGandi())
                        gandis++;
                    if (boxes[i + 1, j + 1].IsGandi())
                        gandis++;
                }

                boxes[i, j].SetNumGandis(gandis);
            }
        }
    }



    private static void ReverseArray(object[] arr)
    {
        for (int i = 0; i < arr.Length / 2; i++)
        {
            object tmp = arr[i];
            arr[i] = arr[arr.Length - i - 1];
            arr[arr.Length - i - 1] = tmp;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OVER = false;
            FIRST_MOVE = true;
            SceneManager.LoadScene(0);
            //Restart();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void Restart()
    {
        gandisToPut = gandisToPut_NOT_CHANGED;
        ClearGandis();
        OVER = false;
        FIRST_MOVE = true;
        //score.StartTime();
        
    }

    //Setting the gandis in the boxes.
    void SetGandisInBoxes(int gandisToPut, int[] position)
    {
        ClearGandis();
        SetUntouchableBoxes(position);
        while (gandisToPut > 0)
        {
            int i = Random.Range(0, boxes.GetLength(0));
            int j = Random.Range(0, boxes.GetLength(1));
            if (!boxes[i, j].IsGandi() && !boxes[i, j].GetUntouchable())
            {
                boxes[i, j].SetGandi();
                gandisToPut--;
            }
            
        }
    }

    void SetUntouchableBoxes(int[] position)
    {
        int i = position[0];
        int j = position[1];
        boxes[i, j].SetUntouchable(true);

        if (i == 0 && j == 0)
        {
            boxes[0, 1].SetUntouchable(true);
            boxes[1, 0].SetUntouchable(true);
            boxes[1, 1].SetUntouchable(true);
        }

        else if (i == height - 1 && j == width - 1)
        {
            boxes[height - 1, width - 2].SetUntouchable(true);
            boxes[height - 2, width - 1].SetUntouchable(true);
            boxes[height - 2, width - 2].SetUntouchable(true);
        }

        else if (i == height - 1 && j == 0)
        {
            boxes[height - 2, 0].SetUntouchable(true);
            boxes[height - 1, 1].SetUntouchable(true);
            boxes[height - 2, 1].SetUntouchable(true);
        }

        else if (i == 0 && j == width - 1)
        {
            boxes[1, width - 1].SetUntouchable(true);
            boxes[0, width - 2].SetUntouchable(true);
            boxes[1, width - 2].SetUntouchable(true);
        }

        else if (i == 0)
        {

            boxes[0, j - 1].SetUntouchable(true);
            boxes[0, j + 1].SetUntouchable(true);
            boxes[1, j - 1].SetUntouchable(true);
            boxes[1, j].SetUntouchable(true);
            boxes[1, j + 1].SetUntouchable(true);
        }

        else if (i == height - 1)
        {

            boxes[height - 1, j - 1].SetUntouchable(true);
            boxes[height - 1, j + 1].SetUntouchable(true);
            boxes[height - 2, j - 1].SetUntouchable(true);
            boxes[height - 2, j].SetUntouchable(true);
            boxes[height - 2, j + 1].SetUntouchable(true);
        }

        else if (j == 0)
        {

            boxes[i - 1, 0].SetUntouchable(true);
            boxes[i + 1, 0].SetUntouchable(true);
            boxes[i - 1, 1].SetUntouchable(true);
            boxes[i, 1].SetUntouchable(true);
            boxes[i + 1, 1].SetUntouchable(true);
        }

        else if (j == width - 1)
        {

            boxes[i - 1, width - 1].SetUntouchable(true);
            boxes[i + 1, width - 1].SetUntouchable(true);
            boxes[i - 1, width - 2].SetUntouchable(true);
            boxes[i, width - 2].SetUntouchable(true);
            boxes[i + 1, width - 2].SetUntouchable(true);
        }

        else
        {
            boxes[i - 1, j - 1].SetUntouchable(true);
            boxes[i - 1, j].SetUntouchable(true);
            boxes[i - 1, j + 1].SetUntouchable(true);
            boxes[i, j - 1].SetUntouchable(true);
            boxes[i, j].SetUntouchable(true);
            boxes[i, j + 1].SetUntouchable(true);
            boxes[i + 1, j - 1].SetUntouchable(true);
            boxes[i + 1, j].SetUntouchable(true);
            boxes[i + 1, j + 1].SetUntouchable(true);
        }
    }

    void ClearGandis()
    {
        foreach (Box box in boxes1Darr)
            box.Clear();
    }

}