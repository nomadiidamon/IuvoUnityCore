using UnityEngine;

namespace IuvoUnity
{
    namespace Colors
    {
        public class LerpColor : MonoBehaviour
        {
            public Color startingColor = Color.green;
            public bool firstPass = false;
            public Color middleColor = Color.yellow;
            public bool secondPass = false;
            public Color endingColor = Color.red;
            public bool finished = false;

            public Color currentColor = Color.white;

            public float lerpSpeed = 0.25f;

            public MeshRenderer rend;

            void Start()
            {
                rend.material.color = startingColor;
            }

            void Update()
            {
                if (finished)
                {
                    return;
                }
                if (!firstPass)
                {
                    FirstPass();
                }
                else
                {
                    SecondPass();
                    finished = true;
                }
            }

            public void FirstPass()
            {
                currentColor = Color.Lerp(startingColor, middleColor, lerpSpeed);
                rend.material.color = currentColor;
                firstPass = true;
            }

            public void SecondPass()
            {
                currentColor = Color.Lerp(middleColor, endingColor, lerpSpeed);
                rend.material.color = currentColor;
                secondPass = true;
            }


        }

    }
}