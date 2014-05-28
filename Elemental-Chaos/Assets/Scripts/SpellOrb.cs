using UnityEngine;
using System.Collections;

public class SpellOrb : MonoBehaviour {

	enum ELEMENTS {ether=0,fire=1,water=2,wind=3,earth=4,spirit=5};

	public GameObject wind, water, land, fire, spirit;
	bool oWind, oWater, oLand, oFire, oSpirit; //opaque
	float sWind, sWater, sLand, sFire, sSpirit; //show

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

		if (oWind)
			Debug.Log("aire");
		
		if (oLand)
			Debug.Log("tierra");
		
		if (oWater)
			Debug.Log("agua");
		
		if (oFire)
			Debug.Log("fuego");
		
		if (oSpirit)
			Debug.Log("espiritu");
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
