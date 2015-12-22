// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using AdventureWorks.Shopper.Behaviors;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace AdventureWorks.Shopper.Tests.Behaviors
{
    [TestClass]
    public class BehaviorFixture
    {
        //[TestMethod]
        //public void BehaviorShouldExposeAttachedObject()
        //{
        //    var testObject = new TextBlock();
        //    var target = new TestBehavior<TextBlock>();
        //    target.Attach(testObject);

        //    Assert.AreSame(testObject, target.AssociatedObject);
        //}
    }

    class TestBehavior<T> : Behavior<T> where T : DependencyObject
    {
        protected override void OnAttached()
        {
        }

        protected override void OnDetached()
        {
        }
    }
}
