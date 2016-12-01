using UnityEngine;
using System.Collections;

public class MovableAreaRestrictedCamera : MonoBehaviour {

	public GameObject target;
	private Vector3 offsetPosition;
	private StageAreaSetting stageAreaController;
	private float distance = 10f;//Distance between camera and player.

	//for display area of camera
	private Vector3 cameraBottomLeft;
	private Vector3 cameraTopLeft;
	private Vector3 cameraBottomRight;
	private Vector3 cameraTopRight;
	public float cameraRangeWidth;
	public float cameraRangeHeight;

	//display area of camera with blue line
	void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(cameraBottomLeft, cameraTopLeft);
		Gizmos.DrawLine(cameraTopLeft, cameraTopRight);
		Gizmos.DrawLine(cameraTopRight, cameraBottomRight);
		Gizmos.DrawLine(cameraBottomRight, cameraBottomLeft);
	}

	void Start()
	{
		//get the player
		target = GameObject.FindGameObjectWithTag("Player");
		offsetPosition = transform.position - target.transform.position;
		//get the StageRangeSetting class
		stageAreaController = GameObject.Find("Stage").GetComponent();
	}

	void Update()
	{
		Vector3 newPosition;//The target coordinate of camera
		Vector3 limitPosition;//The target coordinate of restricted camera
		float newX = 0f;
		float newY = 0f;
		
		newPosition = target.transform.position + offsetPosition + Vector3.up*3f;
		
		cameraBottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
		cameraTopRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, distance));
		cameraTopLeft = new Vector3(cameraBottomLeft.x, cameraTopRight.y, cameraBottomLeft.z);
		cameraBottomRight = new Vector3(cameraTopRight.x, cameraBottomLeft.y, cameraTopRight.z);
		cameraRangeWidth = Vector3.Distance(cameraBottomLeft, cameraBottomRight);
		cameraRangeHeight = Vector3.Distance(cameraBottomLeft, cameraTopLeft);
		//Limit movable area of camera to stage area
		newX = Mathf.Clamp(newPosition.x, stageAreaController.StageRect.xMin + cameraRangeWidth/2, stageAreaController.StageRect.xMax-cameraRangeWidth/2);
		newY = Mathf.Clamp(newPosition.y, 0, stageAreaController.StageRect.yMax - cameraRangeHeight/2);
		//Set coordinates to camera position
		limitPosition = new Vector3(newX,newY,this.transform.position.z);
		transform.position = limitPosition;
	}

}