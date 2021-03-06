// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using AdventureWorks.UILogic.Models;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace AdventureWorks.Shopper.Behaviors
{
    public class ComboBoxKeyboardSelection : Behavior<ComboBox>
    {
        protected override void OnAttached()
        {
            ComboBox comboBox = this.AssociatedObject;

            if (comboBox != null)
            {
                comboBox.KeyUp += comboBox_KeyUp;
            }
        }

        private void comboBox_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            foreach (var item in comboBox.Items)
            {
                var comboBoxItemValue = item as ComboBoxItemValue;
                if (comboBoxItemValue != null && comboBoxItemValue.Value.StartsWith(e.Key.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    comboBox.SelectedItem = comboBoxItemValue;
                    return;
                }
            }
        }
        protected override void OnDetached()
        {
            ComboBox comboBox = this.AssociatedObject;

            if (comboBox != null)
            {
                comboBox.KeyUp -= comboBox_KeyUp;
            }
        }
    }
}
