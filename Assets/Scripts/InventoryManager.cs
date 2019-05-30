using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {

	public static InventoryManager Instance = null;

	public int width = 40;
	public int height = 40;

	private List<string> elements = new List<string>();

	void Start() {
		
	}

	void Awake() {
		if ( Instance == null ) {
			Instance = this;
		} else {
			Destroy (gameObject);
		}
	}

	public void addItem(GameObject go) {
		if ( elements.Count == 0 ) {
			GameObject.Destroy (GameObject.Find("element0"));

		}

		createButton (go);

		elements.Add (go.name);
		GameObject.Find("Inventory Scroll").GetComponent<ScrollSnapRect> ().restart ();
	}

	public bool findItem(string name) {
		return elements.Contains (name);
	}

	private void createButton(GameObject go) {
		GameObject button = new GameObject();

		button.name = go.name;
		button.AddComponent<RectTransform> ();
		button.AddComponent<Image> ();
		button.AddComponent<Button> ();
		button.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);

		Image image = button.GetComponent<Image> ();
		image.sprite = go.GetComponent<SpriteRenderer> ().sprite;

		button.transform.position = Vector3.zero;
		button.transform.SetParent(GameObject.Find("Inventory Container").transform, false);
	}

}
