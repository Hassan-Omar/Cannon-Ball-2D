﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoadingAnimator : MonoBehaviour
{
    [SerializeField]private Text loadingText; 
    [SerializeField]private GameObject resultPanel; 
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("animateLoadingScreen");
    }
    
    IEnumerator animateLoadingScreen()
    {
        yield return new WaitForSeconds(0.1f);
        loadingText.text = " .";
        yield return new WaitForSeconds(0.2f);
        loadingText.text = " ..";
        yield return new WaitForSeconds(0.1f);
        loadingText.text = " ...";
        yield return new WaitForSeconds(0.2f);
        loadingText.text = " ....";
        yield return new WaitForSeconds(0.1f);
        loadingText.text = " ....";
        yield return new WaitForSeconds(0.3f);
        loadingText.text = " .....";
        yield return new WaitForSeconds(0.1f);
        loadingText.text = " ......";
        yield return new WaitForSeconds(0.1f);
        loadingText.text = " .";
        yield return new WaitForSeconds(0.2f);
        loadingText.text = " ..";
        yield return new WaitForSeconds(0.1f);
        loadingText.text = " ...";
        yield return new WaitForSeconds(0.2f);
        loadingText.text = " ....";
        yield return new WaitForSeconds(0.1f);
        loadingText.text = " ....";
        yield return new WaitForSeconds(0.3f);
        loadingText.text = " .....";
        yield return new WaitForSeconds(0.1f);
        loadingText.text = " ......";
        yield return new WaitForSeconds(0.1f);
        loadingText.text = " .......";
        yield return new WaitForSeconds(0.3f);
        loadingText.text = " ........";
        yield return new WaitForSeconds(0.1f);
        loadingText.text = " .........";
        transform.gameObject.SetActive(false);
        resultPanel.SetActive(true);
    }
}