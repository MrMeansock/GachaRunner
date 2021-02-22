using UnityEngine;

namespace GotchaGuys
{
    namespace GameRectangle
    {
        public class Rectangle : MonoBehaviour
        {
            public void Init(RectanglePreview preview)
            {
                transform.position = preview.transform.position;
                transform.rotation = preview.transform.rotation;
                transform.localScale = preview.transform.localScale;
            } 

            public static Rectangle MakeRectangle(Rectangle rectanglePrefab, RectanglePreview preview)
            {
                Rectangle rectangle = Instantiate(rectanglePrefab);
                rectangle.Init(preview);
                return rectangle;
            }
        }
    }
}