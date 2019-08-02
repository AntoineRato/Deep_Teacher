using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_gameSpeed : MonoBehaviour
{
    public TextMesh value_gameSpeed;

    public void OnMouseDown()
    {
        this.transform.localScale = new Vector3((transform.localScale.x * 0.75f), (this.transform.localScale.y * 0.75f), (transform.localScale.z * 0.75f));

        if (Time.timeScale == 1)
            Time.timeScale = 2f;
        else if (Time.timeScale == 2f)
            Time.timeScale = 3;
        else
            Time.timeScale = 1;

        value_gameSpeed.text = "" + Time.timeScale;
    }

    public void OnMouseUp()
    {
        this.transform.localScale = new Vector3((this.transform.localScale.x / 0.75f), (this.transform.localScale.y / 0.75f), (transform.localScale.z / 0.75f));
    }
}
