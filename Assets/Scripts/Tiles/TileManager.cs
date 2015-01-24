﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Direction {
	UP, DOWN, LEFT, RIGHT
}

public class TileManager : MonoBehaviour 
{
	const int width = 16, height = 9;
	Tile[] tiles;

	public GameObject defaultTile;

	void Start () 
	{
		tiles = new Tile[width*height];
		GameObject[] holders = GameObject.FindGameObjectsWithTag("TileHolders");// transform.GetComponentsInChildren<Transform>();

		for(int i = 0; i < holders.Length; i++) 
		{
			int y = (int)char.GetNumericValue(holders[i].name[0]);
			int x = (int)char.GetNumericValue(holders[i].name[2]);

			if(holders[i].transform.childCount == 0) 
			{
				GameObject newTile = (GameObject)Instantiate(defaultTile);
				newTile.transform.parent = holders[i].transform;
				newTile.transform.localPosition = Vector3.zero;
				tiles[y*width + x] = newTile.GetComponent<Tile>();
			} 
			else 
			{
				tiles[y*width + x] = holders[i].transform.GetChild(0).GetComponent<Tile>();
			}
		}
	}

	/// <summary>
	/// Returns tile if empty and reserves it. Returns null if not empty or doesnt exist. Does NOT unreserve current. Fix if you want
	/// </summary>
	/// <returns>The to tile.</returns>
	/// <param name="tile">Tile.</param>
	/// <param name="direction">Direction.</param>
	public Tile MoveToTile( Tile tile, Direction direction )
	{
		int index = GetIndex(tile);
		int targetIndex = 0;
		switch(direction) {
		case Direction.UP:
			targetIndex = index - width;

			if(targetIndex > 0) {
				if(!tiles[targetIndex].IsReserved()) {
					tiles[targetIndex].ReserveNode(true);
					return tiles[targetIndex];
				}
			}
			break;
		case Direction.DOWN:
			targetIndex = index + width;

			if(targetIndex < (width * height) - 1) {
				if(!tiles[targetIndex].IsReserved()) {
					tiles[targetIndex].ReserveNode(true);
					return tiles[targetIndex];
				}
			}
			break;
		case Direction.LEFT:
			targetIndex = (index % width) - 1;

			if(targetIndex > 0) 
			{
				if(!tiles[index - 1].IsReserved()) 
				{
					tiles[index - 1].ReserveNode(true);
					return tiles[index - 1];
				}
			}
			break;
		case Direction.RIGHT:
			targetIndex = (index % width) + 1;

			if(targetIndex < width - 1) 
			{
				if(!tiles[index + 1].IsReserved()) 
				{
					tiles[index + 1].ReserveNode(true);
					return tiles[index + 1];
				}
			}
			break;
		default:
			return null;
			break;
		}
		return null;
	}

	int GetIndex(Tile tile) {
		for(int i = 0; i < tiles.Length; i++) {
			if(tile == tiles[i])
				return i;
		}
		return -1;
	}

	public Tile GetTileFromIndex(int index) {
		return tiles[index];
	}
}
