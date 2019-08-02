using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_gameManager : MonoBehaviour
{
    // Public Objects
    public GameObject player1, player2, downArrow, upArrow, coin1, coin2, coin3, coinX2, generationCoin, summary, fakeBackground;
    public AnimationClip chooseCoin, insertCoin, insertInPiggy, giveMoneyPiggy, summaryAppear;
    public TextMesh coinBankPlayer1, contributionPlayer1, contributionPlayer2, contributionPlayer3, jackpotValue, gainValue;

    // Private Objects
    private int bankAll, bankPlayer1, bankPlayer2, bankPlayer3, valueCoin1, valueCoin2, valueCoin3;

    // Use this for initialization
    void Start()
    {
        bankAll = 0;
        bankPlayer1 = 10;
        bankPlayer2 = 10;
        bankPlayer3 = 10;
    }

    /// <summary>
    /// Gets the bank player1 value.
    /// </summary>
    /// <returns>The value of player1 (red men) bank.</returns>
    public int get_bankPlayer1()
    {
        return bankPlayer1;
    }

    /// <summary>
    /// Re-initialize the values of various objects (Value of coins, value of jackpot, ...).
    /// </summary>
    public void next_Round()
    {
        //Activate the arrows to allow the player to choose the value
        downArrow.SetActive(true);
        upArrow.SetActive(true);

        //Initialize the values of coins/jackpot
        coin1.gameObject.GetComponentInChildren<TextMesh>().text = "" + Mathf.Min(bankPlayer1, 10);
        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        coin1.GetComponent<sc_coin>().reset_transform();
        coin2.GetComponent<sc_coin>().reset_transform();
        coin2.gameObject.GetComponentInChildren<TextMesh>().text = "?";
        coin3.GetComponent<sc_coin>().reset_transform();
        coin3.gameObject.GetComponentInChildren<TextMesh>().text = "?";

        //Deactivates the end summary panel
        summary.SetActive(false);
        fakeBackground.SetActive(false);
    }

    /// <summary>
    /// Actions on the mouse over : less opacity (65%).
    /// </summary>
    public void OnMouseOver()
    {
        foreach (SpriteRenderer spriteImage in this.gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            spriteImage.color = new Color(1f, 1f, 1f, 0.65f);
        }
    }

    /// <summary>
    /// Actions on the mouse exit : more opacity (100%).
    /// </summary>
    public void OnMouseExit()
    {
        foreach (SpriteRenderer spriteImage in this.gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            spriteImage.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    /// <summary>
    /// Actions on the mouse down :
    /// • downgrade the scale
    /// • deactivates the arrows and the box collider
    /// • start the animation of the first coin (men red coin)
    /// </summary>
    public void OnMouseDown()
    {
        this.transform.localScale = new Vector2((transform.localScale.x * 0.75f), (this.transform.localScale.y * 0.75f));

        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        downArrow.SetActive(false);
        upArrow.SetActive(false);

        // start the animation of the first coin
        StartCoroutine(animationCoin(coin1, 1));
    }

    //mode 1: coin1
    //mode 2: coin2
    //mode 3: coin3
    private IEnumerator animationCoin(GameObject objectAnime, int mode)
    {
        objectAnime.GetComponent<Animation>().AddClip(chooseCoin, "chooseCoin");
        objectAnime.GetComponent<Animation>().Play("chooseCoin");
        if (mode == 1)
            yield return new WaitForSeconds(3f);
        else
        {
            yield return new WaitForSeconds(2.5f);
            if (mode == 2)
                objectAnime.GetComponentInChildren<TextMesh>().text = "" + Random.Range(0, Mathf.Min(bankPlayer2, 10));
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

        if (mode == 1)
        {
            valueCoin1 = int.Parse(objectAnime.GetComponentInChildren<TextMesh>().text);
            bankPlayer1 -= valueCoin1;
            coinBankPlayer1.text = "" + (int.Parse(coinBankPlayer1.text) - int.Parse(objectAnime.GetComponentInChildren<TextMesh>().text));
            yield return new WaitForSeconds(1.5f);
            StartCoroutine(animationCoin(coin2, 2));
        }
        else if (mode == 2)
        {
            valueCoin2 = int.Parse(objectAnime.GetComponentInChildren<TextMesh>().text);
            bankPlayer2 -= valueCoin2;
            yield return new WaitForSeconds(1.5f);
            StartCoroutine(animationCoin(coin3, 3));
        }
        else if (mode == 3)
        {
            valueCoin3 = int.Parse(objectAnime.GetComponentInChildren<TextMesh>().text);
            bankPlayer3 -= valueCoin3;
            yield return new WaitForSeconds(2f);
            StartCoroutine(calculMoneyFinalRound());
        }
    }

    private IEnumerator calculMoneyFinalRound()
    {
        coinX2.SetActive(true);
        coinX2.GetComponent<Animation>().AddClip(insertCoin, "insertX2");
        coinX2.GetComponent<Animation>().Play("insertX2");
        this.gameObject.GetComponent<Animation>().Play("insertInPiggy");

        yield return new WaitForSeconds(1.14f);
        coinX2.SetActive(false);
        bankAll *= 2;
        this.gameObject.GetComponentInChildren<TextMesh>().text = "" + bankAll;

        yield return new WaitForSeconds(2f);
        this.gameObject.GetComponent<Animation>().AddClip(giveMoneyPiggy, "giveMoneyPiggy");
        this.gameObject.GetComponent<Animation>().Play("giveMoneyPiggy");

        yield return new WaitForSeconds(0.60f);
        generationCoin.SetActive(true);

        yield return new WaitForSeconds(3.8f);
        generationCoin.SetActive(false);

        bankPlayer1 += bankAll / 3;
        bankPlayer2 += bankAll / 3;
        bankPlayer3 += bankAll / 3;
        jackpotValue.text = "" + bankAll;
        gainValue.text = "" + (bankAll / 3);
        bankAll = 0;
        coinBankPlayer1.text = "" + bankPlayer1;
        this.gameObject.GetComponentInChildren<TextMesh>().text = "" + bankAll;
        contributionPlayer1.text = "" + valueCoin1;
        contributionPlayer2.text = "" + valueCoin2;
        contributionPlayer3.text = "" + valueCoin3;

        StartCoroutine(displayResults());
    }

    private IEnumerator displayResults()
    {
        yield return new WaitForSeconds(2f);

        fakeBackground.SetActive(true);
        summary.SetActive(true);

        summary.GetComponent<Animation>().AddClip(summaryAppear, "summaryAppear");
        summary.GetComponent<Animation>().Play("summaryAppear");
        StartCoroutine(summary.GetComponent<sc_summaryManager>().display_summary());
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
}