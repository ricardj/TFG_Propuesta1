using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class TerrainGenerator : MonoBehaviour
{



    int xSize = 10;
    int zSize = 10;



    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    //Generation params
    public Vector2 cellSize = new Vector2(50,50);
    public int outerCellsOffset = 3;
    public int xCells = 10;
    public int zCells = 10;


    public bool update = false;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        
        CreateShape();
        UpdateMesh();
    }

    void CreateShape(){
        xSize = (outerCellsOffset+ xCells)*(int)cellSize.x;
        zSize = (outerCellsOffset+ zCells)*(int)cellSize.y;

        vertices = new Vector3[(xSize +1) * (zSize + 1)];
        
        for(int z = 0, i = 0; z <= zSize; z++){
            for(int x = 0; x <= xSize; x++){
                vertices[i] = new Vector3(x,0,z);
                i++;
            }
        }

        triangles = new int[xSize * zSize * 6];
        int vert = 0, tris = 0;
        for(int z = 0; z < zSize; z ++){

            for (int x = 0; x < xSize; x++){
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;
                vert++;
                tris += 6;      
            }
            vert++;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(update){
            CreateShape();
            UpdateMesh();
            update = false;
        }
    }

    void OnDrawGizmos(){
    
        if (vertices == null) return;
        for(int i = 0; i < vertices.Length; i++){
    
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }
}
