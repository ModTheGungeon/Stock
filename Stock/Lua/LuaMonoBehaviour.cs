using System;
using UnityEngine;
using Eluant;
using Eluant.ObjectBinding;

namespace Stock {
    public class LuaMonoBehaviour : MonoBehaviour, IDisposable, ILuaTableBinding {
        public void Dispose() {
            EventAwake.Dispose();
            EventFixedUpdate.Dispose();
            EventLateUpdate.Dispose();
            EventOnAnimatorIK.Dispose();
            EventOnAnimatorMove.Dispose();
            EventOnApplicationFocus.Dispose();
            EventOnApplicationPause.Dispose();
            EventOnApplicationQuit.Dispose();
            EventOnAudioFilterRead.Dispose();
            EventOnBecameInvisible.Dispose();
            EventOnBecameVisible.Dispose();
            EventOnCollisionEnter.Dispose();
            EventOnCollisionEnter2D.Dispose();
            EventOnCollisionExit.Dispose();
            EventOnCollisionExit2D.Dispose();
            EventOnCollisionStay.Dispose();
            EventOnCollisionStay2D.Dispose();
            EventOnJointBreak.Dispose();
            EventOnJointBreak2D.Dispose();
            EventOnControllerColliderHit.Dispose();
            EventOnConnectedToServer.Dispose();
            EventOnDisconnectedFromServer.Dispose();
            EventOnMasterServerEvent.Dispose();
            EventOnFailedToConnect.Dispose();
            EventOnFailedToConnectToMasterServer.Dispose();
            EventOnDestroy.Dispose();
            EventOnDisable.Dispose();
            EventOnEnable.Dispose();
            EventOnDrawGizmos.Dispose();
            EventOnDrawGizmosSelected.Dispose();
            EventOnGUI.Dispose();
            EventOnMouseDown.Dispose();
            EventOnMouseDrag.Dispose();
            EventOnMouseEnter.Dispose();
            EventOnMouseExit.Dispose();
            EventOnMouseOver.Dispose();
            EventOnMouseUp.Dispose();
            EventOnMouseUpAsButton.Dispose();
            EventOnNetworkInstantiate.Dispose();
            EventOnParticleCollision.Dispose();
            EventOnParticleTrigger.Dispose();
            EventOnPlayerConnected.Dispose();
            EventOnPlayerDisconnected.Dispose();
            EventOnPostRender.Dispose();
            EventOnPreCull.Dispose();
            EventOnPreRender.Dispose();
            EventOnRenderImage.Dispose();
            EventOnRenderObject.Dispose();
            EventOnSerializeNetworkView.Dispose();
            EventOnServerInitialized.Dispose();
            EventOnTransformChildrenChanged.Dispose();
            EventOnTransformParentChanged.Dispose();
            EventOnTriggerEnter.Dispose();
            EventOnTriggerEnter2D.Dispose();
            EventOnTriggerExit.Dispose();
            EventOnTriggerExit2D.Dispose();
            EventOnTriggerStay.Dispose();
            EventOnTriggerStay2D.Dispose();
            EventOnWillRenderObject.Dispose();
            EventReset.Dispose();
            EventStart.Dispose();
            EventUpdate.Dispose();
        }

