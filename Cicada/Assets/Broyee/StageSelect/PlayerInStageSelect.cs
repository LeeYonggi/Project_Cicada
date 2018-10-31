using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInStageSelect : MonoBehaviour {

    public float jumpingTime;
    public float fallTime;

    private float prev_YPos;

    private void Start()
    {
        prev_YPos = transform.position.y;
    }

    public IEnumerator Jump(float stageYPos)
    {
        Debug.Log("Jump");
        GetComponent<Animator>().SetBool("Jump", true);
        while (transform.position.y < stageYPos + 90 + 30)
        {
            transform.Translate(0, 15, 0);
            yield return new WaitForSeconds(0.001f);
        }

        Debug.Log("Fall");
        GetComponent<Animator>().SetBool("Jump", false);
        while (transform.position.y > stageYPos + 90)
        {
            transform.Translate(0, -15, 0);
            yield return new WaitForSeconds(0.001f);
        }

        GetComponent<Animator>().SetBool("Grounded", true);

        yield return new WaitForSeconds(0.2f);

        GetComponent<Animator>().SetBool("Grounded", false);
    }

    public IEnumerator Fall(float stageYPos)
    {
        Debug.Log("Fall");
        GetComponent<Animator>().SetBool("Fall", true);
        while (transform.position.y > stageYPos + 90)
        {
            transform.Translate(0, -15, 0);
            yield return new WaitForSeconds(0.001f);
        }

        GetComponent<Animator>().SetBool("Grounded", true);

        yield return new WaitForSeconds(0.2f);

        GetComponent<Animator>().SetBool("Grounded", false);

    }
	
}
