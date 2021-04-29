/*! \file MeshGenerator.cs
 *  \brief Contains the mesh generator for the game
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
//! The class responsible for generating meshes
public class MeshGenerator : MonoBehaviour
{

    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;
    public int xSize = 20;
    public int zSize = 20;
    //! Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;    
        StartCoroutine(CreateShape());
    }

    void Update(){
        UpdateMesh();
    }

    //! This method defines the triangles and verticies that make up the mesh
    IEnumerator CreateShape(){ 
        vertices = new Vector3[(xSize + 1) * (zSize +1)];
        for(int i = 0, z = 0; z <= zSize; z++){
            for(int x = 0; x <= xSize; x++){
                float y = Mathf.PerlinNoise(x * .3f, z *.3f) *2f;
                vertices[i] = new Vector3(x, 0, z);
                i++;
            }
        }
        triangles = new int[xSize*zSize*6];
        int vert = 0;
        int tris = 0;
        for(int z = 0; z < zSize; z++){
            for(int x = 0; x < xSize; x++){
                triangles[tris+0] = vert + 0;
                triangles[tris+1] = vert + xSize + 1;
                triangles[tris+2] = vert + 1;
                triangles[tris+3] = vert + 1;
                triangles[tris+4] = vert + xSize + 1;
                triangles[tris+5] = vert + xSize + 2;
                vert++;
                tris+=6;
            }
            vert++;
            yield return new WaitForSeconds(.1f);
        }
    }

    //! Clears the mesh, then updates the triangles, their verticies, and the associated normals
    void UpdateMesh(){
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    //! Checks that the verticies are not null, then calls Gizmos.DrawSphere on verticies
    private void onDrawGizmos(){
        if(vertices == null){
            Debug.Log("vertices is null");
            return;
        }
        for(int i = 0; i < vertices.Length; i++){
            Gizmos.DrawSphere(vertices[i], .1f);
        }
    }
}
