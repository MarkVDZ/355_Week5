using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycaster : MonoBehaviour {

    public GameObject prefab;
    public GameObject cylindar;
    public LayerMask layerMask;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1")){
            DoRaycast();
        }
        //DoRaycast();
    }

    private void DoRaycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction, Color.yellow, 5);

        if(Physics.Raycast(ray, out hit, 10, layerMask))
        {
            //hit.collider.GetComponent<MeshRenderer>().material.color = Color.red;

            Quaternion rot = Quaternion.FromToRotation(Vector3.up, hit.normal);

            //Instantiate(prefab, hit.point, rot);
            cylindar.transform.position = hit.point;
            cylindar.transform.rotation = rot;

            print("succeess?");
        }
    }
}
