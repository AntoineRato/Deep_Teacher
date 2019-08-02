using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sc_summaryManager : MonoBehaviour
{
    public GameObject menRed, menBlue, girl, mise, jackpot, distrib, quit_button, next_button, summary, piggy;

    public void OnMouseDown()
    {
        this.transform.localScale = new Vector3((transform.localScale.x * 0.75f), (this.transform.localScale.y * 0.75f), (transform.localScale.z * 0.75f));
        if(this.name == "quit_button")
        {
            quitGame();
        }
        else if(this.name == "next_button")
        {
            nextRound();
        }
    }

    public void OnMouseUp()
    {
        this.transform.localScale = new Vector3((this.transform.localScale.x / 0.75f), (this.transform.localScale.y / 0.75f), (transform.localScale.z / 0.75f));
    }

    private void quitGame()
    {
        SceneManager.LoadScene(0);
    }

    private void nextRound()
    {
        menRed.SetActive(false);
        menBlue.SetActive(false);
        girl.SetActive(false);
        mise.SetActive(false);
        jackpot.SetActive(false);
        distrib.SetActive(false);
        quit_button.SetActive(false);
        piggy.GetComponent<sc_gameManager>().next_Round();
        OnMouseUp();
        this.gameObject.SetActive(false);
    }

    public IEnumerator display_summary()
    {
        yield return new WaitForSeconds(2f);
        menRed.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        menBlue.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        girl.SetActive(true);
        yield return new WaitForSeconds(1f);
        mise.SetActive(true);
        yield return new WaitForSeconds(1f);
        jackpot.SetActive(true);
        yield return new WaitForSeconds(1f);
        distrib.SetActive(true);
        yield return new WaitForSeconds(1f);
        quit_button.SetActive(true);
        next_button.SetActive(true);
    }
}