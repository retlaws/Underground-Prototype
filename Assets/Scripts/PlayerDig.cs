using UnityEngine;
using Digger.Modules.Core.Sources;
using Digger.Modules.Runtime.Sources;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerDig : MonoBehaviour
{
    [SerializeField] Tool currentTool;

    DiggerNavMeshRuntime navMeshRuntime;
    DiggerMasterRuntime diggerMasterRuntime;

    int navmeshUpdateCounter = 0;
    Animator animator;

    private void Start()
    {
        navMeshRuntime = FindObjectOfType<DiggerNavMeshRuntime>();
        navMeshRuntime.CollectNavMeshSources();
        navMeshRuntime.UpdateNavMeshAsync(); // this is to reset the navmesh
        diggerMasterRuntime = FindObjectOfType<DiggerMasterRuntime>();
        animator = GetComponent<Animator>();
    }

    //private void Update()
    //{
    //    Vector3 endPoint = new Vector3(Camera.main.transform.position.x - currentTool.toolConfig.range, Camera.main.transform.position.y, Camera.main.transform.position.z );
    //    Debug.DrawLine(Camera.main.transform.position, endPoint, Color.blue, Time.deltaTime);
    //}

    public void OnFire(InputAction.CallbackContext context) // called when left mouse button is clicked. 
    {
        Dig();
        UpdateNavmesh();
    }

    public void SetTool(Tool newTool)
    {
        currentTool = newTool;
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
            num += currentTool.toolConfig.diggingSpeed * 10 * Time.deltaTime;

            if (num > 10) // this is an arbitrary number
            {
                num = 0;
                Dig();

            }
            yield return new WaitForEndOfFrame();
        }
    }


    private void Dig()
    {
        if(Physics.Raycast(currentTool.startOfDiggingRaycast.position, currentTool.startOfDiggingRaycast.forward, out var hit, currentTool.toolConfig.range))
        {
            diggerMasterRuntime.Modify(hit.point, currentTool.toolConfig.brushType, ActionType.Dig, 2, currentTool.toolConfig.opacity, currentTool.toolConfig.radius); // TODO can insert a texture reference here which will be useful
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Pickaxe"))
            {
                animator.Play("Pickaxe");
            }
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
