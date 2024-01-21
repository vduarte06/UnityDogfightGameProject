using UnityEngine;
using TMPro;
using System.Collections;

public class LoadingText : MonoBehaviour
{
    public TextMeshProUGUI loadingText;

    void Start()
    {
        StartCoroutine(LoadingAnimation());
    }

    IEnumerator LoadingAnimation()
    {
        while (true)
        {
            loadingText.text = " Loading.";
            yield return new WaitForSeconds(0.5f);

            loadingText.text = " Loading..";
            yield return new WaitForSeconds(0.5f);

            loadingText.text = " Loading...";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
