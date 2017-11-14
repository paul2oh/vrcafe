using UnityEngine;
using System.Collections;
using System;

public class NoloVR_OtherPlayform : NoloVR_Playform {
    public override bool ConnectDevice()
    {
        Debug.Log("NoloVR_OtherPlayform ConnectDevice");
        return true;
    }

    public override void DisconnectDevice()
    {
        Debug.Log("NoloVR_OtherPlayform DisconnectDevice");
    }

    public override void DisConnectedCallBack()
    {
        Debug.Log("NoloVR_OtherPlayform DisConnectedCallBack");
    }

    public override bool InitDevice()
    {
        Debug.Log("NoloVR_OtherPlayform InitDevice");
        return true;
    }

    public override void ReconnectDeviceCallBack()
    {
        Debug.Log("NoloVR_OtherPlayform ReconnectDeviceCallBack");
    }

    public override void TriggerHapticPulse(int deviceIndex, int intensity)
    {
        Debug.Log("NoloVR_OtherPlayform TriggerHapticPulse");
    }

}
