using System.Linq.Expressions;

namespace GainsTracker.Data.Shared;

/// <summary>
/// Can be used to give a query for specific fields on an object as a parameter to a method. 
/// </summary>
/// <typeparam name="T">Class to query the properties on</typeparam>
public delegate Expression<Func<T, object>> IncludeProperty<T>();
