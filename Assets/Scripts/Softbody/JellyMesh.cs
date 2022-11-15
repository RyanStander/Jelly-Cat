using System;
using UnityEngine;

namespace Softbody
{
    public class JellyMesh : MonoBehaviour
    {
        public float Intensity = 1f;
        public float Mass = 1f;
        public float Stiffness = 1f;
        public float Damping = 0.75f;
        
        private Mesh originalMesh;
        private Mesh meshClone;
        private MeshRenderer renderer;
        private JellyVertex[] jellyVertices;
        private Vector3[] vertexArray;
        
        private void Start()
        {
            originalMesh = GetComponent<MeshFilter>().sharedMesh;
            meshClone = Instantiate(originalMesh);
            GetComponent<MeshFilter>().sharedMesh = meshClone;
            renderer = GetComponent<MeshRenderer>();

            jellyVertices = new JellyVertex[meshClone.vertices.Length];
            for (var i = 0; i < meshClone.vertices.Length; i++)
            {
                jellyVertices[i] = new JellyVertex(i, transform.TransformPoint(meshClone.vertices[i]));
            }
        }

        private void FixedUpdate()
        {
            vertexArray = originalMesh.vertices;
            for (var i = 0; i < jellyVertices.Length; i++)
            {
                var target = transform.TransformPoint(vertexArray[jellyVertices[i].ID]);

                var intesity = (1 - (renderer.bounds.max.y - target.y) / renderer.bounds.size.y) * Intensity;
                
                jellyVertices[i].Shake(target,Mass,Stiffness,Damping);
                target = transform.InverseTransformPoint(jellyVertices[i].Position);
                vertexArray[jellyVertices[i].ID] = Vector3.Lerp(vertexArray[jellyVertices[i].ID], target, Intensity);
            }

            meshClone.vertices = vertexArray;
        }

        public class JellyVertex
        {
            public int ID;
            public Vector3 Position;
            public Vector3 Velocity;
            public Vector3 Force;

            public JellyVertex(int id, Vector3 pos)
            {
                ID = id;
                Position = pos;
            }

            public void Shake(Vector3 target, float mass, float stiffness, float damping)
            {
                Force = (target - Position) * stiffness;
                Velocity = (Velocity + Force / mass) * damping;
                Position += Velocity;

                if ((Velocity+Force+Force/mass).magnitude<0.001f)
                {
                    Position = target;
                }
            }
        }
    }
}
