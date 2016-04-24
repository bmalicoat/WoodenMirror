using UnityEngine;
using System.Collections;

public class BlockManager : MonoBehaviour
{
    public Block BlockPrefab;
    public int Resolution;

    public WebcamManager webcam;

    private ArrayList blocks = new ArrayList();
    private float[,] blockValues;
    private float blockSize = 1.0f;
    private float gutterSize = 0.1f;
    private bool isDirty = true;

    private void Start()
    {
        float stepSize = blockSize + gutterSize;
        float startX = -Resolution * 0.5f * (stepSize);
        float currentX = startX;
        float currentY = -startX;

        blocks.Capacity = Resolution * Resolution;

        blockValues = new float[Resolution, Resolution];

        for (int y = 0; y < Resolution; y++)
        {
            for (int x = 0; x < Resolution; x++)
            {
                blockValues[x, y] = 128;
            }
        }

        for (int i = 0; i < Resolution; i++)
        {
            for (int j = 0; j < Resolution; j++)
            {
                Block b = Instantiate(BlockPrefab);

                b.transform.parent = transform;
                b.transform.localPosition = new Vector3(currentX, currentY, 0.0f);

                currentX += stepSize;
                blocks.Add(b);
            }

            currentY -= stepSize;
            currentX = startX;
        }
    }

    private void Update()
    {
        SetWebcamBlocks();

        if (!isDirty)
        {
            return;
        }

        for (int j = 0; j < Resolution; j++)
        {
            for (int i = 0; i < Resolution; i++)
            {
                Block b = blocks[i + j * Resolution] as Block;

                b.SetValue(blockValues[i, j]);
            }
        }

        isDirty = false;
    }

    [ContextMenu("SetWebcamBlocks")]
    public void SetWebcamBlocks()
    {
        SetBlockValues(webcam.GetPixels(Resolution));
    }

    public void SetBlockValues(float[,] newBlockValues)
    {
        for (int j = 0; j < Resolution; j++)
        {
            for (int i = 0; i < Resolution; i++)
            {
                blockValues[i, j] = newBlockValues[i, j];
            }
        }

        isDirty = true;
    }

    [ContextMenu("Checkerboard")]
    public void Checkerboard()
    {
        bool isOn = true;
        for (int y = 0; y < Resolution; y++)
        {
            for (int x = 0; x < Resolution; x++)
            {
                blockValues[x, y] = isOn ? 255 : 0;
                isOn = !isOn;
            }

            isOn = !isOn;
        }

        isDirty = true;
    }

    [ContextMenu("Checkerboard2")]
    public void Checkerboard2()
    {
        bool isOn = true;
        for (int y = 0; y < Resolution; y++)
        {
            for (int x = 0; x < Resolution; x++)
            {
                blockValues[x, y] = isOn ? 128 : 0;
                isOn = !isOn;
            }

            isOn = !isOn;
        }

        isDirty = true;
    }

    [ContextMenu("AllOn")]
    public void AllOn()
    {
        for (int y = 0; y < Resolution; y++)
        {
            for (int x = 0; x < Resolution; x++)
            {
                blockValues[x, y] = 255;
            }
        }

        isDirty = true;
    }

    [ContextMenu("AllOff")]
    public void AllOff()
    {
        for (int y = 0; y < Resolution; y++)
        {
            for (int x = 0; x < Resolution; x++)
            {
                blockValues[x, y] = 0;
            }
        }

        isDirty = true;
    }

    [ContextMenu("Invert")]
    public void Invert()
    {
        for (int y = 0; y < Resolution; y++)
        {
            for (int x = 0; x < Resolution; x++)
            {
                float newValue = blockValues[x, y] - 255;

                if (newValue < 0)
                {
                    newValue = -newValue;
                }

                blockValues[x, y] = newValue;
            }
        }

        isDirty = true;
    }

    [ContextMenu("Image")]
    public void Image()
    {
        blockValues = new float[16, 16] {
            { 73, 74, 75, 76, 76, 60, 59, 47, 51, 51, 65, 84 ,85, 86, 85, 86},
            { 67, 68, 68,60, 26, 31,32, 23, 23,38, 44, 78,83, 84, 84, 84 },
            { 255, 255, 255,255, 255, 255,255, 255, 255,255, 255, 255,255, 255, 255, 255 },
            { 255, 255, 255,255, 255, 255,255, 255, 255,255, 255, 255,255, 255, 255, 255 },
            { 255, 255, 255,255, 255, 255,255, 255, 255,255, 255, 255,255, 255, 255, 255 },
            { 255, 255, 255,255, 255, 255,255, 255, 255,255, 255, 255,255, 255, 255, 255 },
            { 255, 255, 255,255, 255, 255,255, 255, 255,255, 255, 255,255, 255, 255, 255 },
            { 255, 255, 255,255, 255, 255,255, 255, 255,255, 255, 255,255, 255, 255, 255 },
            { 255, 255, 255,255, 255, 255,255, 255, 255,255, 255, 255,255, 255, 255, 255 },
            { 255, 255, 255,255, 255, 255,255, 255, 255,255, 255, 255,255, 255, 255, 255 },
            { 255, 255, 255,255, 255, 255,255, 255, 255,255, 255, 255,255, 255, 255, 255 },
            { 255, 255, 255,255, 255, 255,255, 255, 255,255, 255, 255,255, 255, 255, 255 },
            { 255, 255, 255,255, 255, 255,255, 255, 255,255, 255, 255,255, 255, 255, 255 },
            { 255, 255, 255,255, 255, 255,255, 255, 255,255, 255, 255,255, 255, 255, 255 },
            { 255, 255, 255,255, 255, 255,255, 255, 255,255, 255, 255,255, 255, 255, 255 },
            { 255, 255, 255,255, 255, 255,255, 255, 255,255, 255, 255,255, 255, 255, 255 }
        };

        isDirty = true;
    }
}
