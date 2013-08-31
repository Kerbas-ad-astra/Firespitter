﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class FSwingletRangeAdjustment : PartModule
{
    private float defaultRange;
    ControlSurface winglet = new ControlSurface();

    [KSPField]
    public float stepAngle = 5f;
    [KSPField]
    public float lockModifier = 0.001f; //remove?
    [KSPField]
    public float maxRange = 60f;

    [KSPField(guiActive = true, guiName = "Control range", isPersistant = true)]
    public float currentControlRange = 16;
    private bool currentControlRangeSet = false;
    private bool locked = false;

    [KSPEvent(name = "decreaseRange", active = true, guiActive = true, guiName = "Decrease control range")]
    public void decreaseRangeEvent()
    {
        alterRange(-stepAngle);
    }
    [KSPAction("Decrease Control Range")]
    public void decreaseRangeAction(KSPActionParam param)
    {
        alterRange(-stepAngle);
    }

    [KSPEvent(name = "increaseRange", active = true, guiActive = true, guiName = "Increase control range")]
    public void increaseRangeEvent()
    {
        alterRange(stepAngle);
    }
    [KSPAction("Increase Control Range")]
    public void increaseRangeAction(KSPActionParam param)
    {
        alterRange(stepAngle);
    }

    private void alterRange(float amount)
    {
        currentControlRange += amount;
        if (currentControlRange < 0) currentControlRange = 0;
        if (currentControlRange > maxRange) currentControlRange = maxRange;
        winglet.ctrlSurfaceRange = currentControlRange;
    }

    [KSPEvent(name = "lockRange", active = true, guiActive = true, guiName = "Toggle lock")]
    public void lockRangeEvent()
    {
        lockRange();
    }
    [KSPAction("Toggle lock")]
    public void lockRangeAction(KSPActionParam param)
    {
        lockRange();
    }

    private void lockRange()
    {
        if (!locked)
        {
            winglet.ctrlSurfaceRange = 0;
            locked = true;
        }
        else
        {
            winglet.ctrlSurfaceRange = currentControlRange;
            locked = false;
        }
    }

    public override void OnStart(PartModule.StartState state)
    {
        base.OnStart(state);
        //winglet = part.Modules.OfType<ControlSurface>().FirstOrDefault();
        winglet = part as ControlSurface;
        defaultRange = winglet.ctrlSurfaceRange;
        if (!currentControlRangeSet)
        {
            currentControlRange = defaultRange;
            currentControlRangeSet = true;
        }
    }
}

