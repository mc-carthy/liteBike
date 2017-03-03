using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[RequireComponent (typeof (LineRenderer))]
[RequireComponent (typeof (EdgeCollider2D))]
public class Trail : MonoBehaviour {

    [SerializeField]
    private float pointSpacing;
    [SerializeField]
    private Transform bike;

	private LineRenderer line;
	private EdgeCollider2D col;
    private List<Vector2> points;

    private void Awake ()
    {
        line = GetComponent<LineRenderer> ();
        col = GetComponent<EdgeCollider2D> ();
        points = new List<Vector2> ();
    }

    private void Start ()
    {
        SetPoint ();
    }

    private void Update ()
    {
        if (Vector3.Distance (points.Last (), bike.position) > pointSpacing)
        {
            SetPoint ();
        }
    }

    private void SetPoint ()
    {
        if (points.Count > 1)
        {
            col.points = points.ToArray<Vector2> ();
        }

        points.Add (bike.position);

        line.numPositions = points.Count;
        line.SetPosition (points.Count - 1, bike.position);
    }

}
