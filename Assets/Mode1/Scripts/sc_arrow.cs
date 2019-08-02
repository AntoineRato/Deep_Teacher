using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_arrow : MonoBehaviour
{
    public TextMesh coinRedMen;
    public sc_gameManager gameManager;

    private void OnMouseDown()
    {
        this.transform.localScale = new Vector2((transform.localScale.x * 0.75f), (this.transform.localScale.y * 0.75f));

        if (this.name == "up_arrow")
        {
            up_arrow();
        }
        else if (this.name == "down_arrow")
        {
            down_arrow();
        }
    }

    public void OnMouseUp()
    {
        this.transform.localScale = new Vector2((this.transform.localScale.x / 0.75f), (this.transform.localScale.y / 0.75f));
    }

    public void OnMouseOver()
    {
        foreach (SpriteRenderer spriteImage in this.gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            spriteImage.color = new Color(1f, 1f, 1f, 0.65f);
        }
    }

    public void OnMouseExit()
    {
        foreach (SpriteRenderer spriteImage in this.gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            spriteImage.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    public void up_arrow()
    {
        if (int.Parse(coinRedMen.text) < Mathf.Min(gameManager.get_bankPlayer1(), 10))
            coinRedMen.text = "" + (int.Parse(coinRedMen.text) + 1);
    }

    public void down_arrow()
    {
        if (int.Parse(coinRedMen.text) > 0)
            coinRedMen.text = "" + (int.Parse(coinRedMen.text) - 1);
    }
}
