using UnityEngine;
using System.Collections;

public class BumperPair : MonoBehaviour {

	private Bumper[] bumpers;
	// Use this for initialization
	void Start () {
		bumpers = GetComponentsInChildren<Bumper>();
		StartCoroutine(ChangeColors());
	}

	private IEnumerator ChangeColors() {
		while (true) {
			Color firstColor = GameManager.RandomColor();
			Color secondColor = GameManager.hits < 30 ? firstColor : GameManager.RandomColor();
			bumpers[0].SetColor(firstColor);
			bumpers[1].SetColor(secondColor);

			yield return new WaitForSeconds(5);
		}
	}

	public void SetBumperSize(int size) {
		for (int i = 0; i < bumpers.Length; i++) {
			bumpers[i].SetSize(size);
		}
	}
}