        public LuaFunction EventAwake;
        public LuaFunction EventFixedUpdate;
        public LuaFunction EventLateUpdate;
        public LuaFunction EventOnAnimatorIK;
        public LuaFunction EventOnAnimatorMove;
        public LuaFunction EventOnApplicationFocus;
        public LuaFunction EventOnApplicationPause;
        public LuaFunction EventOnApplicationQuit;
        public LuaFunction EventOnAudioFilterRead;
        public LuaFunction EventOnBecameInvisible;
        public LuaFunction EventOnBecameVisible;
        public LuaFunction EventOnCollisionEnter;
        public LuaFunction EventOnCollisionEnter2D;
        public LuaFunction EventOnCollisionExit;
        public LuaFunction EventOnCollisionExit2D;
        public LuaFunction EventOnCollisionStay;
        public LuaFunction EventOnCollisionStay2D;
        public LuaFunction EventOnJointBreak;
        public LuaFunction EventOnJointBreak2D;
        public LuaFunction EventOnControllerColliderHit;
        public LuaFunction EventOnConnectedToServer;
        public LuaFunction EventOnDisconnectedFromServer;
        public LuaFunction EventOnMasterServerEvent;
        public LuaFunction EventOnFailedToConnect;
        public LuaFunction EventOnFailedToConnectToMasterServer;
        public LuaFunction EventOnDestroy;
        public LuaFunction EventOnDisable;
        public LuaFunction EventOnEnable;
        public LuaFunction EventOnDrawGizmos;
        public LuaFunction EventOnDrawGizmosSelected;
        public LuaFunction EventOnGUI;
        public LuaFunction EventOnMouseDown;
        public LuaFunction EventOnMouseDrag;
        public LuaFunction EventOnMouseEnter;
        public LuaFunction EventOnMouseExit;
        public LuaFunction EventOnMouseOver;
        public LuaFunction EventOnMouseUp;
        public LuaFunction EventOnMouseUpAsButton;
        public LuaFunction EventOnNetworkInstantiate;
        public LuaFunction EventOnParticleCollision;
        public LuaFunction EventOnParticleTrigger;
        public LuaFunction EventOnPlayerConnected;
        public LuaFunction EventOnPlayerDisconnected;
        public LuaFunction EventOnPostRender;
        public LuaFunction EventOnPreCull;
        public LuaFunction EventOnPreRender;
        public LuaFunction EventOnRenderImage;
        public LuaFunction EventOnRenderObject;
        public LuaFunction EventOnSerializeNetworkView;
        public LuaFunction EventOnServerInitialized;
        public LuaFunction EventOnTransformChildrenChanged;
        public LuaFunction EventOnTransformParentChanged;
        public LuaFunction EventOnTriggerEnter;
        public LuaFunction EventOnTriggerEnter2D;
        public LuaFunction EventOnTriggerExit;
        public LuaFunction EventOnTriggerExit2D;
        public LuaFunction EventOnTriggerStay;
        public LuaFunction EventOnTriggerStay2D;
        // OnValidate - editor only
        public LuaFunction EventOnWillRenderObject;
        public LuaFunction EventReset;
        public LuaFunction EventStart;
        public LuaFunction EventUpdate;

