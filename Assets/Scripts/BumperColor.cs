using System;
using UnityEngine;

public class BumperColor {
	public const int CHANGE_NOTHING = 0;
	public const int CHANGE_SPEED = 1;
	public const int CHANGE_RAD = 2;
	public const int CHANGE_BUMP = 3;

	private Color color;

	public Color Color {
		get {
			return color;
		}
	}

	public int doesWhat;
	public bool incUp;

	public BumperColor (Color color, int doesWhat, bool incUp) {
		this.color = color;
		this.doesWhat = doesWhat;
		this.incUp = incUp;
	}
}

