using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class ModelDrone : ModelNodeUsingEnemy
{
    public float scanProgress;
    public float scanProgressUnit;
    public float runSpeed;
    public ControllerWrapper scanController;
    public SoundClip droneHelix;
    public SoundClip droneScan;
    public TextMeshPro tmp;

    protected override void Start()
    {
        scanController = scanController.Clone();
        (scanController as IController).AssignModel(this);
        scanController.SetController();
        base.Start();

        EventManager.SubscribeToEvent("Alert", AlertBehavior);
        EventManager.SubscribeToEvent("AlertStop", NormalBehavior);
        EventManager.SubscribeToEvent("UnsubEnter", EnterBehavior);

        StartCoroutine(FoVConeIitialization());
    }

    protected override void Update()
    {
        base.Update();
        if (IsInSight(target, alertRange))
        {
            if (controller != scanController)
            {
                controller = scanController;
                sm.Play(droneScan);
            }
            else
                sm.Play(droneHelix);

            tmp.gameObject.SetActive(true);

            if (scanProgress < 100)
            {
                scanProgress += scanProgressUnit * Time.deltaTime;
            }
            else scanProgress = 100;
        }
        else
        {
            if (scanProgress > 0)
                scanProgress -= scanProgressUnit * Time.deltaTime;
            else scanProgress = 0;
        }

        tmp.text = "Scanning: " + Mathf.RoundToInt(scanProgress) + " %";
        tmp.gameObject.transform.forward = (Camera.main.transform.position - tmp.transform.position).normalized * -1;
        if (scanProgress == 100)
        {
            EventManager.TriggerEvent("Alert");
            controller = alertController;
            tmp.gameObject.SetActive(false);
        }

        if (scanProgress == 0)
        {
            if (controller != standardController)
            {
                sm.Stop(droneHelix);
                sm.Stop(droneScan);
                controller = standardController;
                GetComponent<NavMeshAgent>().isStopped = false;
                (controller as PatrolAI).SetTarget();
            }
            tmp.gameObject.SetActive(false);
        }
    }

    void AlertBehavior()
    {
        if (controller is ChaseAI) return;
        if (Vector3.Distance(transform.position, target.transform.position) > _alertDistance) return;
        lastSight = target.transform.position;
        currentSpeed = runSpeed;
        controller = alertController;
        if (controller.myController == null)
            controller.SetController();
    }

    void NormalBehavior()
    {
        if (this == null) return;
        currentSpeed = standardSpeed;
        controller = standardController;

        (controller as PatrolAI).SetTarget();
    }

    void EnterBehavior()
    {
        EventManager.UnsubscribeToEvent("Alert", AlertBehavior);
        EventManager.UnsubscribeToEvent("AlertStop", NormalBehavior);
        EventManager.UnsubscribeToEvent("UnsubEnter", EnterBehavior);
    }
}