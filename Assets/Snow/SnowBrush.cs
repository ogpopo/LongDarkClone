using System;
using UnityEngine;

public class SnowBrush : MonoBehaviour
{
    public CustomRenderTexture SnowHeightMap;
    public Material HeightMapUpdate;

    public GameObject[] Legs;
    public GameObject[] CornersLags;
    
    private Camera mainCamera;
    private int legsIndex;

    private static readonly int DrawPosition = Shader.PropertyToID("_DrawPosition");
    private static readonly int DrawAngle = Shader.PropertyToID("_DrawAngle");
   
    void Start()
    {
        SnowHeightMap.Initialize();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        DrawWithMouse();
        //DrawWithLegs();
    }

    private void DrawWithMouse()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector2 hitTextureCoord = hit.textureCoord;

                HeightMapUpdate.SetVector(DrawPosition, hitTextureCoord);
                HeightMapUpdate.SetFloat(DrawAngle, 180 * Mathf.Deg2Rad);
            }
        }
    }
    
    private void DrawWithLegs()
    {
        var leg = Legs[legsIndex++ % Legs.Length];
        var corners = CornersLags[legsIndex++ % CornersLags.Length];
        
        Ray ray = new Ray(leg.transform.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, .249f))
        {
            Vector2 hitTextureCoord = hit.textureCoord;
            float angle = 180 + corners.transform.rotation.eulerAngles.y;

            HeightMapUpdate.SetVector(DrawPosition, hitTextureCoord);
            HeightMapUpdate.SetFloat(DrawAngle, angle * Mathf.Deg2Rad);
        }
    }
}
