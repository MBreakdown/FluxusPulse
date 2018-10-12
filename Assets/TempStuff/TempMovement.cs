using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Temp
{
	public class TempMovement : MonoBehaviour
	{
		void Start()
		{
			// (disable warnings for unused variables)
#pragma warning disable 0219, 0168

			// declaring variables
			int myInt = 10 + 3;
			int otherInt = Mathf.Min(4, 5); // otherInt is now 4
			float myFloat = 3.0f;
			var myVariant = 6; // auto in C++

			// unassigned variable cannot be used.
			// in C++ they can be used, but their value is indeterminate.
			int x;
			//Debug.Log(x);

#pragma warning restore 0219, 0168

			// classes are Reference types.
			// structs, ints, floats, etc. are Value types.

			MyStruct what; // C++ would initialize the MyStruct, but C# does not.
			what = new MyStruct(); // initialize the MyStruct in the variable 'what'.

			//what.x = 5;

			// C++:
			// MyClass* who = new MyClass()
			// ...
			// delete who;

			// C# has automatic memory management, and a 'garbage collector', which
			// deletes the object on the heap if it is no longer referenced.
			MyClass who = new MyClass();

			//who.x = 10;

			//Debug.Log(what.x); // prints 0
			//Debug.Log(who.x); // prints 0

			// Assigning structs
			MyStruct other = what; // copies the VALUE of what into other

			other.x = 5;

			//Debug.Log(what.x); // prints 0
			//Debug.Log(other.x); // prints 5

			// Assigning classes
			MyClass friend = who; // copies the REFERENCE to who on the heap

			friend.x = 6;
			//Debug.Log(who.x); // prints 6
			//Debug.Log(friend.x); // prints 6

			friend = new MyClass();
			friend.x = 4;

			//Debug.Log(who.x); // prints 6
			//Debug.Log(friend.x); // prints 4

			// Functions!
			MyFunc(6); // calls  void MyFunc(int x)
			MyFunc(); // calls  int MyFunc()
					  // do this...
			MyFunc(5.0f); // calls void MyFunc(float x)

			// store the return value of MyFunc()...


			int Thisint = MyFunc(); // Thisint = 5
			Debug.Log(MyFunc());
			Debug.Log(Thisint);


			// Generics!
			//Debug.Log(Choose(true, 5, 4));
			//Debug.Log(Choose(false, new MyClass(), new MyClass()));
		}

		int MyFunc()
		{
			return 5;
		}

		void MyFunc(float x)
		{
			//Debug.Log(x);
		}

		// Functions!
		void MyFunc(int x)
		{
			//Debug.Log(x);
			return; // optional
		}

		int MyFunc(bool condition)
		{
			if (condition)
			{
				return 3;
			}

			return 4;
		}

		// C++:
		// template<class T>
		// T Choose(bool x, T a, T b) { return x ? a : b; }
		T Choose<T>(bool x, T a, T b) { return x ? a : b; }
	}

	struct MyStruct
	{
		public int x; // fields are 0 or null by default
	}

	class MyClass
	{
		// constructor
		public MyClass()
		{
			x = 5;
		}

		// function that takes an int as a parameter and returns an int.
		//     return type   name   parameter  body
		public float         MyFunc(int arg  ) { return (float)arg; }

		public int x; // 0 by default
					  // x is a boxed int value
	}

	// C++:
	/*class MyClass
	{
	public:
		MyClass() { x = 5; } // constructor (no return value, same name as class)

	private:
		int x;
	};*/
}
