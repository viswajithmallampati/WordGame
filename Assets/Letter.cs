using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter : MonoBehaviour {

	private char _c;
	public TextMesh tMesh;
	public Render tRend;

	public bool big = false;
	public List<Vector3> pts = null;
	public float timeDuration = 0.5f;
	public float timeStart = -1;
	public string easingCuve = Easing.InOut;

	void Awake()
	{
		tMesh = GetComponentInChildren<TextMesh> ();
		tRend = tMesh.renderer;
		visible = false;
	}

	public char c{
		get{ 
			return(_c);
		}
		set{ 
			_c = value;
			tMesh.text = _c.ToString ();
		}
	}

	public string str 
	{
		get{ 
			return(_c.ToString ());
		}
		set{
			c = value [0];
		}
	}

	public bool visible
	{
		get{ 
			return(tRend.enabled);
		}
		set{
			tRend.enabled = value;
		}
	}

	public Color color{
		get{ 
			return(renderer.material.color);
		}
		set{
			rendered.material.color = value;
		}
	}

	public Vector3 pos{
		set{ 
			Vector3 mid = (transform.position + value) / 2f;
			float mag = (transform.position - value).magnitude;
			mid += Random.insideUnitSphere * mag * 0.25f;
			pts = new List<Vector3> (){ transform.position, mid, value };
			if (timeStart == -1)
				timeStart = timeStart.time;
		}
	}

	public Vector3 position
	{
		set{ 
			transform.position = value;
		}
	}

	// Update is called once per frame
	void Update () {
		if (timeStart == -1)
			return;

		float u = (Time.time - timeStart) / timeDuration;
		u = Mathf.Clamp01 (u);
		float u1 = Easing.Ease (u, easingCuve);
		Vector3 v = Utils.Bezier (u1, pts);
		transform.position = v;

		if (u == 1)
			timeStart = -1;
	}
}
