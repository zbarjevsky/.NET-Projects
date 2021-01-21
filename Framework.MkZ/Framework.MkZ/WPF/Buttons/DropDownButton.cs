using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace MkZ.WPF.Buttons
{
    /// <summary>
    /// https://www.codeproject.com/Tips/1136681/WPF-Drop-Down-Menu-Button
    /// https://dotnetlearning.wordpress.com/2011/02/20/dropdown-menu-in-wpf/
    /// 
    /// Sometimes menu appears to the left, fix it here:
    /// In the Handedness section, place a check mark in the Left Handed option.
    /// https://www.sevenforums.com/tutorials/786-menus-open-left-right-side.html
    /// </summary>
    public class DropDownButton : Button
    {
        // *** Dependency Properties *** 

        // *** Constructors *** 
        public DropDownButton()
        {
            // Bind the ToogleButton.IsChecked property to the drop-down's IsOpen property 
            Binding binding = new Binding("Menu.IsOpen");
            binding.Source = this;
            //this.SetBinding(IsCheckedProperty, binding);

            DataContextChanged += (sender, args) =>
            {
                if (Menu != null)
                    Menu.DataContext = DataContext;
            };
        }

        // *** Properties *** 
        public ContextMenu Menu
        {
            get { return (ContextMenu)GetValue(MenuProperty); }
            set { SetValue(MenuProperty, value); }
        }
        public static readonly DependencyProperty MenuProperty = DependencyProperty.Register(
            nameof(Menu), typeof(ContextMenu), typeof(DropDownButton), new UIPropertyMetadata(null, OnMenuChanged));

        private static void OnMenuChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dropDownButton = (DropDownButton)d;
            var contextMenu = (ContextMenu)e.NewValue;
            contextMenu.DataContext = dropDownButton.DataContext;
        }

        // *** Overridden Methods *** 
        protected override void OnClick()
        {
            if (Menu != null)
            {
                this.IsEnabled = false;

                // If there is a drop-down assigned to this button, then position and display it 
                Menu.PlacementTarget = this;
                Menu.Placement = PlacementMode.Bottom;
                Menu.IsOpen = true;  //!Menu.IsOpen;

                System.Diagnostics.Debug.WriteLine("Options clicked: " + Menu.IsOpen);

                this.IsEnabled = true;
            }
        }
    }
}
