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
        
            //down
            SoftbodyPhysics.AddSpring(ref A, ref D);
            SoftbodyPhysics.AddSpring(ref B, ref C);
            SoftbodyPhysics.AddSpring(ref F, ref G);
            SoftbodyPhysics.AddSpring(ref E, ref H);

            //across
            SoftbodyPhysics.AddSpring(ref A, ref G);
            SoftbodyPhysics.AddSpring(ref B, ref H);
            SoftbodyPhysics.AddSpring(ref F, ref D);
            SoftbodyPhysics.AddSpring(ref E, ref C);

            //top
            SoftbodyPhysics.AddSpring(ref A, ref B);
            SoftbodyPhysics.AddSpring(ref B, ref F);
            SoftbodyPhysics.AddSpring(ref F, ref E);
            SoftbodyPhysics.AddSpring(ref E, ref A);

            //bottom
            SoftbodyPhysics.AddSpring(ref D, ref C);
            SoftbodyPhysics.AddSpring(ref C, ref G);
            SoftbodyPhysics.AddSpring(ref G, ref H);
            SoftbodyPhysics.AddSpring(ref H, ref D);
        }
    }
}