using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventControl : MonoBehaviour 
{
	//Events
	public class Event : UnityEvent
	{
		public void Listen (UnityAction action) {this.AddListener (action);}
		public void Remove (UnityAction action) {this.RemoveListener (action);}
		public void Trigger() {this.Invoke ();}
	}

	public class EventString : UnityEvent<string>
	{
		public void Listen (UnityAction<string> action) {this.AddListener(action);}
		public void Remove (UnityAction<string> action) {this.RemoveListener (action);}
		public void Trigger(string data) {this.Invoke (data);}
	}

//	public delegate 
	delegate void Delegate();
	Delegate del;
	delegate void DelegateString(string data);
	DelegateString delString;
	delegate int DelegateReturnInt();
	DelegateReturnInt delReturnInt;

//Events
	public Event e;
	public EventString eS;

//Actions
	public UnityAction action;
	public UnityAction<string> actionString;

	// Use this for initialization
	void Start () 
	{
		e = new Event ();
		eS = new EventString ();
		e.Listen(NormalEvent);
		eS.Listen(StringEvent);

		//Events need function call to invoke
		e.Trigger ();
		eS.Trigger ("EVENT RIGGERED!");

		//Delegates
		del += NormalEvent;
		delString += StringEvent;
		delReturnInt += EventReturnValue;

		//Delegates can directly call itself as a function for invocation
		del ();
		delString ("DELEGATE TRIGGERED!");
		Debug.Log ("EVENT RETURN VALUE " + delReturnInt ());

		action += NormalEvent;
		actionString += StringEvent;

		//Action can directly call itself as a function for invocation
		action ();
		actionString ("ACTION TRIGGERED!");
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void NormalEvent()
	{
		Debug.Log ("Normal Event");
	}

	void StringEvent(string data)
	{
		Debug.Log (data);
	}

	int EventReturnValue()
	{
		return 1;
	}
}
	


