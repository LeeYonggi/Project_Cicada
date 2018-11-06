using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInStageSelect : MonoBehaviour {

    public bool moving;

    public float jumpingTime;
    public float fallTime;

    private UnityEngine.UI.Image image;
    private Animator animator;

    private void Start()
    {
        image = GetComponent<UnityEngine.UI.Image>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        image.sprite = GetComponent<SpriteRenderer>().sprite;
    }

    public IEnumerator Jump(float stageYPos)
    {
        if (moving) yield break;
        moving = true;
        Debug.Log("StageYPos : " + stageYPos.ToString());

        Debug.Log("Jump");
        GetComponent<Animator>().SetBool("Jump", true);
        while (transform.position.y < stageYPos + 90 + 30)
        {
            Debug.Log("Jumping");
            transform.Translate(0, 15, 0);
            yield return new WaitForSeconds(0.001f);
        }

        Debug.Log("Fall");
        GetComponent<Animator>().SetBool("Jump", false);
        GetComponent<Animator>().SetBool("Fall", true);
        while (transform.position.y > stageYPos + 100)
        {
            transform.Translate(0, -15, 0);
            yield return new WaitForSeconds(0.001f);
        }

        GetComponent<Animator>().SetBool("Fall", false);
        GetComponent<Animator>().SetBool("Grounded", true);

        yield return new WaitForSeconds(0.2f);

        GetComponent<Animator>().SetBool("Grounded", false);

        moving = false;
    }

    public IEnumerator Fall(float stageYPos)
    {
        if (moving) yield break;
        moving = true;
        Debug.Log("StageYPos : " + stageYPos.ToString());

        Debug.Log("Fall");
        GetComponent<Animator>().SetBool("Fall", true);
        while (transform.position.y > stageYPos + 100)
        {
            transform.Translate(0, -15, 0);
            yield return new WaitForSeconds(0.001f);
        }

        GetComponent<Animator>().SetBool("Fall", false);
        GetComponent<Animator>().SetBool("Grounded", true);

        yield return new WaitForSeconds(0.2f);

        GetComponent<Animator>().SetBool("Grounded", false);
        moving = false;

    }
	
}
