using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_gameManager : MonoBehaviour
{
    public GameObject player1, player2;
    public TextMesh textCoin;

	// Use this for initialization
	void Start ()
    {
        
	}

    public void OnMouseDown()
    {
        if(this.name == "up_arrow")
        {
            up_arrow();
        }
        else if(this.name == "down_arrow")
        {
            down_arrow();
        }
    }

    public void up_arrow()
    {
        if(int.Parse(textCoin.text) < 10)
            textCoin.text = "" + (int.Parse(textCoin.text) + 1);
    }

    public void down_arrow()
    {
        if (int.Parse(textCoin.text) > 0)
            textCoin.text = "" + (int.Parse(textCoin.text) - 1);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
