using UnityEngine;
using System.Collections;

public class DynamicZoomCamera : MonoBehaviour {

	[SerializeField] Transform target = null, player = null;
	[SerializeField] Vector2 offset = new Vector2(1, 1);
	private float screenAspect = 0; 
	private Camera _camera = null;

	void Awake()
	{
		screenAspect = (float)Screen.height / Screen.width;
		_camera = GetComponent<Camera> ();
	}

	void Update () 
	{
		UpdateCameraPosition ();
		UpdateOrthographicSize ();
	}

	void UpdateCameraPosition()
	{
		// Update camera position from center point between two points
		Vector3 center = Vector3.Lerp (target.position, player.position, 0.5f);
		transform.position = center + Vector3.forward * -10;
	}

	void UpdateOrthographicSize()
	{
		// Get a vector between two points
		Vector3 targetsVector = AbsPositionDiff (target, player) + (Vector3)offset;

		
		float targetsAspect = targetsVector.y / targetsVector.x;
		float targetOrthographicSize = 0;
		if ( screenAspect < targetsAspect) {
			targetOrthographicSize = targetsVector.y * 0.5f;
		} else {
			targetOrthographicSize = targetsVector.x * (1/_camera.aspect) * 0.5f;
		}
		_camera.orthographicSize =  targetOrthographicSize;
	}

	Vector3 AbsPositionDiff(Transform target, Transform player)
	{
		Vector3 targetsDiff = target.position - player.position;
		return new Vector3(Mathf.Abs(targetsDiff.x), Mathf.Abs(targetsDiff.y));
	}
}