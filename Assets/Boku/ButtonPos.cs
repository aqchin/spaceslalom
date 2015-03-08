using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using Leap;

public class ButtonPos : MonoBehaviour {

	public UnityEngine.UI.Button play;
	public UnityEngine.UI.Button load;
	public UnityEngine.UI.Button edit;
	int i = 0;
	Button button;

	Controller ctrl;
	Frame frm;

	void Start () {
		play.transform.Translate (UnityEngine.Screen.width/2 - UnityEngine.Screen.width * 0.15f, -UnityEngine.Screen.height/2 + UnityEngine.Screen.height * 0.2f, 0);
		load.transform.Translate (UnityEngine.Screen.width/2 - UnityEngine.Screen.width * 0.15f, -UnityEngine.Screen.height/2 + UnityEngine.Screen.height * 0.2f, 0);
		edit.transform.Translate (UnityEngine.Screen.width/2 - UnityEngine.Screen.width * 0.15f, -UnityEngine.Screen.height/2 + UnityEngine.Screen.height * 0.2f, 0);
		//GlobalShit.ba = new List<Button> ();
		GlobalShit.ba.Add (play);
		GlobalShit.ba.Add (load);
		GlobalShit.ba.Add (edit);
		button = GlobalShit.ba [0];
		ctrl = new Controller ();
		ctrl.EnableGesture(Gesture.GestureType.TYPE_CIRCLE);
		ctrl.EnableGesture(Gesture.GestureType.TYPE_KEY_TAP);
		ctrl.EnableGesture(Gesture.GestureType.TYPE_SCREEN_TAP);
		ctrl.EnableGesture(Gesture.GestureType.TYPE_SWIPE);
	}
	
	// Update is called once per frame
	void Update () {
		var pointer = new PointerEventData (EventSystem.current);

		if (Input.GetKeyDown (KeyCode.E)) {
			ExecuteEvents.Execute (button.gameObject, pointer, ExecuteEvents.pointerExitHandler);
			if(i >= GlobalShit.ba.Count - 1) i = 0;
			else i++;
			print (GlobalShit.ba.Count + " index:" + i);
		}
		if (Input.GetKeyDown (KeyCode.Q)) {
			ExecuteEvents.Execute (button.gameObject, pointer, ExecuteEvents.pointerExitHandler);
			if(i <= 0) i = GlobalShit.ba.Count - 1;
			else i--;
			print (GlobalShit.ba.Count + " index:" + i);
		}
		button = GlobalShit.ba [i];
		ExecuteEvents.Execute (button.gameObject, pointer, ExecuteEvents.pointerEnterHandler);
		//if (Input.GetKeyDown (KeyCode.H)) {
		//	ExecuteEvents.Execute (button.gameObject, pointer, ExecuteEvents.pointerEnterHandler);
		//}
		if (Input.GetKeyDown (KeyCode.S)) {
			ExecuteEvents.Execute (button.gameObject, pointer, ExecuteEvents.submitHandler);
		}
		if (Input.GetKeyDown (KeyCode.P)) {
			ExecuteEvents.Execute (button.gameObject, pointer, ExecuteEvents.pointerDownHandler);
		}
		if (Input.GetKeyUp (KeyCode.P)) {
			ExecuteEvents.Execute (button.gameObject, pointer, ExecuteEvents.pointerUpHandler);
		}

		frm = ctrl.Frame();
		for (int g = 0; g < frm.Gestures().Count; g++) {
			switch (frm.Gestures () [g].Type) {
			case Gesture.GestureType.TYPE_CIRCLE:
				//Handle circle gestures
				break;
			case Gesture.GestureType.TYPE_KEY_TAP:
				ExecuteEvents.Execute (button.gameObject, pointer, ExecuteEvents.pointerExitHandler);
				if (i >= GlobalShit.ba.Count - 1)
					i = 0;
				else
					i++;
				print (GlobalShit.ba.Count + " index:" + i);
				//Handle key tap gestures
				break;
			case Gesture.GestureType.TYPE_SCREEN_TAP:
				ExecuteEvents.Execute (button.gameObject, pointer, ExecuteEvents.pointerExitHandler);
				if (i <= 0)
					i = GlobalShit.ba.Count - 1;
				else
					i--;
				print (GlobalShit.ba.Count + " index:" + i);
				//Handle screen tap gestures
				break;
			case Gesture.GestureType.TYPE_SWIPE:
				ExecuteEvents.Execute (button.gameObject, pointer, ExecuteEvents.submitHandler);
				//Handle swipe gestures
				break;
			default:
				//Handle unrecognized gestures
				break;
			}
		}
	}
}
