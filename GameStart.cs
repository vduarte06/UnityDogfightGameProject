using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class GameStart : MonoBehaviour {
	public Button playButton;
    public GameObject loading;

	void Start () {
		Button btn = playButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick(){
        SceneManager.LoadScene(1); 
        loading.SetActive(true);
	}
}