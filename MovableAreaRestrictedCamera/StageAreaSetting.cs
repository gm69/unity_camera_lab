using UnityEngine;
using System.Collections;

public class StageAreaSetting : MonoBehaviour {

	private Transform floor;
	public float StageHeight = 10f;//Stage height

	public Rect StageRect;//Set range of stage with Rect
	public Vector3 LowerLeft;
	public Vector3 UpperLeft;
	public Vector3 LowerRight;
	public Vector3 UpperRight;

	//Stage range
	void OnDrawGizmos()
	{
		LowerLeft = new Vector3 (StageRect.xMin, StageRect.yMax, 0);
		UpperLeft = new Vector3 (StageRect.xMin, StageRect.yMin, 0);
		LowerRight = new Vector3 (StageRect.xMax, StageRect.yMax, 0);
		UpperRight = new Vector3 (StageRect.xMax, StageRect.yMin, 0);

		Gizmos.color = Color.red;
		Gizmos.DrawLine(LowerLeft, UpperLeft);
		Gizmos.DrawLine(UpperLeft, UpperRight);
		Gizmos.DrawLine(UpperRight, LowerRight);
		Gizmos.DrawLine(LowerRight, LowerLeft);
	}

	void Start()
	{
		//Get the ground
		floor = transform.Find("Floor");
		//Set Rect based on ground Collider
		Bounds floorBounds = floor.GetComponent().bounds;
		StageRect.xMin = floorBounds.min.x;
		StageRect.xMax = floorBounds.max.x;
		StageRect.yMin = floorBounds.min.y;
		StageRect.yMax = floorBounds.max.y+StageHeight;
	}

	

}