        public LuaValue this[LuaRuntime runtime, LuaValue key] {
            get {
                if (!(key is LuaString)) throw new Exception("Invalid key type - must be a string");

                var key_str = key as LuaString;

                if (key_str == "Awake") return EventAwake;
                else if (key_str == "FixedUpdate") return EventFixedUpdate;
                else if (key_str == "LateUpdate") return EventLateUpdate;
                else if (key_str == "OnAnimatorIK") return EventOnAnimatorIK;
                else if (key_str == "OnAnimatorMove") return EventOnAnimatorMove;
                else if (key_str == "OnApplicationFocus") return EventOnApplicationFocus;
                else if (key_str == "OnApplicationPause") return EventOnApplicationPause;
                else if (key_str == "OnApplicationQuit") return EventOnApplicationQuit;
                else if (key_str == "OnAudioFilterRead") return EventOnAudioFilterRead;
                else if (key_str == "OnBecameInvisible") return EventOnBecameInvisible;
                else if (key_str == "OnBecameVisible") return EventOnBecameVisible;
                else if (key_str == "OnCollisionEnter") return EventOnCollisionEnter;
                else if (key_str == "OnCollisionEnter2D") return EventOnCollisionEnter2D;
                else if (key_str == "OnCollisionExit") return EventOnCollisionExit;
                else if (key_str == "OnCollisionExit2D") return EventOnCollisionExit2D;
                else if (key_str == "OnCollisionStay") return EventOnCollisionStay;
                else if (key_str == "OnCollisionStay2D") return EventOnCollisionStay2D;
                else if (key_str == "OnJointBreak") return EventOnJointBreak;
                else if (key_str == "OnJointBreak2D") return EventOnJointBreak2D;
                else if (key_str == "OnControllerColliderHit") return EventOnControllerColliderHit;
                else if (key_str == "OnConnectedToServer") return EventOnConnectedToServer;
                else if (key_str == "OnDisconnectedFromServer") return EventOnDisconnectedFromServer;
                else if (key_str == "OnMasterServerEvent") return EventOnMasterServerEvent;
                else if (key_str == "OnFailedToConnect") return EventOnFailedToConnect;
                else if (key_str == "OnFailedToConnectToMasterServer") return EventOnFailedToConnectToMasterServer;
                else if (key_str == "OnDestroy") return EventOnDestroy;
                else if (key_str == "OnDisable") return EventOnDisable;
                else if (key_str == "OnEnable") return EventOnEnable;
                else if (key_str == "OnDrawGizmos") return EventOnDrawGizmos;
                else if (key_str == "OnDrawGizmosSelected") return EventOnDrawGizmosSelected;
                else if (key_str == "OnGUI") return EventOnGUI;
                else if (key_str == "OnMouseDown") return EventOnMouseDown;
                else if (key_str == "OnMouseDrag") return EventOnMouseDrag;
                else if (key_str == "OnMouseEnter") return EventOnMouseEnter;
                else if (key_str == "OnMouseExit") return EventOnMouseExit;
                else if (key_str == "OnMouseOver") return EventOnMouseOver;
                else if (key_str == "OnMouseUp") return EventOnMouseUp;
                else if (key_str == "OnMouseUpAsButton") return EventOnMouseUpAsButton;
                else if (key_str == "OnNetworkInstantiate") return EventOnNetworkInstantiate;
                else if (key_str == "OnParticleCollision") return EventOnParticleCollision;
                else if (key_str == "OnParticleTrigger") return EventOnParticleTrigger;
                else if (key_str == "OnPlayerConnected") return EventOnPlayerConnected;
                else if (key_str == "OnPlayerDisconnected") return EventOnPlayerDisconnected;
                else if (key_str == "OnPostRender") return EventOnPostRender;
                else if (key_str == "OnPreCull") return EventOnPreCull;
                else if (key_str == "OnPreRender") return EventOnPreRender;
                else if (key_str == "OnRenderImage") return EventOnRenderImage;
                else if (key_str == "OnRenderObject") return EventOnRenderObject;
                else if (key_str == "OnSerializeNetworkView") return EventOnSerializeNetworkView;
                else if (key_str == "OnServerInitialized") return EventOnServerInitialized;
                else if (key_str == "OnTransformChildrenChanged") return EventOnTransformChildrenChanged;
                else if (key_str == "OnTransformParentChanged") return EventOnTransformParentChanged;
                else if (key_str == "OnTriggerEnter") return EventOnTriggerEnter;
                else if (key_str == "OnTriggerEnter2D") return EventOnTriggerEnter2D;
                else if (key_str == "OnTriggerExit") return EventOnTriggerExit;
                else if (key_str == "OnTriggerExit2D") return EventOnTriggerExit2D;
                else if (key_str == "OnTriggerStay") return EventOnTriggerStay;
                else if (key_str == "OnTriggerStay2D") return EventOnTriggerStay2D;
                else if (key_str == "OnWillRenderObject") return EventOnWillRenderObject;
                else if (key_str == "Reset") return EventReset;
                else if (key_str == "Start") return EventStart;
                else if (key_str == "Update") return EventUpdate;

                return LuaNil.Instance;
            }
            set {
                if (!(key is LuaString)) throw new Exception("Invalid key type - must be a string");
                if (!(value is LuaFunction)) {
                    throw new Exception("Invalid value type - must be a function");
                }

                var key_str = key as LuaString;
                var value_func = value as LuaFunction;
                value_func.DisposeAfterManagedCall = false;

                if (key_str == "Awake") { EventAwake = value_func; return; }
                else if (key_str == "FixedUpdate") { EventFixedUpdate = value_func; return; }
                else if (key_str == "LateUpdate") { EventLateUpdate = value_func; return; }
                else if (key_str == "OnAnimatorIK") { EventOnAnimatorIK = value_func; return; }
                else if (key_str == "OnAnimatorMove") { EventOnAnimatorMove = value_func; return; }
                else if (key_str == "OnApplicationFocus") { EventOnApplicationFocus = value_func; return; }
                else if (key_str == "OnApplicationPause") { EventOnApplicationPause = value_func; return; }
                else if (key_str == "OnApplicationQuit") { EventOnApplicationQuit = value_func; return; }
                else if (key_str == "OnAudioFilterRead") { EventOnAudioFilterRead = value_func; return; }
                else if (key_str == "OnBecameInvisible") { EventOnBecameInvisible = value_func; return; }
                else if (key_str == "OnBecameVisible") { EventOnBecameVisible = value_func; return; }
                else if (key_str == "OnCollisionEnter") { EventOnCollisionEnter = value_func; return; }
                else if (key_str == "OnCollisionEnter2D") { EventOnCollisionEnter2D = value_func; return; }
                else if (key_str == "OnCollisionExit") { EventOnCollisionExit = value_func; return; }
                else if (key_str == "OnCollisionExit2D") { EventOnCollisionExit2D = value_func; return; }
                else if (key_str == "OnCollisionStay") { EventOnCollisionStay = value_func; return; }
                else if (key_str == "OnCollisionStay2D") { EventOnCollisionStay2D = value_func; return; }
                else if (key_str == "OnJointBreak") { EventOnJointBreak = value_func; return; }
                else if (key_str == "OnJointBreak2D") { EventOnJointBreak2D = value_func; return; }
                else if (key_str == "OnControllerColliderHit") { EventOnControllerColliderHit = value_func; return; }
                else if (key_str == "OnConnectedToServer") { EventOnConnectedToServer = value_func; return; }
                else if (key_str == "OnDisconnectedFromServer") { EventOnDisconnectedFromServer = value_func; return; }
                else if (key_str == "OnMasterServerEvent") { EventOnMasterServerEvent = value_func; return; }
                else if (key_str == "OnFailedToConnect") { EventOnFailedToConnect = value_func; return; }
                else if (key_str == "OnFailedToConnectToMasterServer") { EventOnFailedToConnectToMasterServer = value_func; return; }
                else if (key_str == "OnDestroy") { EventOnDestroy = value_func; return; }
                else if (key_str == "OnDisable") { EventOnDisable = value_func; return; }
                else if (key_str == "OnEnable") { EventOnEnable = value_func; return; }
                else if (key_str == "OnDrawGizmos") { EventOnDrawGizmos = value_func; return; }
                else if (key_str == "OnDrawGizmosSelected") { EventOnDrawGizmosSelected = value_func; return; }
                else if (key_str == "OnGUI") { EventOnGUI = value_func; return; }
                else if (key_str == "OnMouseDown") { EventOnMouseDown = value_func; return; }
                else if (key_str == "OnMouseDrag") { EventOnMouseDrag = value_func; return; }
                else if (key_str == "OnMouseEnter") { EventOnMouseEnter = value_func; return; }
                else if (key_str == "OnMouseExit") { EventOnMouseExit = value_func; return; }
                else if (key_str == "OnMouseOver") { EventOnMouseOver = value_func; return; }
                else if (key_str == "OnMouseUp") { EventOnMouseUp = value_func; return; }
                else if (key_str == "OnMouseUpAsButton") { EventOnMouseUpAsButton = value_func; return; }
                else if (key_str == "OnNetworkInstantiate") { EventOnNetworkInstantiate = value_func; return; }
                else if (key_str == "OnParticleCollision") { EventOnParticleCollision = value_func; return; }
                else if (key_str == "OnParticleTrigger") { EventOnParticleTrigger = value_func; return; }
                else if (key_str == "OnPlayerConnected") { EventOnPlayerConnected = value_func; return; }
                else if (key_str == "OnPlayerDisconnected") { EventOnPlayerDisconnected = value_func; return; }
                else if (key_str == "OnPostRender") { EventOnPostRender = value_func; return; }
                else if (key_str == "OnPreCull") { EventOnPreCull = value_func; return; }
                else if (key_str == "OnPreRender") { EventOnPreRender = value_func; return; }
                else if (key_str == "OnRenderImage") { EventOnRenderImage = value_func; return; }
                else if (key_str == "OnRenderObject") { EventOnRenderObject = value_func; return; }
                else if (key_str == "OnSerializeNetworkView") { EventOnSerializeNetworkView = value_func; return; }
                else if (key_str == "OnServerInitialized") { EventOnServerInitialized = value_func; return; }
                else if (key_str == "OnTransformChildrenChanged") { EventOnTransformChildrenChanged = value_func; return; }
                else if (key_str == "OnTransformParentChanged") { EventOnTransformParentChanged = value_func; return; }
                else if (key_str == "OnTriggerEnter") { EventOnTriggerEnter = value_func; return; }
                else if (key_str == "OnTriggerEnter2D") { EventOnTriggerEnter2D = value_func; return; }
                else if (key_str == "OnTriggerExit") { EventOnTriggerExit = value_func; return; }
                else if (key_str == "OnTriggerExit2D") { EventOnTriggerExit2D = value_func; return; }
                else if (key_str == "OnTriggerStay") { EventOnTriggerStay = value_func; return; }
                else if (key_str == "OnTriggerStay2D") { EventOnTriggerStay2D = value_func; return; }
                else if (key_str == "OnWillRenderObject") { EventOnWillRenderObject = value_func; return; }
                else if (key_str == "Reset") { EventReset = value_func; return; }
                else if (key_str == "Start") { EventStart = value_func; return; }
                else if (key_str == "Update") { EventUpdate = value_func; return; }

                throw new Exception($"Invalid event name: '{key_str}'");
            }
        }

