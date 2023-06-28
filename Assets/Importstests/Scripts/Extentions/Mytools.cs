using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using System;

namespace tools
{

    public static class MyTools
    {
        // patricio malvasio maddalena (extensiones)
        public static float SumarNum(this float A, float B) // sumar floats
        {
            float result = A + B;

            return result;
        }

        public static string SumarString(this string A, string B) // sumar strings
        {
            string result = A + B;
            return result;
        }

        public static int NextIndex(this int CurrentIndex, int IndexLenght) // subir index 
        {
            CurrentIndex++;

            if(CurrentIndex > IndexLenght)
            {
                CurrentIndex = 0;
            }

            return CurrentIndex;
        }

        public static int BackIndex(this int currentindex, int IndexLenght)
        {
            currentindex--;

            if(currentindex <= 0)
            {
                currentindex = IndexLenght - 1;
            }

            return currentindex;
        }

        public static float GetMaximumNumber(this float[] currentnum, int IndexLenght)
        {
            float maximum = 0;
            float result= 0;

            for(int I = 0; I < IndexLenght; I++)
            {
                if (currentnum[I] > maximum)
                {
                    maximum = currentnum[I];
                    result = currentnum[I];
                }

            }

            return result;
        }


        public static float GetMaximumNumber(this List<float> currentnum, int IndexLenght)
        {
            float maximum = 0;
            float result = 0;

            for (int I = 0; I < IndexLenght; I++)
            {
                if (currentnum[I] > maximum)
                {
                    maximum = currentnum[I];
                    result = currentnum[I];
                }
            }
            return result;
        }



        public static float GetMinimumNumber(this float[] currentnum, int IndexLenght)
        {
            float minimum = float.MaxValue;
            float result= 0;

            for(int I = 0; I< IndexLenght; I++)
            {
                if (currentnum[I] < minimum) 
                {
                    minimum = currentnum[I];
                    result = currentnum[I];
                }

            }

            return result;

        }

        public static float GetMinimumNumber(this List<float> currentnum, int IndexLenght)
        {
            float minimum = float.MaxValue;
            float result = 0;

            for (int I = 0; I < IndexLenght; I++)
            {
                if (currentnum[I] < minimum)
                {
                    minimum = currentnum[I];
                    result = currentnum[I];
                }

            }

            return result;

        }

        public static GameObject GetClosestinList(this List<GameObject> col, Vector3 PositionCaller) 
        {
            float Closer = float.MaxValue;
            GameObject Result = null;

            for(int I = 0; I < col.Count;I++) 
            {
                float Distance = Vector3.Distance(col[I].transform.position, PositionCaller) ;
                if (Distance < Closer )
                {
                    Closer = Distance;
                    Result = col[I]; 

                }
            }

            return Result;

        }


        public static GameObject GetFurthestinList(this List<GameObject> col, Vector3 PositionCaller)
        {
            float furthest = 0;
            GameObject Result = null;

            for (int I = 0; I < col.Count; I++)
            {
                float Distance = Vector3.Distance(col[I].transform.position, PositionCaller);
                if (Distance > furthest)
                {
                    furthest = Distance;
                    Result = col[I];

                }
            }

            return Result;

        }

        //generico que devuelve un generic component (puede ser un gameobject, un transform, etc)
        public static T GetMostClosest<T>(this T[] col, Vector3 posPlayer, Func<T, bool> predicate) where T : Component
        {
            float mostClose = int.MaxValue;
            T mcVal = null;
            for (int i = 0; i < col.Length; i++)
            {
                if (predicate.Invoke(col[i]))
                {
                    float dist = Vector3.Distance(posPlayer, col[i].transform.position);
                    if (dist < mostClose)
                    {
                        mostClose = dist;
                        mcVal = col[i];
                    }

                }

            }

            return mcVal;
        }







    }
}
