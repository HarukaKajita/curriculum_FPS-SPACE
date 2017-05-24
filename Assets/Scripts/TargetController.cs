using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour {

	int m_life;
	int m_MaxLife = 5;
	public Animator m_anim;
	public Transform m_HeadMarker;

	// Use this for initialization
	void Start () {
		m_life = m_MaxLife;
		m_anim = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		if(m_life <= 0){
			m_anim.SetBool ("broken", true);
			Invoke ("StandUp",10f);
			m_life = m_MaxLife;
		}

	}

	void StandUp(){
		m_anim.SetBool ("broken", false);
	}

	public void GetDamaged(){
		m_life--;
	}
}