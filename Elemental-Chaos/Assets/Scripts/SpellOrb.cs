using UnityEngine;
using System.Collections;

public class SpellOrb : MonoBehaviour {
	
	enum ELEMENTS {ether=0,fire=1,water=2,wind=3,earth=4,spirit=5};

	public GameObject camera;
	public RaycastHit hit;
	
	public GameObject wind, water, land, fire, spirit;
	bool oWind, oWater, oLand, oFire, oSpirit; //opaque
	float sWind, sWater, sLand, sFire, sSpirit; //show
	
	public Texture crosshair;
	public Transform fireballPrefab;
	public Transform iceballPrefab;
	public Transform icefirePrefab;
	public Transform landballPrefab;
	
	GameObject fireballPrefabGo;
	GameObject iceballPrefabGo;
	GameObject icefirePrefabGo;
	GameObject landballPrefabGo;
	
	Transform position;
	ELEMENTS[] myElements;
	float[] cooldowns;
	public int maxElements = 3;
	public float elementCooldown = 1;
	
	void Start () {
		myElements = new ELEMENTS[maxElements];
		cooldowns = new float[6];
		
		float iO = 0.31f; //initial opaccity
		sWind = iO; sWater = iO; sLand = iO; sFire = iO; sSpirit = iO;
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
			oFire = true;
		}
		if (Input.GetKeyUp("2")) {
			selectRune(ELEMENTS.water);
			oWater = true;
		}
		if (Input.GetKeyUp("3")) {
			selectRune(ELEMENTS.wind);
			oWind = true;
		}
		if (Input.GetKeyUp("4")) {
			selectRune(ELEMENTS.earth);
			oLand = true;
		}
		if (Input.GetKeyUp("5")) {
			selectRune(ELEMENTS.spirit);
			oSpirit = true;
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
			spellCode += (int)myElements[i]*(int)Mathf.Pow(10,i);
		}
		Debug.Log ("cast..." + spellCode);
		switch(spellCode){
		case 1:
			if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, 100.0f)){
				GameObject.Find("TheWizard").animation.Play("FireBallSpell"); //Loads the animation when click is pressed
				Vector3 dir = hit.point - this.transform.position;
				fireballPrefabGo = Instantiate(fireballPrefab,this.transform.position,Quaternion.LookRotation(dir)) as GameObject;
				Destroy(fireballPrefabGo, 10);
			}
			break;
			
		case 2:
			if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, 100.0f)){
				GameObject.Find("TheWizard").animation.Play("HelixSpell");
				Vector3 dir = hit.point - this.transform.position;
				iceballPrefabGo = Instantiate(iceballPrefab,this.transform.position,Quaternion.LookRotation(dir)) as GameObject;
				Destroy (iceballPrefabGo, 10);
			}

			break;
			
		case 21: case 12:
			if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, 100.0f)){
				icefirePrefabGo = Instantiate(icefirePrefab,hit.point, Quaternion.LookRotation(Vector3.up)) as GameObject;
				Destroy (icefirePrefabGo, 10);
			}
	
			break;
			
		case 3:
			Windblow.Cast(transform.position,camera.transform.forward);
			break;
			
		case 4:
			if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, 100.0f)){
				Vector3 dir = hit.point - this.transform.position;
				landballPrefabGo = Instantiate(landballPrefab,this.transform.position,Quaternion.LookRotation(dir)) as GameObject;
				Destroy (landballPrefab, 10);
			}
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
			oWind = false;
			oWater = false;
			oLand = false;
			oFire = false;
			oSpirit = false;
		}
	}
	
	void OnGUI(){
		
		if (oWind) 
		{
			if(sWind < 1)
				sWind += Time.deltaTime * 2;
			wind.guiTexture.color = Color.gray * sWind;
		}
		else
		{
			if(sWind >= 1)
				while(sWind > 0.31f)
					sWind -= Time.deltaTime * 2;
			wind.guiTexture.color = Color.gray * sWind;
		}
		
		if (oLand)
		{
			if(sLand < 1)
				sLand += Time.deltaTime * 2;
			land.guiTexture.color = Color.gray * sLand;
		}
		else
		{
			if(sLand >= 1)
				while(sLand > 0.31f)
					sLand -= Time.deltaTime * 2;
			land.guiTexture.color = Color.gray * sLand;
		}
		
		if (oWater)
		{
			if(sWater < 1)
				sWater += Time.deltaTime * 2;
			water.guiTexture.color = Color.gray * sWater;
		}
		else
		{
			if(sWater >= 1)
				while(sWater > 0.31f)
					sWater -= Time.deltaTime * 2;
			water.guiTexture.color = Color.gray * sWater;
		}
		
		if (oFire)
		{
			if(sFire < 1)
				sFire += Time.deltaTime * 2;
			fire.guiTexture.color = Color.gray * sFire;
		}
		else
		{
			if(sFire >= 1)
				while(sFire > 0.31f)
					sFire -= Time.deltaTime * 2;
			fire.guiTexture.color = Color.gray * sFire;
		}
		
		if (oSpirit)
		{
			if(sSpirit < 1)
				sSpirit += Time.deltaTime * 2;
			spirit.guiTexture.color = Color.gray * sSpirit;
		}
		else
		{
			if(sSpirit >= 1)
				while(sSpirit > 0.31f)
					sSpirit -= Time.deltaTime * 2;
			spirit.guiTexture.color = Color.gray * sSpirit;
		}
		
		string text="";
		for(int i=0;i<6;i++){
			text+=","+cooldowns[i];
		}
		GUI.Label(new Rect(0, 0, Screen.width, Screen.height),text );
		
		GUI.DrawTexture(new Rect(Screen.width/2-25, Screen.height/2-25, 50, 50), crosshair, ScaleMode.ScaleToFit, true, 1.0F);
	}
}
