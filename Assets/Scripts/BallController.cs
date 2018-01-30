using UnityEngine;
using System.Collections;
//Adding this allows us to access members of the UI namespace including Text.
using UnityEngine.UI;

public class BallController : MonoBehaviour {
	public float prevSpeed;             //Floating point variable to store the player's movement speed.
	public float currSpeed;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.

	void Start () {
		//Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D> ();
        spriteRenderer = GetComponent<SpriteRenderer> ();
		spriteRenderer.color = GameManager.RandomColor();
	}

	void FixedUpdate() {
		Vector2 goDir = rb2d.velocity / prevSpeed;
		//Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis ("Horizontal");
        if (moveHorizontal != 0) {
        	rb2d.constraints = RigidbodyConstraints2D.FreezePositionY;
			float sendSpeed = moveHorizontal > 0 ? 1 : -1;
			goDir = new Vector2(sendSpeed, 0);
		}

        //Store the current vertical input in the float moveVertical.
		else {
			float moveVertical = Input.GetAxis ("Vertical");
			if (moveVertical != 0) {
	        	rb2d.constraints = RigidbodyConstraints2D.FreezePositionX;
				float sendSpeed = moveVertical > 0 ? 1 : -1;
	        	goDir = new Vector2(0, sendSpeed);
        	}
        }

        rb2d.velocity = goDir * currSpeed;
        prevSpeed = currSpeed;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		Color colColor = coll.gameObject.GetComponentInParent<Bumper>().GetColor();
		if (spriteRenderer.color == colColor) {
			GameManager.hits++;
		} else {
			GameManager.HandleIncorrect(colColor);
			GameManager.hits--;

		}
		spriteRenderer.color = GameManager.RandomColor();
	}

	public void IncSpeed(bool incUp) {
		if (incUp) {
			currSpeed = prevSpeed + 1;
		}
		else {
			currSpeed = prevSpeed - 1;
		}
	}

	public void IncSize (bool incUp) {
		if (incUp) {
			transform.localScale += new Vector3(1,1);
		} else {
			transform.localScale -= new Vector3(1,1);
		}
	}
}

