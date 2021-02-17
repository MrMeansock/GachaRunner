using UnityEngine;

public class Rectangle : MonoBehaviour
{
    float lifespan; // in seconds

    void Update()
    {
        lifespan -= Time.deltaTime;

        if (lifespan <= 0)
        {
            Destroy(gameObject);
        }
    }

    public static Rectangle MakeRectangle(Rectangle rectanglePrefab, RectanglePreview preview, float lifespan)
    {
        Rectangle rectangle = Instantiate(rectanglePrefab, preview.transform.position, preview.transform.rotation);
        rectangle.transform.localScale = preview.transform.localScale;
        rectangle.lifespan = lifespan;
        return rectangle;
    }
}