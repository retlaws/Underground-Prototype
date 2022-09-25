using UnityEngine;
using Digger.Modules.Core.Sources;

[CreateAssetMenu(fileName = "DiggingTool", menuName = "Digging Tool/Make New Digging Tool", order = 0)]
public class DiggingTool : ScriptableObject
{
    [Header("Attributes")]
    [Range(0.5f, 20f)]
    public float radius;

    [Range(0,1)]
    public float opacity;  

    [Range(-0.5f, 0.5f)]
    public float depth;

    [Range(1, 10)]
    public float diggingSpeed; 

    public BrushType brushType; 



}
