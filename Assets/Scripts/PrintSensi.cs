using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintSensi : MonoBehaviour {

	public Slider sensibility;
	
	public void CatSensi(){
		GetComponent<InputField>().text = sensibility.value.ToString();
	}

	public void SetSensi(){
		string text = GetComponent<InputField>().text;
		sensibility.value = float.Parse(text);
	}
}
