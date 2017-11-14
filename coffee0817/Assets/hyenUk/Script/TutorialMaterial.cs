using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMaterial : MonoBehaviour {

    Material met;
    MeshRenderer[] mat ;
    Material[] Temp = new Material[11];
    // Use this for initialization
    void Start () {
        mat = this.gameObject.GetComponentsInChildren<MeshRenderer>();

        met = (Material)Resources.Load("Materials/TutorialMaterial");

    }

    //게임메니져에서 센드메시지로 호출 
   public void OnTutorial()
    {
        if(GameManager.instance.tuto)
        {
            StartCoroutine(this.ChangeMaterial());
        }
     
    }

    //색을 반짝이게 표현
    IEnumerator ChangeMaterial()
    {
        for (int a = 0; a < mat.Length; a++)
        {
            Temp[a] = mat[a].material;
        }

        for (int a=0; a < mat.Length;a++)
        {
            mat[a].material = met;
        }

        yield return new WaitForSeconds(0.5f);

        for (int a = 0; a < mat.Length; a++)
        {
            mat[a].material = Temp[a];
        }

        yield return new WaitForSeconds(0.5f);

        for (int a = 0; a < mat.Length; a++)
        {
            mat[a].material = met;
        }

        yield return new WaitForSeconds(0.5f);

        for (int a = 0; a < mat.Length; a++)
        {
            mat[a].material = Temp[a];
        }

        yield return new WaitForSeconds(0.5f);

        for (int a = 0; a < mat.Length; a++)
        {
            mat[a].material = met;
        }

        yield return new WaitForSeconds(0.5f);

        for (int a = 0; a < mat.Length; a++)
        {
            mat[a].material = Temp[a];
        }
    }
}
