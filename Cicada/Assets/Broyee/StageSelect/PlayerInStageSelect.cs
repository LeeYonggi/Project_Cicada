using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInStageSelect : MonoBehaviour {

    [HideInInspector] public bool moving;

    public float jumpingTime;
    public float fallTime;

    private Transform destinationTrans;

    private UnityEngine.UI.Image image;
    private Animator animator;

    private AudioSource[] audioSources;

    private void Start()
    {
        image = GetComponent<UnityEngine.UI.Image>();
        animator = GetComponent<Animator>();

        audioSources = GetComponents<AudioSource>();

        MovePlayerToCurrentStage();
    }

    private void Update()
    {
        image.sprite = GetComponent<SpriteRenderer>().sprite;
    }

    public void Move()
    {
        if (transform.position.y < destinationTrans.position.y)
            StartCoroutine(Jump());
        else
            StartCoroutine(Fall());
    }

    public void Move(Transform stageTrans)
    {
        destinationTrans = stageTrans;

        if (transform.position.y < destinationTrans.position.y)
            StartCoroutine(Jump());
        else
            StartCoroutine(Fall());
    }

    public void Move(float stageYPos)
    {
        if (transform.position.y < stageYPos)
            StartCoroutine(Jump());
        else
            StartCoroutine(Fall());
    }

    public IEnumerator Jump()
    {
        if (moving) yield break;
        moving = true;
        Debug.Log("StageYPos : " + destinationTrans.position.y.ToString());

        Debug.Log("Jump");
        GetComponent<Animator>().SetBool("Jump", true);
        audioSources[0].Play();
        while (transform.position.y < destinationTrans.position.y + 200 + 30)
        {
            Debug.Log("Jumping");
            transform.Translate(0, 15, 0);
            yield return new WaitForSeconds(0.001f);
        }

        Debug.Log("Fall");
        GetComponent<Animator>().SetBool("Jump", false);
        GetComponent<Animator>().SetBool("Fall", true);
        audioSources[1].Play();
        while (transform.position.y > destinationTrans.position.y + 200)
        {
            transform.Translate(0, -15, 0);
            yield return new WaitForSeconds(0.001f);
        }
        transform.parent.GetChild(4).transform.position = destinationTrans.position;

        GetComponent<Animator>().SetBool("Fall", false);
        GetComponent<Animator>().SetBool("Grounded", true);
        audioSources[2].Play();

        yield return new WaitForSeconds(0.2f);

        GetComponent<Animator>().SetBool("Grounded", false);

        moving = false;
    }

    public IEnumerator Fall()
    {
        if (moving) yield break;
        moving = true;
        Debug.Log("StageYPos : " + destinationTrans.position.y.ToString());

        Debug.Log("Fall");
        GetComponent<Animator>().SetBool("Fall", true);
        audioSources[1].Play();
        while (transform.position.y > destinationTrans.position.y + 200)
        {
            transform.Translate(0, -15, 0);
            yield return new WaitForSeconds(0.001f);
        }
        transform.parent.GetChild(4).transform.position = destinationTrans.position;

        GetComponent<Animator>().SetBool("Fall", false);
        GetComponent<Animator>().SetBool("Grounded", true);
        audioSources[2].Play();

        yield return new WaitForSeconds(0.2f);

        GetComponent<Animator>().SetBool("Grounded", false);
        moving = false;

    }

    //public void Move(float stageYPos)
    //{
    //    if (transform.position.y < stageYPos)
    //        StartCoroutine(Jump(stageYPos));
    //    else
    //        StartCoroutine(Fall(stageYPos));
    //}

    //public IEnumerator Jump(float stageYPos)
    //{
    //    if (moving) yield break;
    //    moving = true;
    //    Debug.Log("StageYPos : " + stageYPos.ToString());

    //    Debug.Log("Jump");
    //    GetComponent<Animator>().SetBool("Jump", true);
    //    audioSources[0].Play();
    //    while (transform.position.y < stageYPos + 200 + 30)
    //    {
    //        Debug.Log("Jumping");
    //        transform.Translate(0, 15, 0);
    //        yield return new WaitForSeconds(0.001f);
    //    }

    //    Debug.Log("Fall");
    //    GetComponent<Animator>().SetBool("Jump", false);
    //    GetComponent<Animator>().SetBool("Fall", true);
    //    audioSources[1].Play();
    //    while (transform.position.y > stageYPos + 200)
    //    {
    //        transform.Translate(0, -15, 0);
    //        yield return new WaitForSeconds(0.001f);
    //    }
    //    transform.parent.GetChild(4).transform.position = new Vector3(1024, stageYPos, 0);

    //    GetComponent<Animator>().SetBool("Fall", false);
    //    GetComponent<Animator>().SetBool("Grounded", true);
    //    audioSources[2].Play();

    //    yield return new WaitForSeconds(0.2f);

    //    GetComponent<Animator>().SetBool("Grounded", false);

    //    moving = false;
    //}

    //public IEnumerator Fall(float stageYPos)
    //{
    //    if (moving) yield break;
    //    moving = true;
    //    Debug.Log("StageYPos : " + stageYPos.ToString());

    //    Debug.Log("Fall");
    //    GetComponent<Animator>().SetBool("Fall", true);
    //    audioSources[1].Play();
    //    while (transform.position.y > stageYPos + 200)
    //    {
    //        transform.Translate(0, -15, 0);
    //        yield return new WaitForSeconds(0.001f);
    //    }
    //    transform.parent.GetChild(4).transform.position = new Vector3(1024, stageYPos, 0);

    //    GetComponent<Animator>().SetBool("Fall", false);
    //    GetComponent<Animator>().SetBool("Grounded", true);
    //    audioSources[2].Play();

    //    yield return new WaitForSeconds(0.2f);

    //    GetComponent<Animator>().SetBool("Grounded", false);
    //    moving = false;

    //}

    public void MovePlayerToCurrentStage()
    {
        StageManager stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();

        int map = stageManager.GetMap();
        int stage = stageManager.GetStage();

        int childNum = (map - 1) * 5 + stage - 1;

        destinationTrans = transform.parent.GetChild(childNum).transform;

        transform.position = new Vector3(destinationTrans.position.x, destinationTrans.position.y + 200, 0);
        Move();
    }
	
}
