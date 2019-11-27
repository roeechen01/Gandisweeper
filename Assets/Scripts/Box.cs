using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Box : MonoBehaviour {

    public BoxManager boxManager;
    private int[] position = new int[2];
    private SpriteRenderer sr;
    public Sprite[] sprites = new Sprite[7];
    public Sprite[] numSprites = new Sprite[8];
    private int numGandis = 0;
    private bool gandi = false;
    bool flag = false, hidden = true, question = false;
    bool untouchable = false;
    private Time score;

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        score = FindObjectOfType<Time>();
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    void RightClick()
    {
        if (boxManager.questionMode)
        {
            if (!flag && !question)
            {
                sr.sprite = sprites[3];
                ChangeFlagStatus();
            }

            else if (flag)
            {
                sr.sprite = sprites[4];
                question = true;
                ChangeFlagStatus();
            }
            else
            {
                flag = false;
                question = false;
                sr.sprite = sprites[0];
            }
        }
        else
        {
            if (!flag)
            {
                sr.sprite = sprites[3];
                ChangeFlagStatus();
            }

            else
            {

                question = false;
                sr.sprite = sprites[0];
                ChangeFlagStatus();
            }

        }
    }

    void LeftClick()
    {
        print("clicked on box: " + position[0] + ", " + position[1]);
        if (sr.sprite == sprites[0])
            Uncover();
        else if (!hidden)
        {
            boxManager.UncoverByFlags(position, numGandis);
        }
    }

    void OnMouseOver()
    {
        if (!BoxManager.OVER)
        {
            if (Input.GetMouseButtonDown(1) && hidden)
                RightClick();
            if (Input.GetMouseButtonDown(0))
                LeftClick();   
        }
        
    }

    public void Uncover()
    {
        hidden = false;
        if (BoxManager.FIRST_MOVE)
        {
            boxManager.SetGandis(position);
            score.StartTime();
            
        }
            
            
        if (numGandis != 0 || gandi)
        {
            if (BoxManager.FIRST_MOVE)
            {
                BoxManager.FIRST_MOVE = false;
                boxManager.SetGandis(position);
                if (numGandis == 0)
                {
                    Show();
                    boxManager.UncoverEmptyBoxes(position);
                }
                else Show();
            }
        }
        if (gandi)
        {
            Show();
            boxManager.Lose();
            sr.sprite = sprites[5];
        }
            
        else
        {
            BoxManager.FIRST_MOVE = false;
            if (numGandis == 0)
            {
                Show();
                boxManager.UncoverEmptyBoxes(position);
            }
            else Show();
        }
        if(!BoxManager.OVER)
            boxManager.WinCheck();
    }

    

    public int GetNumGandis()
    {
        return this.numGandis;
    }

    public void SetGandi()
    {
        this.gandi = true;
    }

    public bool IsGandi()
    {
        return this.gandi;
    }

    public bool IsHidden()
    {
        return this.hidden;
    }

    public void SetNumGandis(int num)
    {
        numGandis = num;
    }

    public Sprite GetSprite()
    {
        return this.sr.sprite;
    }

    public bool IsClosed()
    {
        return sr.sprite == sprites[0];
    }

    public Sprite[] GetSprites()
    {
        return this.sprites;
    }

    public bool IsFlag()
    {
        return this.flag && this.hidden;
    }

    public void Clear()
    {
        gandi = false;
        numGandis = 0;
        flag = false;
        question = false;
        hidden = true;
        untouchable = false;
        sr.sprite = sprites[0];
    }


    public void SetBox(Box first, int[] position)
    {
        this.sprites = first.sprites;
        this.numSprites = first.numSprites;
        this.boxManager = first.boxManager;
        this.position = position;
    }

    public void SetPosition(float x, float y)
    {
        this.transform.position = new Vector2(x, y);
    }

    public  Vector2 GetPosotion()
    {
        return this.transform.position;
    }

    public void Show()
    {
        hidden = false;
        if (gandi)
            sr.sprite = sprites[1];
        else if (numGandis == 0)
            sr.sprite = sprites[2];
        else sr.sprite = numSprites[numGandis - 1];
    }

    public void WinFlag()
    {
        if (gandi && sr.sprite != sprites[3])
        {
            sr.sprite = sprites[3];
            boxManager.FlagsLeftChange(false);
        }
            
    }

    public void LoseGandi()
    {
        if (gandi && !flag)
            sr.sprite = sprites[1];
        else if (!gandi && flag)
            sr.sprite = sprites[6];
    }

    public void SetUntouchable(bool untouchable)
    {
        this.untouchable = untouchable;
    }

    public bool GetUntouchable()
    {
        return this.untouchable;
    }

    public void ChangeFlagStatus()
    {
        flag = !flag;
        boxManager.FlagsLeftChange(!flag);
    }
}
