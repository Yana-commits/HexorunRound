using System.Linq;
using UnityEngine;

namespace _3D_Haven
{
    public class TranslateYrandomize : MonoBehaviour
    {
        [Range(0,10)]
        public float Power;
        
        [ContextMenu("random")]
        private void Do()
        {
            foreach (var t in transform.Cast<Transform>())
            {
                t.position = new Vector3(t.position.x, Random.Range(-1f,1f) * Power, t.position.z);
            }
        }
    }
}