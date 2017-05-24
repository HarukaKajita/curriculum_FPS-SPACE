using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	public ScoreManager scoreManager;
	public GunController gunController;

	public Text scoreText;
	public Text timerText;
	public Text bulletNumText;
	public Text extraBulletNumText;
	public Image snipeImage;

	float timer;

	// Use this for initialization
	void Start () {
		timer = 90;
	}

	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;

		if(timer <= 0){
			timer = 0.0f;
		}

		scoreText.text = "Pt:" + scoreManager.score;
		timerText.text = "Time:" + timer.ToString("0.0") + "s";
		bulletNumText.text = "Bullet:" + gunController.m_CurrentBulletNum;
		extraBulletNumText.text = "BulletBox:" + gunController.m_ExtraBulletNum;

	}

	public void snipeImageEnabled(){
		snipeImage.enabled = true;
	}

	public void snipeImageNotEnabled(){
		snipeImage.enabled = false;
	}
}