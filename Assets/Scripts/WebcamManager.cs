using UnityEngine;
using System.Collections;

public class WebcamManager : MonoBehaviour
{
    public GameObject webCamGameObject;

    private WebCamTexture webCamTexture;
    private Color32[] pixelData;

    private int width = 800; //160;
    private int height = 600; //90;
    private int framerate = 60;

    private void Start()
    {
        webCamTexture = new WebCamTexture(width, height, framerate);

        pixelData = new Color32[width * height];

        webCamTexture.Play();
        webCamGameObject.GetComponent<Renderer>().material.mainTexture = webCamTexture;
    }

    [ContextMenu("GetPixels")]
    public float[,] GetPixels(int resolution)
    {
        webCamTexture.GetPixels32(pixelData);

        float sumOfGrayscale = 0.0f;
        int chunkSize = width / resolution;
        int otherSize = width / chunkSize;

        float[,] newValues = new float[otherSize, otherSize];
        int currentIndex = 0;

        for (int i = 0; i < width * height; i += chunkSize)
        {
            if (i > 0 && i%width==0)
            {
                i += width * chunkSize;
            }

            for (int y=0; y<chunkSize; y++)
            {
                for (int x=0; x<chunkSize; x++)
                {
                    int currentPixel = i + x + y * width;

                    if (currentPixel > 0 && currentPixel < width * height)
                    {
                        sumOfGrayscale +=
                            pixelData[currentPixel].r * 0.2126f +
                            pixelData[currentPixel].g * 0.7152f +
                            pixelData[currentPixel].b * 0.0722f;
                    }
                }
            }

            int xIndex = currentIndex % otherSize;
            int yIndex = Mathf.Max(0,(otherSize - 1) - (currentIndex / otherSize));

            newValues[xIndex, yIndex] = sumOfGrayscale / (chunkSize*chunkSize);
            currentIndex++;

            sumOfGrayscale = 0.0f;
        }

        return newValues;
    }
}
