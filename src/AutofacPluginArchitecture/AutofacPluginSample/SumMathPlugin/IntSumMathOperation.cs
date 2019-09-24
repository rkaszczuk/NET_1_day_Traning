﻿using PluginTemplate;
using System;

namespace SumMathPlugin
{
    public class IntSumMathOperation : IMathOperation<int>
    {
        public int Operation(int arg1, int arg2)
        {
            return arg1 + arg2;
        }
    }
}
