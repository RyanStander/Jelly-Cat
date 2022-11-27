using UnityEngine;

//complete list of unity inpector attributes https://docs.unity3d.com/ScriptReference/AddComponentMenu.html?_ga=2.45747431.2107391006.1601167752-1733939537.1520033247
//inspector attributes https://unity3d.college/2017/05/22/unity-attributes/
//Nvidia Flex video https://youtu.be/TNAKv1dkYyQ

namespace Softbody
{
    public class BoneCube : MonoBehaviour
    {
        /*
        E --------- F
        |           |
        |   A --------- B
        |   |       |   |
        |   |       |   |
        H --|------ G   |
            |           |
            D --------- C
    */
        [Header("Bones")]
        public GameObject A = null;
        public GameObject B = null;
        public GameObject C = null;
        public GameObject D = null;
        public GameObject E = null;
        public GameObject F = null;
        public GameObject G = null;
        public GameObject H = null;
        [Header("Spring Joint Settings")]
        [Tooltip("Strength of spring")]
        public float Spring = 100f;
        [Tooltip("Higher the value the faster the spring oscillation stops")]
        public float Damper = 0.2f;
        [Header("Other Settings")]
        public SoftbodyPhysics.ColliderShape Shape = SoftbodyPhysics.ColliderShape.Box;
        public float ColliderSize = 0.002f;
        public float RigidbodyMass = 1f; 
        public LineRenderer PrefabLine = null;
        public bool ViewLines = true;

        private void Start()
        {
            SoftbodyPhysics.Init(Shape, ColliderSize, RigidbodyMass, Spring, Damper, RigidbodyConstraints.None, PrefabLine, ViewLines);

            SoftbodyPhysics.AddCollider(ref A);
            SoftbodyPhysics.AddCollider(ref B);
            SoftbodyPhysics.AddCollider(ref C);
            SoftbodyPhysics.AddCollider(ref D);
            SoftbodyPhysics.AddCollider(ref E);
            SoftbodyPhysics.AddCollider(ref F);
            SoftbodyPhysics.AddCollider(ref G);
            SoftbodyPhysics.AddCollider(ref H);
            
            //A
            SoftbodyPhysics.AddSpring(ref A, ref B);
            SoftbodyPhysics.AddSpring(ref A, ref C);
            SoftbodyPhysics.AddSpring(ref A, ref D);
            SoftbodyPhysics.AddSpring(ref A, ref E);
            SoftbodyPhysics.AddSpring(ref A, ref F);
            SoftbodyPhysics.AddSpring(ref A, ref G);
            SoftbodyPhysics.AddSpring(ref A, ref H);

            //B
            SoftbodyPhysics.AddSpring(ref B, ref A);
            SoftbodyPhysics.AddSpring(ref B, ref C);
            SoftbodyPhysics.AddSpring(ref B, ref D);
            SoftbodyPhysics.AddSpring(ref B, ref E);
            SoftbodyPhysics.AddSpring(ref B, ref F);
            SoftbodyPhysics.AddSpring(ref B, ref G);
            SoftbodyPhysics.AddSpring(ref B, ref H);
            
            //C
            SoftbodyPhysics.AddSpring(ref C, ref B);
            SoftbodyPhysics.AddSpring(ref C, ref A);
            SoftbodyPhysics.AddSpring(ref C, ref D);
            SoftbodyPhysics.AddSpring(ref C, ref E);
            SoftbodyPhysics.AddSpring(ref C, ref F);
            SoftbodyPhysics.AddSpring(ref C, ref G);
            SoftbodyPhysics.AddSpring(ref C, ref H);
            
            //D
            SoftbodyPhysics.AddSpring(ref D, ref B);
            SoftbodyPhysics.AddSpring(ref D, ref C);
            SoftbodyPhysics.AddSpring(ref D, ref A);
            SoftbodyPhysics.AddSpring(ref D, ref E);
            SoftbodyPhysics.AddSpring(ref D, ref F);
            SoftbodyPhysics.AddSpring(ref D, ref G);
            SoftbodyPhysics.AddSpring(ref D, ref H);
            
            //E
            SoftbodyPhysics.AddSpring(ref E, ref B);
            SoftbodyPhysics.AddSpring(ref E, ref C);
            SoftbodyPhysics.AddSpring(ref E, ref D);
            SoftbodyPhysics.AddSpring(ref E, ref A);
            SoftbodyPhysics.AddSpring(ref E, ref F);
            SoftbodyPhysics.AddSpring(ref E, ref G);
            SoftbodyPhysics.AddSpring(ref E, ref H);
            
            //F
            SoftbodyPhysics.AddSpring(ref F, ref B);
            SoftbodyPhysics.AddSpring(ref F, ref C);
            SoftbodyPhysics.AddSpring(ref F, ref D);
            SoftbodyPhysics.AddSpring(ref F, ref E);
            SoftbodyPhysics.AddSpring(ref F, ref A);
            SoftbodyPhysics.AddSpring(ref F, ref G);
            SoftbodyPhysics.AddSpring(ref F, ref H);
            
            //G
            SoftbodyPhysics.AddSpring(ref G, ref B);
            SoftbodyPhysics.AddSpring(ref G, ref C);
            SoftbodyPhysics.AddSpring(ref G, ref D);
            SoftbodyPhysics.AddSpring(ref G, ref E);
            SoftbodyPhysics.AddSpring(ref G, ref F);
            SoftbodyPhysics.AddSpring(ref G, ref A);
            SoftbodyPhysics.AddSpring(ref G, ref H);
            
            //H
            SoftbodyPhysics.AddSpring(ref H, ref B);
            SoftbodyPhysics.AddSpring(ref H, ref C);
            SoftbodyPhysics.AddSpring(ref H, ref D);
            SoftbodyPhysics.AddSpring(ref H, ref E);
            SoftbodyPhysics.AddSpring(ref H, ref F);
            SoftbodyPhysics.AddSpring(ref H, ref G);
            SoftbodyPhysics.AddSpring(ref H, ref A);
            
        }
    }
}