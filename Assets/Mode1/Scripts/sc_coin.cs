using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_coin : MonoBehaviour
{
    private Vector3 initialPosition, initialScale;

	// Use this for initialization
	void Awake ()
    {
        initialPosition = this.transform.position;
        initialScale = this.transform.localScale;
	}
	
	public void reset_transform()
    {
        this.gameObject.SetActive(true);
        this.transform.position = initialPosition;
        this.transform.localScale = initialScale;
    }
}
