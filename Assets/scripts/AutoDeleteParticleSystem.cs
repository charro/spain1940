using UnityEngine;
using System.Collections;

public class AutoDeleteParticleSystem : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		bool anySystemActive = false;
		var systems = GetComponentsInChildren<ParticleSystem>();
		foreach (ParticleSystem system in systems)
		{
			if(system.IsAlive()){
				anySystemActive = true;
			}
		}

		if(!anySystemActive){
			Destroy(this.gameObject);
		}
	}
}
