using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

	RaycastHit hit;
	public GameObject m_HitObjectSparkle;
	public GameObject m_MuzzleSparkle;
	AudioSource m_GunAudioSource;
	public AudioClip m_fire;
	public AudioClip m_reload;
	bool m_canShot = true;
	bool m_onReloading = false;
	float m_coolTime;
	public int m_CurrentBulletNum;
	int m_BulletLimit = 30; 
	public int m_ExtraBulletNum = 150;
	public ScoreManager scoreManager;
	public UIController uIController; //
	bool onSniping = false; //

	// Use this for initialization
	void Start () {
		m_GunAudioSource = GetComponent<AudioSource>();
		m_coolTime = 0.5f;
		m_CurrentBulletNum = m_BulletLimit;
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0) && 
			m_canShot == true && 
			m_CurrentBulletNum >0 
		){
			Shot ();
		}

		if (Input.GetKeyDown ("r") && m_onReloading == false) {
			m_GunAudioSource.PlayOneShot (m_reload);
			Reload ();
		}

		if(Input.GetMouseButtonDown(1)){//
			if(onSniping == false){ //
				Camera.main.fieldOfView = 25; //
				uIController.snipeImageEnabled(); //
				onSniping = true; //
			} else { //
				Camera.main.fieldOfView = 60; //
				uIController.snipeImageNotEnabled (); //
				onSniping = false; //
			} //
		}//
	}

	void Shot(){
		Ray ray = Camera.main.ViewportPointToRay (new Vector3(0.5f, 0.5f, 0));
		m_GunAudioSource.PlayOneShot (m_fire);
		m_canShot = false;
		Invoke("EnableToShot", m_coolTime);
		m_CurrentBulletNum--;
		GameObject MuzzleSparkle = Instantiate (
			m_MuzzleSparkle,
			transform    
		);
		Destroy(MuzzleSparkle, 0.1f);

		if (Physics.Raycast (ray, out hit)) {
			if (hit.collider) {         
				GameObject HitObjectSparkle = Instantiate (
					m_HitObjectSparkle,
					hit.point - ray.direction * 0.25f,
					Camera.main.transform.rotation
				);
				Destroy(HitObjectSparkle, 0.5f);

				if (hit.collider.transform.parent && hit.collider.transform.parent.tag == "Target") {
					TargetController target = hit.collider.transform.parent.GetComponent<TargetController> ();
					target.GetDamaged ();

					Vector3 centerPosition = target.m_HeadMarker.position;
					float distance = Vector3.Distance(centerPosition, hit.point);
					scoreManager.GetScore(distance);
				}
			}
		}
	}

	void EnableToShot(){
		m_canShot = true;
	}

	void Reload (){
		m_onReloading = true;
		m_canShot = false;
		Invoke("EnableToShot", 2.1f);
		Invoke("EndOfReload", 2.1f);
		while(m_CurrentBulletNum < m_BulletLimit && m_ExtraBulletNum > 0){
			m_CurrentBulletNum++;
			m_ExtraBulletNum--;
		}
	}

	void EndOfReload (){
		m_onReloading =false;
	}
}