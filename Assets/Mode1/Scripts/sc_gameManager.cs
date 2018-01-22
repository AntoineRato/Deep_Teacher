using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_gameManager : MonoBehaviour
{
    public GameObject player1, player2, downArrow, upArrow, coin1, coin2, coin3, coinX2, generationCoin;
    public AnimationClip chooseCoin, insertCoin, insertInPiggy, giveMoneyPiggy;
    public TextMesh coinBankPlayer1;

    private TextMesh textCoin;
    //private bool flagQuit;
    private int bankAll, bankPlayer1, bankPlayer2, bankPlayer3;

	// Use this for initialization
	void Start ()
    {
        bankAll = 0;
        bankPlayer1 = 10;
        bankPlayer2 = 10;
        bankPlayer3 = 10;
        //flagQuit = false;

        if (this.name == "up_arrow" || this.name == "down_arrow")
        {
            textCoin = coin1.gameObject.GetComponentInChildren<TextMesh>();
            textCoin.text = "" + Mathf.Min(bankPlayer1, 10);
        }
	}

    public void OnMouseOver()
    {
        foreach(SpriteRenderer spriteImage in this.gameObject.GetComponentsInChildren<SpriteRenderer>())
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

    public void OnMouseDown()
    {
        this.transform.localScale = new Vector2((transform.localScale.x * 0.75f), (this.transform.localScale.y * 0.75f));

        if(this.name == "up_arrow")
        {
            up_arrow();
        }
        else if(this.name == "down_arrow")
        {
            down_arrow();
        }
        else if(this.name == "Piggy")
        {
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            downArrow.SetActive(false);
            upArrow.SetActive(false);
            StartCoroutine(animationCoin(coin1, 1));
        }
    }

    //mode 1: coin1
    //mode 2: coin2
    //mode 3: coin3
    private IEnumerator animationCoin(GameObject objectAnime, int mode)
    {
        objectAnime.GetComponent<Animation>().AddClip(chooseCoin, "chooseCoin");
        objectAnime.GetComponent<Animation>().Play("chooseCoin");
        if(mode == 1)
            yield return new WaitForSeconds(3f);
        else
        {
            yield return new WaitForSeconds(2.5f);
            if(mode == 2)
                objectAnime.GetComponentInChildren<TextMesh>().text = "" + Random.Range(0,Mathf.Min(bankPlayer2,10));
            else
                objectAnime.GetComponentInChildren<TextMesh>().text = "" + Random.Range(0, Mathf.Min(bankPlayer3, 10));
            yield return new WaitForSeconds(0.5f);
        }

        StartCoroutine(MoveObjectToB(objectAnime, new Vector3(-0.2f, 2f, 0f), 10f));
        yield return new WaitForSeconds(1.65f);

        objectAnime.GetComponent<Animation>().AddClip(insertCoin, "insertCoin");
        objectAnime.GetComponent<Animation>().Play("insertCoin");
        this.gameObject.GetComponent<Animation>().AddClip(insertInPiggy, "insertInPiggy");
        this.gameObject.GetComponent<Animation>().Play("insertInPiggy");
        yield return new WaitForSeconds(1.14f);
        objectAnime.SetActive(false);

        bankAll += int.Parse(objectAnime.GetComponentInChildren<TextMesh>().text);
        this.gameObject.GetComponentInChildren<TextMesh>().text = "" + bankAll;

        if(mode == 1)
        {
            bankPlayer1 -= int.Parse(objectAnime.GetComponentInChildren<TextMesh>().text);
            coinBankPlayer1.text = "" + (int.Parse(coinBankPlayer1.text) - int.Parse(objectAnime.GetComponentInChildren<TextMesh>().text));
            yield return new WaitForSeconds(1.5f);
            StartCoroutine(animationCoin(coin2, 2));
        }
        else if(mode == 2)
        {
            bankPlayer2 -= int.Parse(objectAnime.GetComponentInChildren<TextMesh>().text);
            yield return new WaitForSeconds(1.5f);
            StartCoroutine(animationCoin(coin3, 3));
        }
        else if(mode == 3)
        {
            bankPlayer3 -= int.Parse(objectAnime.GetComponentInChildren<TextMesh>().text);
            yield return new WaitForSeconds(2f);
            StartCoroutine(calculMoneyFinalRound());
        }
    }

    private IEnumerator calculMoneyFinalRound()
    {
        coinX2.SetActive(true);
        coinX2.GetComponent<Animation>().AddClip(insertCoin, "insertX2");
        coinX2.GetComponent<Animation>().Play("insertX2");

        yield return new WaitForSeconds(1.14f);
        coinX2.SetActive(false);
        bankAll *= 2;
        this.gameObject.GetComponentInChildren<TextMesh>().text = "" + bankAll;

        yield return new WaitForSeconds(2f);
        this.gameObject.GetComponent<Animation>().AddClip(giveMoneyPiggy, "giveMoneyPiggy");
        this.gameObject.GetComponent<Animation>().Play("giveMoneyPiggy");

        yield return new WaitForSeconds(0.60f);
        generationCoin.SetActive(true);

        yield return new WaitForSeconds(5.3f);
        generationCoin.SetActive(false);

        bankPlayer1 += bankAll / 3;
        bankPlayer2 += bankAll / 3;
        bankPlayer3 += bankAll / 3;
        bankAll = 0;
        coinBankPlayer1.text = "" + bankPlayer1;
        this.gameObject.GetComponentInChildren<TextMesh>().text = "" + bankAll;

    }

    private IEnumerator MoveObjectToB(GameObject objectMove, Vector3 pointB, float time)
    {
         float i = 0.0f;
         float rate = 1.0f / time;
         while (i < 1.0)
         {
            i += Time.deltaTime * rate;
            objectMove.transform.position = Vector3.Lerp(objectMove.transform.position, pointB, i);
            yield return null;
         }
     }

    public void OnMouseUp()
    {
        this.transform.localScale = new Vector2((this.transform.localScale.x / 0.75f), (this.transform.localScale.y / 0.75f));
    }

    public void up_arrow()
    {
        if(int.Parse(textCoin.text) < Mathf.Min(bankPlayer1, 10))
            textCoin.text = "" + (int.Parse(textCoin.text) + 1);
    }

    public void down_arrow()
    {
        if (int.Parse(textCoin.text) > 0)
            textCoin.text = "" + (int.Parse(textCoin.text) - 1);
    }
}
