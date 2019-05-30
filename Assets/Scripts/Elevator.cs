using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour {

	public GameObject floor;
	public int floorNumber;
	public bool isWorking = true;
	public float liftTime;
	public float liftWaitingTime;

	Player player;
	Rigidbody2D rb;
	BoxCollider2D bc;
	Animator anim; 
	GameObject go;

	Elevator nextElevator;
	bool m_up;
	bool m_down;
	bool onLiftZone;
	bool canUseLift;
	bool liftIsOccupied;
	int nextFloor;
	float floor_distance = 0f;

	void Start () {
		player = FindObjectOfType<Player>();
		rb = player.gameObject.GetComponent<Rigidbody2D>() as Rigidbody2D;
		bc = GetComponent<BoxCollider2D>() as BoxCollider2D;
		anim = GetComponent<Animator>() as Animator;

		liftIsOccupied = false;
		canUseLift = false;
		onLiftZone = false;

		//If lift is not working in current floor, player q can pass trough, no animation and other mechanics will work
		if (!isWorking) bc.isTrigger = true;
	}

	void Update(){
		if (isWorking && onLiftZone){
			m_up = Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W);
			m_down = Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S);

			if ((m_up || m_down) && !liftIsOccupied && onLiftZone) {

				if ((floorNumber == 12 && m_up) || (floorNumber == -1 && m_down)) {
					nextFloor = -100;
					canUseLift = false;
				} else {
					if (m_up) {
						nextFloor = floorNumber + 1;
					} else {
						nextFloor = floorNumber - 1;
					}

					if (GameObject.Find ("Elevator " + nextFloor) != null && GameObject.Find ("Elevator " + nextFloor).GetComponent<Elevator> () != null) {
						nextElevator = GameObject.Find ("Elevator " + nextFloor).GetComponent<Elevator> ();
						canUseLift = nextElevator.IsWorking;
					} else {
						canUseLift = false;
					}

					if (canUseLift) {
						//If player has pressed the button Up, start moving platform
						//Do something to flib the player and make it not move
						go = GameObject.Find ("Floor Level " + floorNumber);
						floor.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, go.transform.position.z);

						go = GameObject.Find ("Floor Level " + nextFloor);
						floor_distance = go.transform.position.y - floor.transform.position.y;
						StartCoroutine (UseLift ());
					}
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player" && isWorking) {
			onLiftZone = true;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player" && isWorking) {
			onLiftZone = false;
			StartCoroutine (ExitLift());
		}
	}

	IEnumerator ExitLift(){
		yield return new WaitForSeconds (.5f);
		anim.SetBool ("open", false);
		anim.SetBool ("close", true);
		yield return new WaitForSeconds (.5f);
		anim.SetBool ("close", false);
		yield return null;
	}

	IEnumerator UseLift(){
		player.OnLift = true;
		rb.isKinematic = true;
		anim.SetBool ("close", false);
		anim.SetBool ("open", true);
		yield return new WaitForSeconds (1.2f);
		anim.SetBool ("open", false);
		anim.SetBool ("close", true);
		yield return new WaitForSeconds (.8f);
		gameObject.GetComponent<SpriteRenderer> ().sortingOrder = 2;
		liftIsOccupied = true;
		player.transform.parent = floor.transform;
		nextElevator.OrderLayer (2);
		iTween.MoveAdd (floor,iTween.Hash("y", floor_distance, "time", liftTime, "looptype", iTween.LoopType.none,"easetype",iTween.EaseType.linear, "delay", 2));
		yield return new WaitForSeconds (liftWaitingTime);
		liftIsOccupied = false;
		gameObject.GetComponent<SpriteRenderer> ().sortingOrder = 0;
		anim.SetBool ("close", false);
		nextElevator.OpenAsNextElevator ();
		player.OnLift = false;
		player.transform.parent = null;
		rb.isKinematic = false;
		yield return null;
	}

	void OpenAsNextElevator(){
		bc.isTrigger = true;
		anim.SetBool ("close", false);
		anim.SetBool ("open", true);
		OrderLayer(0);
	}

	public void OrderLayer(int order){
		gameObject.GetComponent<SpriteRenderer>().sortingOrder = order;
	}

	public bool IsWorking {
		get { 
			return isWorking;
		}
	}
}