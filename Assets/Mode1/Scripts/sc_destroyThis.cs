using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_destroyThis : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(destroyAfterXSeconds(2f));
	}
	
    private IEnumerator destroyAfterXSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(this.gameObject);
    }
}
