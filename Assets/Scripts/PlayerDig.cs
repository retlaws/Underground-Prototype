using UnityEngine;
using Digger.Modules.Core.Sources;
using Digger.Modules.Runtime.Sources;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerDig : MonoBehaviour
{
    [SerializeField] DiggingTool currentTool;
    [SerializeField] bool debugDigging = false;
    [SerializeField] bool debugAllowContinuousDigging = false;

    DiggerNavMeshRuntime navMeshRuntime;
    DiggerMasterRuntime diggerMasterRuntime;

    int navmeshUpdateCounter = 0;

    private void Start()
    {
        navMeshRuntime = FindObjectOfType<DiggerNavMeshRuntime>();
        navMeshRuntime.CollectNavMeshSources();
        navMeshRuntime.UpdateNavMeshAsync(); // this is to reset the navmesh
        diggerMasterRuntime = FindObjectOfType<DiggerMasterRuntime>();
    }

    public void OnFire(InputAction.CallbackContext context) // called when left mouse button is clicked. 
    {
        print("on fire performed");
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out var hit, 2000f))
        {
            if (debugDigging == true)
            {
                diggerMasterRuntime.Modify(hit.point, BrushType.Sphere, ActionType.Dig, 2, 0.5f, 4);
            }
            else
            {
                diggerMasterRuntime.Modify(hit.point, currentTool.brushType, ActionType.Dig, 2, currentTool.opacity, currentTool.radius); // TODO can insert a texture reference here which will be useful
            }
            UpdateNavmesh();
        }
    }

    public void OnFireHoldStart(InputAction.CallbackContext context)
    {
        StartCoroutine(ContinuousDigging());
    }

    public void OnFireHoldPerformed(InputAction.CallbackContext context)
    {
        StopAllCoroutines();
    }

    private IEnumerator ContinuousDigging()
    {
        float num = 0;

        while (true)
        {
            num += currentTool.diggingSpeed * 10 * Time.deltaTime;

            if (num > 10) // this is an arbitrary number
            {
                num = 0;
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out var hit, 2000f))
                {
                    diggerMasterRuntime.Modify(hit.point, currentTool.brushType, ActionType.Dig, 2, currentTool.opacity, currentTool.radius); // TODO can insert a texture reference here which will be useful
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }


    private void UpdateNavmesh()
    {
        navmeshUpdateCounter++; 

        if(navmeshUpdateCounter > 5)
        {
            navmeshUpdateCounter = 0;
            navMeshRuntime.UpdateNavMeshAsync();
        }
    }

}