        public void Awake() {
            if (EventAwake != null) EventAwake.Call();
        }
        public void FixedUpdate() {
            if (EventFixedUpdate != null) EventFixedUpdate.Call();
        }
        public void LateUpdate() {
            if (EventLateUpdate != null) EventLateUpdate.Call();
        }
        public void OnAnimatorIK() {
            if (EventOnAnimatorIK != null) EventOnAnimatorIK.Call();
        }
        public void OnAnimatorMove() {
            if (EventOnAnimatorMove != null) EventOnAnimatorMove.Call();
        }
        public void OnApplicationFocus() {
            if (EventOnApplicationFocus != null) EventOnApplicationFocus.Call();
        }
        public void OnApplicationPause() {
            if (EventOnApplicationPause != null) EventOnApplicationPause.Call();
        }
        public void OnApplicationQuit() {
            if (EventOnApplicationQuit != null) EventOnApplicationQuit.Call();
        }
        public void OnAudioFilterRead() {
            if (EventOnAudioFilterRead != null) EventOnAudioFilterRead.Call();
        }
        public void OnBecameInvisible() {
            if (EventOnBecameInvisible != null) EventOnBecameInvisible.Call();
        }
        public void OnBecameVisible() {
            if (EventOnBecameVisible != null) EventOnBecameVisible.Call();
        }
        public void OnCollisionEnter() {
            if (EventOnCollisionEnter != null) EventOnCollisionEnter.Call();
        }
        public void OnCollisionEnter2D() {
            if (EventOnCollisionEnter2D != null) EventOnCollisionEnter2D.Call();
        }
        public void OnCollisionExit() {
            if (EventOnCollisionExit != null) EventOnCollisionExit.Call();
        }
        public void OnCollisionExit2D() {
            if (EventOnCollisionExit2D != null) EventOnCollisionExit2D.Call();
        }
        public void OnCollisionStay() {
            if (EventOnCollisionStay != null) EventOnCollisionStay.Call();
        }
        public void OnCollisionStay2D() {
            if (EventOnCollisionStay2D != null) EventOnCollisionStay2D.Call();
        }
        public void OnJointBreak() {
            if (EventOnJointBreak != null) EventOnJointBreak.Call();
        }
        public void OnJointBreak2D() {
            if (EventOnJointBreak2D != null) EventOnJointBreak2D.Call();
        }
        public void OnControllerColliderHit() {
            if (EventOnControllerColliderHit != null) EventOnControllerColliderHit.Call();
        }
        public void OnConnectedToServer() {
            if (EventOnConnectedToServer != null) EventOnConnectedToServer.Call();
        }
        public void OnDisconnectedFromServer() {
            if (EventOnDisconnectedFromServer != null) EventOnDisconnectedFromServer.Call();
        }
        public void OnMasterServerEvent() {
            if (EventOnMasterServerEvent != null) EventOnMasterServerEvent.Call();
        }
        public void OnFailedToConnect() {
            if (EventOnFailedToConnect != null) EventOnFailedToConnect.Call();
        }
        public void OnFailedToConnectToMasterServer() {
            if (EventOnFailedToConnectToMasterServer != null) EventOnFailedToConnectToMasterServer.Call();
        }
        public void OnDestroy() {
            if (EventOnDestroy != null) EventOnDestroy.Call();
        }
        public void OnDisable() {
            if (EventOnDisable != null) EventOnDisable.Call();
        }
        public void OnEnable() {
            if (EventOnEnable != null) EventOnEnable.Call();
        }
        public void OnDrawGizmos() {
            if (EventOnDrawGizmos != null) EventOnDrawGizmos.Call();
        }
        public void OnDrawGizmosSelected() {
            if (EventOnDrawGizmosSelected != null) EventOnDrawGizmosSelected.Call();
        }
        public void OnGUI() {
            if (EventOnGUI != null) EventOnGUI.Call();
        }
        public void OnMouseDown() {
            if (EventOnMouseDown != null) EventOnMouseDown.Call();
        }
        public void OnMouseDrag() {
            if (EventOnMouseDrag != null) EventOnMouseDrag.Call();
        }
        public void OnMouseEnter() {
            if (EventOnMouseEnter != null) EventOnMouseEnter.Call();
        }
        public void OnMouseExit() {
            if (EventOnMouseExit != null) EventOnMouseExit.Call();
        }
        public void OnMouseOver() {
            if (EventOnMouseOver != null) EventOnMouseOver.Call();
        }
        public void OnMouseUp() {
            if (EventOnMouseUp != null) EventOnMouseUp.Call();
        }
        public void OnMouseUpAsButton() {
            if (EventOnMouseUpAsButton != null) EventOnMouseUpAsButton.Call();
        }
        public void OnNetworkInstantiate() {
            if (EventOnNetworkInstantiate != null) EventOnNetworkInstantiate.Call();
        }
        public void OnParticleCollision() {
            if (EventOnParticleCollision != null) EventOnParticleCollision.Call();
        }
        public void OnParticleTrigger() {
            if (EventOnParticleTrigger != null) EventOnParticleTrigger.Call();
        }
        public void OnPlayerConnected() {
            if (EventOnPlayerConnected != null) EventOnPlayerConnected.Call();
        }
        public void OnPlayerDisconnected() {
            if (EventOnPlayerDisconnected != null) EventOnPlayerDisconnected.Call();
        }
        public void OnPostRender() {
            if (EventOnPostRender != null) EventOnPostRender.Call();
        }
        public void OnPreCull() {
            if (EventOnPreCull != null) EventOnPreCull.Call();
        }
        public void OnPreRender() {
            if (EventOnPreRender != null) EventOnPreRender.Call();
        }
        public void OnRenderImage() {
            if (EventOnRenderImage != null) EventOnRenderImage.Call();
        }
        public void OnRenderObject() {
            if (EventOnRenderObject != null) EventOnRenderObject.Call();
        }
        public void OnSerializeNetworkView() {
            if (EventOnSerializeNetworkView != null) EventOnSerializeNetworkView.Call();
        }
        public void OnServerInitialized() {
            if (EventOnServerInitialized != null) EventOnServerInitialized.Call();
        }
        public void OnTransformChildrenChanged() {
            if (EventOnTransformChildrenChanged != null) EventOnTransformChildrenChanged.Call();
        }
        public void OnTransformParentChanged() {
            if (EventOnTransformParentChanged != null) EventOnTransformParentChanged.Call();
        }
        public void OnTriggerEnter() {
            if (EventOnTriggerEnter != null) EventOnTriggerEnter.Call();
        }
        public void OnTriggerEnter2D() {
            if (EventOnTriggerEnter2D != null) EventOnTriggerEnter2D.Call();
        }
        public void OnTriggerExit() {
            if (EventOnTriggerExit != null) EventOnTriggerExit.Call();
        }
        public void OnTriggerExit2D() {
            if (EventOnTriggerExit2D != null) EventOnTriggerExit2D.Call();
        }
        public void OnTriggerStay() {
            if (EventOnTriggerStay != null) EventOnTriggerStay.Call();
        }
        public void OnTriggerStay2D() {
            if (EventOnTriggerStay2D != null) EventOnTriggerStay2D.Call();
        }
        public void OnWillRenderObject() {
            if (EventOnWillRenderObject != null) EventOnWillRenderObject.Call();
        }
        public void Reset() {
            if (EventReset != null) EventReset.Call();
        }
        public void Start() {
            if (EventStart != null) EventStart.Call();
        }
        public void Update() {
            if (EventUpdate != null) EventUpdate.Call();
        }
    }
}
