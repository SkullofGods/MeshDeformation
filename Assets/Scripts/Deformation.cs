using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deformation : MonoBehaviour
{
    private Mesh _mesh;
    private Vector3[] _meshOnStart;
    public bool isSoft;
    
    [Header("Deformation options")]
    public float minVelocity = 5f;
    public float radius = 0.5f;
    public float multiplayer = 0.05f;


    private void Awake()
    {
        _mesh = GetComponent<MeshFilter>().mesh;
        _meshOnStart = _mesh.vertices;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.relativeVelocity.magnitude > minVelocity && !isSoft)
        {
            bool isDeformated = false;
            Vector3[] verticles = _mesh.vertices;

            for (int i = 0; i < _mesh.vertexCount; i++)
            {
                for (int j = 0; j < other.contacts.Length; j++)
                {
                    Vector3 point = transform.InverseTransformPoint(other.contacts[j].point);
                    Vector3 velocity = transform.InverseTransformVector(other.relativeVelocity);
                    float distance = Vector3.Distance(point, verticles[i]);
                    if (distance < radius)
                    {
                        Vector3 deformation = velocity * (radius - distance) * multiplayer;
                        verticles[i] += deformation;
                        isDeformated = true;
                    }
                }
            } // end of "for"
            
            if (isDeformated)
            {
                _mesh.vertices = verticles;
                _mesh.RecalculateNormals();
                _mesh.RecalculateBounds();
                GetComponent<MeshCollider>().sharedMesh = _mesh;
            }
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.relativeVelocity.magnitude > minVelocity && isSoft)
        {
            bool isDeformated = false;
            Vector3[] verticles = _mesh.vertices;

            for (int i = 0; i < _mesh.vertexCount; i++)
            {
                for (int j = 0; j < other.contacts.Length; j++)
                {
                    Vector3 point = transform.InverseTransformPoint(other.contacts[j].point);
                    Vector3 velocity = transform.InverseTransformVector(other.relativeVelocity);
                    float distance = Vector3.Distance(point, verticles[i]);
                    if (distance < radius)
                    {
                        Vector3 deformation = velocity * (radius - distance) * multiplayer;
                        verticles[i] += deformation;
                        isDeformated = true;
                    }
                }
            } // end of "for"
            
            if (isDeformated)
            {
                _mesh.vertices = verticles;
                _mesh.RecalculateNormals();
                _mesh.RecalculateBounds();
                GetComponent<MeshCollider>().sharedMesh = _mesh;
            }
        }
    }
}
