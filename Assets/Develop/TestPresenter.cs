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

        [EnableIf("show")]
        [Range(0,10)]
        public float test;
        [ShowIf("show")] public float test2;



        public int intt;

        //public Vector2 vectorA;
        
        [ShowIf("show")]
        [MinMaxSlider(0,100)]
        public  Vector2 vectorB;



    }
}
