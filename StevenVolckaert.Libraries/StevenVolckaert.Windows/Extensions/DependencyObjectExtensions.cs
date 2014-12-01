using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace StevenVolckaert.Windows
{
    /// <summary>
    /// Provides extension methods for System.Windows.DependencyObject objects.
    /// </summary>
    public static class DependencyObjectExtensions
    {
        /* TODO In GetFocusableControls, assess if it is possible to use Linq and extension
         * methods provided by this class:
         * 
         * return dependencyObject.Children<Control>()
         *          .Where(x => x.IsKeyboardFocusable())
         *          .OrderBy(x => x.TabIndex);
         * 
         * Steven Volckaert. September 26, 2013.
         */

        /// <summary>
        /// Returns the DependencyObject's child System.Windows.FrameworkElement by examining the visual tree.
        /// </summary>
        /// <typeparam name="T">The type of the child element, which must inherit System.Windows.FrameworkElement.</typeparam>
        /// <param name="dependencyObject">The DependencyObject that this extension method affects.</param>
        /// <param name="name">The identifying name of the child FrameworkElement.</param>
        /// <returns>The child FrameworkElement, or <c>null</c> if it does not exist.</returns>
        public static T Child<T>(this DependencyObject dependencyObject, string name)
            where T : FrameworkElement
        {
            return dependencyObject.Children<T>().FirstOrDefault(x => x.Name == name);
        }

        /// <summary>
        /// Returns the DependencyObject's child System.Windows.FrameworkElement by examining the visual tree.
        /// </summary>
        /// <param name="dependencyObject">The DependencyObject that this extension method affects.</param>
        /// <param name="name">The identifying name of the child FrameworkElement.</param>
        /// <returns>The child FrameworkElement, or <c>null</c> if it does not exist.</returns>
        public static FrameworkElement Child(this DependencyObject dependencyObject, string name)
        {
            return dependencyObject.Child<FrameworkElement>(name);
        }

        /// <summary>
        /// Returns the DependencyObject's child objects by examining the visual tree.
        /// </summary>
        /// <param name="dependencyObject">The DependencyObject this extension method affects.</param>
        public static IEnumerable<DependencyObject> Children(this DependencyObject dependencyObject)
        {
            return Children(dependencyObject, Int32.MaxValue);
        }

        /// <summary>
        /// Returns the DependencyObject's child objects by examining the visual tree.
        /// </summary>
        /// <typeparam name="T">The type of the child objects.</typeparam>
        /// <param name="dependencyObject">The DependencyObject this extension method affects.</param>
        public static IEnumerable<T> Children<T>(this DependencyObject dependencyObject)
        {
            return Children(dependencyObject).OfType<T>();
        }

        /// <summary>
        /// Returns the DependencyObject's child objects by examining the visual tree.
        /// </summary>
        /// <param name="dependencyObject">The DependencyObject this extension method affects.</param>
        /// <param name="depth"></param>
        public static IEnumerable<DependencyObject> Children(this DependencyObject dependencyObject, int depth)
        {
            int numberOfChildren = VisualTreeHelper.GetChildrenCount(dependencyObject);

            for (int i = 0; i < numberOfChildren; i++)
            {
                var childDependencyObject = VisualTreeHelper.GetChild(dependencyObject, i);
                yield return childDependencyObject;

                if (depth > 0)
                    foreach (var child in Children(childDependencyObject, --depth))
                        yield return child;
            }
        }

        /// <summary>
        /// Returns the DependencyObject's child objects by examining the visual tree.
        /// </summary>
        /// <typeparam name="T">The type of the child objects.</typeparam>
        /// <param name="dependencyObject">The DependencyObject this extension method affects.</param>
        /// <param name="depth"></param>
        public static IEnumerable<T> Children<T>(this DependencyObject dependencyObject, int depth)
        {
            return Children(dependencyObject, depth).OfType<T>();
        }

        /// <summary>
        /// Returns the DependencyObject's parent objects by examining the visual tree.
        /// </summary>
        /// <param name="dependencyObject">The DependencyObject this extension method affects.</param>
        public static IEnumerable<DependencyObject> Parents(this DependencyObject dependencyObject)
        {
            var parentDependencyObject = VisualTreeHelper.GetParent(dependencyObject);

            while (parentDependencyObject != null)
            {
                yield return parentDependencyObject;
                parentDependencyObject = VisualTreeHelper.GetParent(parentDependencyObject);
            }
        }

        /// <summary>
        /// Returns the DependencyObject's parent objects by examining the visual tree.
        /// </summary>
        /// <typeparam name="T">The type of the parent objects.</typeparam>
        /// <param name="dependencyObject">The DependencyObject this extension method affects.</param>
        public static IEnumerable<T> Parents<T>(this DependencyObject dependencyObject)
        {
            return dependencyObject.Parents().OfType<T>();
        }

        /// <summary>
        /// Returns a collection of focusable controls, ordered by tab index.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        public static IEnumerable<Control> GetFocusableControls(this DependencyObject dependencyObject)
        {
            if (dependencyObject == null)
                return null;

            var controlStack = new Stack<Control>();
            var dependencyObjectStack = new Stack<DependencyObject>();
            dependencyObjectStack.Push(dependencyObject);

            while (dependencyObjectStack.Count > 0)
            {
                var currentDependencyObject = dependencyObjectStack.Pop();
                var control = currentDependencyObject as Control;

                if (control.IsKeyboardFocusable())
                    controlStack.Push(control);

                var numChildren = VisualTreeHelper.GetChildrenCount(currentDependencyObject);

                for (int i = 0; i < numChildren; i++)
                {
                    var childDependencyObject = VisualTreeHelper.GetChild(currentDependencyObject, i);
                    dependencyObjectStack.Push(childDependencyObject);
                }
            }

            return controlStack.OrderBy(x => x.TabIndex);
        }

        /// <summary>
        /// Returns a value that indicates whether the validation state of the ValidationStates visual state group is currently valid.
        /// </summary>
        /// <param name="dependencyObject">The element to perform the operation on.</param>
        /// <returns><c>true</c> if valid, <c>false</c> if not, or <c>null</c> if the dependency object doesn't have a
        /// visual state group named "ValidationStates", or if the validation state can't be verified.</returns>
        public static bool? IsValidationStateValid(this DependencyObject dependencyObject)
        {
            if (dependencyObject == null || VisualTreeHelper.GetChildrenCount(dependencyObject) == 0)
                return null;

            var templateRoot = VisualTreeHelper.GetChild(dependencyObject, 0) as FrameworkElement;

            if (templateRoot == null)
                return null;

            var validationStateGroup = VisualStateManager.GetVisualStateGroups(templateRoot).OfType<VisualStateGroup>().FirstOrDefault(x => x.Name == "ValidationStates");

            if (validationStateGroup == null || validationStateGroup.CurrentState == null)
                return null;

            return validationStateGroup.CurrentState.Name.Equals("Valid", System.StringComparison.OrdinalIgnoreCase)
                ? true
                : false;
        }
    }
}
