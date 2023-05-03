/*
 * 别的转成它 是 隐式
 * 它转成别的 是 显式
 */

namespace Libs;

public static partial class MaxHelper
{

    /// <summary>
    /// Sequential version
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="values"></param>
    /// <returns></returns>
    public static TValue Max<TValue>(params TValue[] values)
        where TValue : IComparable, IComparable<TValue>
    {
        if (values.Length==1)
            return values[0]; 

        TValue result = values[0];
        for (int i = 0; i < values.Length; i++)
        {
            if (values[i].CompareTo(result) == 1)
                result = values[i];
        }
        return result;
    }


    public static (Index, TValue) LocateMax<TValue>(this TValue[] values)
        where TValue : IComparable, IComparable<TValue>
    {
        TValue result = values[0];
        int resultIndex = 0;
        for (int i = 0; i < values.Length; i++)
        {
            if (values[i].CompareTo(result) == 1)   
            {
                result = values[i];
                resultIndex = i;
            }
        }
        return (new Index(resultIndex), result);
    }


    public static TValue Max<TValue>(this TValue[] values, int start, int length)
        where TValue : IComparable, IComparable<TValue>
    {
        TValue result = values[start];
        for (int i = start; i < start + length; i++)
        {
            if (values[i].CompareTo(result) == 1)
                result = values[i];
        }

        return result;
    }


    /// <summary>
    /// Returns the maximum value and its index in a sequential sequence of values.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="values"></param>
    /// <param name="start"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static (Index, TValue) LocateMax<TValue>(this TValue[] values, int start, int length)
        where TValue : IComparable, IComparable<TValue>
    {
        TValue result = values[start];
        int resultIndex = 0;
        for (int i = start; i < start + length; i++)
        {
            if (values[i].CompareTo(result) == 1)
            {
                result = values[i];
                resultIndex = i;
            }
        }

        return (new Index(resultIndex), result);
    }





}
