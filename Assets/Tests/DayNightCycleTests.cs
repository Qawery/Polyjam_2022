using System;
using System.Collections;
using System.Collections.Generic;
using Lifecycle;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;
using Random = UnityEngine.Random;

namespace Polyjam_2022.Tests
{
    public class DayNightCycleTests
    {
        
        [Test]
        public void CycleTest()
        {
            DayNightCycle cycle = new DayNightCycle();
            cycle.OnCycleChanged += Cycle_OnCycleChanged;

            var time = cycle.GetHourMinute();
            Assert.IsTrue(cycle.IsDay);
            Assert.IsTrue(time.Item1 == 0 && time.Item2 == 0);
            cycle.UpdateTimeProgress(0.75f);
            Assert.IsTrue(cycle.IsNight);
            time = cycle.GetHourMinute();
            Assert.IsTrue(time.Item1 == 18);

            cycle.UpdateTimeProgress(0.7f);
            Assert.IsTrue(cycle.GetHourMinute().Item1 < 24);

        }

        private void Cycle_OnCycleChanged(DayNightCycle cycle)
        {
            Assert.IsNotNull(cycle);
        }
    }
}
