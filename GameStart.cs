using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;
using hadrack.gpst.core.events;



public class GameStart : MonoBehaviour
{
	public Button playButton;
	public GameObject loading;
	public TextMeshProUGUI loadingText;
	public GameObject mission1;
	public SOEvent<string> loadMissionEvent;

	void Start()
	{
		Button btn = playButton.GetComponent<Button>();
		btn.onClick.AddListener(OnPlayMissionClick);
		StartCoroutine(LoadingAnimation());

		Transform startMissionButton = mission1.transform.Find("PlayButton");
		Button startBtn = startMissionButton.GetComponent<Button>();
		startBtn.onClick.AddListener(OnStartClick);
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



	void OnPlayMissionClick()
	{
		mission1.SetActive(true);
	}

	private void OnStartClick()
	{
		Debug.Log("ONSTART");
		loadMissionEvent?.invoke("Stage0");
	}

}