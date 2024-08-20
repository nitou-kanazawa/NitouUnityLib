using UniRx;
using UnityEngine;



namespace Project
{
    using nitou;
    using nitou.Inspector;

    public class TestPresenter : MonoBehaviour
    {

        public bool show;
        public bool boolB;

        [Range(0,10)]
        [EnableIf("show")]public float test;
        [ShowIf("show")] public float test2;


        [Space]

        public int intt;

        //public Vector2 vectorA;
        
        [MinMaxSlider(0,100)]
        public  Vector2 vectorB;



    }
}
