using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameText : MonoBehaviour {

    private Text txt;
    public Image img;
    public Sprite[] sprites = new Sprite[3];
    public List<string> gandiQuotes = new List<string>();
    int curr = 0;

	// Use this for initialization
	void Start () {
        txt = GetComponent<Text>();
        InvokeRepeating("GenerateQuote", 0f, 10f);
        img.sprite = sprites[0];
    }

    void GenerateQuote()
    {
        if (!BoxManager.OVER)
        {
            int rnd = Random.Range(0, gandiQuotes.Count);
            while (rnd == curr)
                rnd = Random.Range(0, gandiQuotes.Count);
            curr = rnd;
            SetQuoteMode();
            txt.text = gandiQuotes[curr];
        }

    }

    public void SetQuoteMode()
    {
        txt.color = new Color(0.5f, 0.5f, 0.5f);
        txt.fontSize = 18;
    }

    public void SetOverMode(bool win)
    {
        txt.color = new Color(1f, 1f, 1f);
        txt.fontSize = 30;
        if (win)
            Win();
        else Lose();
    }

    public void Win()
    {
        txt.text = "You won!";
        img.sprite = sprites[2];
    }

    public void Lose()
    {
        txt.text = "You lost!";
        img.sprite = sprites[1];
    }

    // Update is called once per frame
    void Update () {
		
	}
}
