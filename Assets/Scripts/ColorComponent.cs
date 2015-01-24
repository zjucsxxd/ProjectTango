﻿using UnityEngine;
using System.Collections;

public class ColorComponent : MonoBehaviour 
{
	public enum pColor
	{
		grey = 0,
		blue = 1,
		red = 2
	}

	public pColor currentColor;
	private bool colorChanged;

	// Use this for initialization
	void Start () 
	{
		SetMaterialColor();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	void ForceMissingColor()
	{

	}
	
	void SetMaterialColor()
	{
		if (currentColor == pColor.blue)
		{
			renderer.material.color = Color.blue;

			renderer.material.SetColor("_MainColor", Color.blue);
		}
		else if (currentColor == pColor.red)
		{
			renderer.material.color = Color.red;

			renderer.material.SetColor("_MainColor", Color.red);
		}
		else
		{
			renderer.material.color = Color.white;

			renderer.material.SetColor("_MainColor", Color.white);
		}
	}

	bool IsSameColor(pColor color)
	{
		return currentColor == color;
	}
}
