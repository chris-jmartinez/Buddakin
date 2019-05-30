using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoInventory : MonoBehaviour {

	[SerializeField]
	public GameObject[] objects;

	private int count = 0;
	private int timer = 0;

	void Update () {
		if (count > objects.Length - 1)
			return;

		timer++;

		if ( timer % 100 == 0) {
			string name = objects [count].name;
			InventoryManager.Instance.addItem (objects[count]);
			Debug.logger.Log (InventoryManager.Instance.findItem(objects [count].name));
			count++;
		}

	}
}