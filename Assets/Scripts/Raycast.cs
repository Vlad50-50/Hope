using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Raycast : MonoBehaviour
{
    GameObject obj;
    GameObject objPrev;
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            // obj = hit.transform.gameObject;
            // if (objPrev == null)
            // {
            //     obj.GetComponent<MeshRenderer>().material.color = Color.red;
            //     objPrev = obj;

            // }
            // else if (!objPrev.Equals(obj))
            // {
            //     objPrev.GetComponent<MeshRenderer>().material = obj.GetComponent<MeshRenderer>().material;
            //     objPrev = null;
            // }


        }
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(obj, new Vector3(obj.transform.position.x,
                                              obj.transform.position.y+1,
                                              obj.transform.position.z),
                        Quaternion.identity);
        }
        if (Input.GetMouseButtonDown(1))
        {
            Destroy(obj);
        }



    }
}
