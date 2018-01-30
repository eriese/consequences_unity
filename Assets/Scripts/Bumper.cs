using UnityEngine;
using System.Collections;

/// <summary>
/// Individual Bumper
/// </summary>
public class Bumper : MonoBehaviour {
	[SerializeField()]
	private SpriteRenderer corner;
	[SerializeField()]
	private SpriteRenderer side;

	void Awake() {
//		// get the sprite renderer
//		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	/// <summary>
	/// Sets the bumper's width.
	/// </summary>
	/// <param name="size">The size to set to.</param>
	public void SetSize(int size) {
		StartCoroutine(ChangeSize(size));
	}

	private IEnumerator ChangeSize(int size) {
		yield return new WaitForSeconds(0.1f);
		side.transform.localScale = new Vector3 (GameManager.GameSize / 2 - size, size);;
		corner.transform.localPosition = new Vector3(size, size);
	}

	/// <summary>
	/// Sets the bumper's color.
	/// </summary>
	/// <param name="setColor">The color to set it to</param>
	public void SetColor(Color setColor) {
		corner.color = setColor;
		side.color = setColor;
	}

	/// <summary>
	/// Gets the bumper's color.
	/// </summary>
	/// <returns>The color.</returns>
	public Color GetColor() {
		return side.color;
	}
}
