using UnityEngine;
using System.Collections;

public class SpellOrb : MonoBehaviour {

	enum ELEMENTS {ether=0,fire=1,water=2,wind=3,earth=4,spirit=5};

	public Texture crosshair;
	public GameObject fireballPrefab;
	Transform position;
	ELEMENTS[] myElements;
	float[] cooldowns;
	public int maxElements = 3;
	public float elementCooldown = 1;

	void Start () {
		myElements = new ELEMENTS[maxElements];
		cooldowns = new float[6];
	}
	

	void Update () {

		UpdateCooldowns ();
		HandleInput ();
	}

	void UpdateCooldowns ()
	{
		for(int i=1;i<6;i++){
			if(cooldowns[i]>0){
				cooldowns[i]-=Time.deltaTime;
				if(cooldowns[i]<0)
					cooldowns[i]=0;
			}
		}
	}

	void HandleInput ()
	{
		
		if (Input.GetKeyUp("1")) {
			selectRune(ELEMENTS.fire);
		}
		if (Input.GetKeyUp("2")) {
			selectRune(ELEMENTS.water);
		}
		if (Input.GetKeyUp("3")) {
			selectRune(ELEMENTS.wind);
		}
		if (Input.GetKeyUp("4")) {
			selectRune(ELEMENTS.earth);
		}
		if (Input.GetKeyUp("5")) {
			selectRune(ELEMENTS.spirit);
		}
		if(Input.GetMouseButtonUp(0)){
			Cast();
		}
	}

	void selectRune(ELEMENTS element){
		for (int i=0; i<maxElements; i++){
			if(myElements[i]==ELEMENTS.ether){
				if(cooldowns[(int)element]<=0){
					myElements[i]=element;
					cooldowns[(int)element]+=elementCooldown;
				}
				break;
			}
		}
		
	}

	void Cast(){
		int spellCode = 0;
		for (int i=0; i<maxElements; i++) {
			spellCode += (int)myElements[i]*(i+1)*10;
		}
		switch(spellCode){
		case 10:
			Debug.Log ("cast fireball");
			Instantiate(fireballPrefab,this.transform.position,GetDirection());
			break;
		case 30:
			Debug.Log ("cast windblow");
			Windblow.Cast(transform.position,transform.forward);
			break;
		default:
			Debug.Log ("No spell");
			break;
		}
		ClearElements();
	}

	Quaternion GetDirection(){
//		Transform cam = Camera.main.transform;
//		RaycastHit hit = new RaycastHit();
//		if (Physics.Raycast (cam.position, cam.forward,out hit, 500)) {
//			return Quaternion.LookRotation((hit.point-transform.position));
//
//		}
//		return Quaternion.identity;
		return Quaternion.LookRotation (this.transform.forward);
	}

	void ClearElements(){
		for(int i=0;i<maxElements;i++){
			myElements[i]=ELEMENTS.ether;
		}
	}

	void OnGUI(){
		string text="";
		for(int i=0;i<6;i++){
			text+=","+cooldowns[i];
		}
		GUI.Label(new Rect(0, 0, Screen.width, Screen.height),text );

		GUI.DrawTexture(new Rect(Screen.width/2-25, Screen.height/2-25, 50, 50), crosshair, ScaleMode.ScaleToFit, true, 1.0F);
	}
}
