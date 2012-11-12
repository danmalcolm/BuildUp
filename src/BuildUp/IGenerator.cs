﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace BuildUp
{
	/// <summary>
	/// Generates a sequence of objects 
	/// </summary>
	public interface IGenerator
	{
		IEnumerable Build();
	}

	/// <summary>
	/// Generates a sequence of objects 
	/// </summary>
	/// <typeparam name="TObject"></typeparam>
	public interface IGenerator<out TObject> : IGenerator
	{
		/// <summary>
		/// Creates the objects
		/// </summary>
		/// <returns></returns>
		new IEnumerable<TObject> Build();
	}
}