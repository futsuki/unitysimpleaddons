using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(AnchorPosition))]
public class PositionAnchorDrawer : Editor
{
	/// The RenderMapGizmo method will be called if the map is selected. 
	[DrawGizmo(GizmoType.NotSelected | GizmoType.Selected | GizmoType.Pickable | GizmoType.SelectedOrChild)]
	static void RenderMapGizmono(AnchorPosition map, GizmoType gizmoType) 
	{
		var p = map.transform.position;
		Handles.color = Gizmos.color = map.color;
		Gizmos.DrawSphere(p, map.transform.localScale.magnitude*0.2f);
		Handles.color = Gizmos.color = new Color(1, 1, 1, 1);
		Handles.Label(p, map.name);
	}

	[DrawGizmo(GizmoType.Active)]
	static void RenderMapGizmo(AnchorPosition map, GizmoType gizmoType)
	{
		var p = map.transform.position;
		Handles.color = Gizmos.color = map.color;
		Gizmos.DrawSphere(p, map.transform.localScale.magnitude*0.2f);
		Handles.color = Gizmos.color = new Color(1, 1, 1, 1);
		Handles.Label(p, map.name);
		Gizmos.DrawLine(p-map.transform.forward, p+map.transform.forward);
		Gizmos.DrawLine(p-map.transform.right, p+map.transform.right);
		Gizmos.DrawLine(p-map.transform.up, p+map.transform.up);
	}
 }
