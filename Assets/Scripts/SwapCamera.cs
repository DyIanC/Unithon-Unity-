using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapCamera : MonoBehaviour
{
    private GameObject mainCam;
    public Vector3 positionGlobal;
    public Vector3 rotationGlobal;
    private GameObject[] tabCamIA;
    public int selectedIA;
    public bool globalActivated;

    /*
    * @do : Initialise la caméra sur la globale et désactive celles des IAs
    * @return void
    */
    void Start()
    {
        //On commence en position globale
        mainCam = GameObject.FindGameObjectWithTag("MainCamera");
        mainCam.SetActive(true);
        mainCam.transform.position = positionGlobal;
        mainCam.transform.rotation = Quaternion.Euler(rotationGlobal);
        globalActivated = true;
        
        //on récupère toute les caméra des IA sur la map
        tabCamIA = GameObject.FindGameObjectsWithTag("CharacterCamera");
        foreach(GameObject cam in tabCamIA){
            cam.SetActive(false);
        }
        selectedIA = 0;
    }

    /*
    * @do : Positionne la caméra à l'endroit voulu par l'utilisateur
    * @return void
    */
    void Update()
    {
        //cliquer sur la molette pour changer entre TPS et global
        if(Input.GetMouseButtonDown(2)){
            globalActivated = !globalActivated;
        }
        
        //cam global
        if(globalActivated){
            if(Input.GetAxis("Mouse ScrollWheel")==0){
                tabCamIA[Mathf.Abs(selectedIA) % tabCamIA.Length].SetActive(false);
                mainCam.SetActive(true);
            }
            else{
                globalActivated = !globalActivated;
                tabCamIA[Mathf.Abs(selectedIA) % tabCamIA.Length].SetActive(true);
                mainCam.SetActive(false);
            }
        }

        //cam TPS
        else{
            tabCamIA[Mathf.Abs(selectedIA) % tabCamIA.Length].SetActive(true);
            mainCam.SetActive(false);
            //changer vers l'IA précédente
            if(Input.GetAxis("Mouse ScrollWheel")<0){
                mainCam.SetActive(false);
                tabCamIA[Mathf.Abs(selectedIA) % tabCamIA.Length].SetActive(false);
                selectedIA--;
                tabCamIA[Mathf.Abs(selectedIA) % tabCamIA.Length].SetActive(true);
            }
            //changer vers l'IA suivante
            if(Input.GetAxis("Mouse ScrollWheel")>0){
                tabCamIA[Mathf.Abs(selectedIA) % tabCamIA.Length].SetActive(false);
                selectedIA++;
                tabCamIA[Mathf.Abs(selectedIA) % tabCamIA.Length].SetActive(true);
            }
        }

        
        
    }
}