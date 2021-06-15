using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DirtySurfaceBehaviour : MonoBehaviour
{
    public int Radius;
    public Slider slider;
    public GameObject Sponge;
    private Texture2D mainTexture;
    public UnityEvent<float> SurfaceEmpty;

    public NavigationScript navigationScript;
    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        var renderer = gameObject.GetComponent<MeshRenderer>();
        startTime = Time.time;
        mainTexture = new Texture2D(renderer.material.mainTexture.width, renderer.material.mainTexture.height, TextureFormat.RGBA32, false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var mouse = Input.mousePosition;
            var ray = Camera.main.ScreenPointToRay(mouse);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit))
            {
                Sponge.transform.position = hit.point;
                Debug.Log("Spawned Sponge at " + hit.point.x + ", " + hit.point.y);
                Texture2D texture2D = new Texture2D(mainTexture.width, mainTexture.height, TextureFormat.RGBA32, false);
                var X = Mathf.CeilToInt(hit.textureCoord.x * mainTexture.width);
                var Y = Mathf.CeilToInt(hit.textureCoord.y * mainTexture.width);
                Debug.Log(texture2D.GetPixel(X,Y));
                var renderer = gameObject.GetComponent<MeshRenderer>();
                ColorTexture(X, Y);
                var returnTex = mainTexture;
                returnTex.Apply();
                renderer.material.mainTexture = returnTex;
            }
            else
            {
                Sponge.transform.position = new Vector3(3000, 3000, 3000);
            }
        } else
        {
            Sponge.transform.position = new Vector3(3000, 3000, 3000);
        }

        slider.value = Cleanliness();
        if (Cleanliness() == 1)
        {
            SurfaceEmpty.Invoke(Time.time - startTime);
        }
    }

    void ColorTexture(int X, int Y)
    {
        Debug.Log("Cleaned from " + X + ", " + Y);
        var points = new List<Point>();

        var radius = Radius;
        Point point = new Point(X, Y);
        for (int x = -radius; x <= radius; ++x) {
            for (int y = -radius; y <= radius; ++y)
            {
                if (x * x + y * y <= radius * radius)
                {
                    points.Add(new Point(x + point.X, y + point.Y));
                }
            }
        }

        foreach (Point p in points)
        {
            mainTexture.SetPixel(p.X,p.Y, UnityEngine.Color.clear);
        }
    }

    float Cleanliness()
    {
        Texture2D texture2D = mainTexture;
        float clean = 0;
        float max = 0;

        for (int x = texture2D.width; x >= 0; x--)
        {
            for (int y = texture2D.height; y>=0; y--)
            {
                max++;
                if (texture2D.GetPixel(x, y).Equals(UnityEngine.Color.clear))
                {
                    clean++;
                }
            }
        }
        if(clean==max){
            //We're done
             navigationScript.SwitchScreen();
             Game.currentGame.PlayerData.AlterMoney(9);
             Game.currentGame.PlayerData.AlterEnergy(-4);
             Game.currentGame.PlayerData.AlterHappiness(-2);
        }
        return clean / max;
    }
}
