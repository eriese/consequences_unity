using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

/// <summary>
/// Game manager.
/// </summary>
public class GameManager : MonoBehaviour {

	/// <summary>
	/// Gets the colors for bumpers, balls and such
	/// </summary>
	/// <value>The colors.</value>
	public static Color[] colors {
		get {
			return instance.cols;
		}
	}

	/// <summary>
	/// Gets or sets the number of correct bumper hits.
	/// </summary>
	/// <value>The hits.</value>
	public static int hits {
		get {
			return instance.hitCount;
		}
		set {
			instance.hitCount = value;
			instance.countText.text = value.ToString();
		}
	}

	/// <summary>
	/// The instance so this can operate as a singleton
	/// </summary>
	public static GameManager instance;

	/// <summary>
	/// The number of hits
	/// </summary>
	public int hitCount;

	/// <summary>
	/// The size of the bumpers.
	/// </summary>
	public int bumperSize;

	/// <summary>
	/// The colors, editable in the inspector
	/// </summary>
	public Color[] cols;

	public GameObject bumpers;

	public SpriteRenderer background;

	public static float GameSize {
		get {return instance.background.transform.localScale.x;}
	}

	public Text countText;          //Store a reference to the UI Text component which will display the number of pickups collected.
	/// <summary>
	/// The bumpers.
	/// </summary>
	private BumperPair[] bumperPairs;

	private BallController ballControl;

	private Dictionary<Color, BumperColor> bumperColors;

	/// <summary>
	/// The random seed, for getting random colors
	/// </summary>
	private static System.Random rand = new System.Random();

	void Awake() {
		instance = this;
	}

	// Use this for initialization
	void Start () {
		bumperPairs = bumpers.GetComponentsInChildren<BumperPair>();
		ballControl = GetComponent<BallController>();
		StartGame();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// Get a random color based on the number of hits so far
	/// </summary>
	/// <returns>The color.</returns>
	public static Color RandomColor() {
		int numColors = 2;
		if (hits > 20) {
			numColors = 8;
		} 
		else if (hits > 10) {
			numColors = 6;
		}
		else if (hits > 5) {
			numColors = 4;
		}

		int colorIndex = rand.Next(0, numColors);

		return (Color) colors[colorIndex];
	}

	public static void StartGame() {
//		colors.;
    	instance.bumperColors = new Dictionary<Color, BumperColor>();
	    bool incUp = false;
	    for (int i = 0; i < colors.Length; ++i) {
	    	Color thisColor = colors[i];
			// get which thing it does. each thing has two colors
			int doesWhat = (int) Math.Floor((double) i/2);
			instance.bumperColors[thisColor] = new BumperColor(thisColor, doesWhat, incUp);
			// reverse whether it increments up or down for the next one
			incUp = !incUp;
	    }
		
	}

	public static void SetBumperSize(Boolean isInc) {
		instance.ChangeBumperSize(isInc);
	}

	public void ChangeBumperSize(Boolean isInc) {
		if (bumperSize < 20 && isInc) {
			bumperSize++;
		} else if (bumperSize > 5 && !isInc) {
			bumperSize--;
		}

		for(int i = 0; i< bumperPairs.Length; i++) {
			bumperPairs[i].SetBumperSize(bumperSize);
		}
	}

	public static BumperColor GetBumperEffect(Color bumperColor) {
		return instance.bumperColors[bumperColor];
	}

	void Reset() {
		cols = new Color[] {
			new Color32 (137, 240, 255, 255),
			new Color32 (216, 94, 0, 255),
			new Color32 (218, 109, 214, 255),
			new Color32 (109, 218, 152, 255),
			new Color32 (218, 207, 109, 255),
			new Color32 (116, 109, 218, 255),
			new Color32 (218, 109, 120, 255),
			new Color32 (134, 28, 161, 255)
		};
		hitCount = 0;
	}

	public static void HandleIncorrect(Color bumpColor) {
		instance.StartCoroutine(instance.Impact(bumpColor));
	}

	private IEnumerator Impact(Color bumpColor) {
		yield return new WaitForSeconds(0.5f);

		BumperColor bumperColor = GameManager.GetBumperEffect(bumpColor);
			switch (bumperColor.doesWhat) {
				case BumperColor.CHANGE_BUMP : 
					Debug.Log("Changing bumper size with incUp " + bumperColor.incUp);
					GameManager.SetBumperSize(bumperColor.incUp);
					break;
				case BumperColor.CHANGE_RAD :
					Debug.Log("Changing ball radius with incUp " + bumperColor.incUp);
					ballControl.IncSize(bumperColor.incUp);
					break;
				case BumperColor.CHANGE_SPEED :
					Debug.Log("Changing ball speed with incUp " + bumperColor.incUp);
					ballControl.IncSpeed(bumperColor.incUp);
					break;
				case BumperColor.CHANGE_NOTHING :
					Debug.Log("Not changing anything");
					break;
			}
	}
}
