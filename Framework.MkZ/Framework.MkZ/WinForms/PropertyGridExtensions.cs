using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MkZ.WinForms
{
    public static class PropertyGridExtensions
    {
        /// <summary>
        /// Moves the splitter to the supplied horizontal position.
        /// </summary>
        /// <param name="propertyGrid">The property grid.</param>
        /// <param name="xpos">The horizontal position.</param>
        public static void MoveSplitterTo(this PropertyGrid propertyGrid, int xpos)
        {
            //System.Windows.Forms.PropertyGridInternal.PropertyGridView
            object gridView = GetPropertyGridView(propertyGrid);

            //private void MoveSplitterTo(int xpos);
            const BindingFlags flags = BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance;
            Type type = gridView.GetType();
            MethodInfo method = type.GetMethod("MoveSplitterTo", flags);

            method.Invoke(gridView, new Object[] { xpos });
        }

        public static void SelectItem(this PropertyGrid propertyGrid, string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                GridItem gi = propertyGrid.EnumerateAllItems().First((item) =>
                                item.PropertyDescriptor != null &&
                                item.PropertyDescriptor.Name == name);

                // select it
                propertyGrid.Focus();
                gi.Select();
            }
        }

        public static void ExpandGridItem(this PropertyGrid propertyGrid, string name)
        {
            GridItem root = propertyGrid.SelectedGridItem;
            //Get the parent
            while (root.Parent != null)
                root = root.Parent;

            ExpandGridItem(root, name);
        }

        public static void ExpandGridGroup(this PropertyGrid propertyGrid, string groupName)
        {
            GridItem root = propertyGrid.SelectedGridItem;
            //Get the parent
            while (root.Parent != null)
                root = root.Parent;

            if (root != null)
            {
                foreach (GridItem g in root.GridItems)
                {
                    if (g.GridItemType == GridItemType.Category && g.Label == groupName)
                    {
                        g.Expanded = true;
                        break;
                    }
                }
            }
        }

        private static void ExpandGridItem(GridItem root, string name)
        {
            if (root != null)
            {
                foreach (GridItem g in root.GridItems)
                {
                    ExpandGridItem(g, name);
                    if (g.Label == name)
                        g.Expanded = true;
                }
            }
        }

        /// <summary>
        /// Gets the (private) PropertyGridView instance.
        /// </summary>
        /// <param name="propertyGrid">The property grid.</param>
        /// <returns>The PropertyGridView instance.</returns>
        private static object GetPropertyGridView(this PropertyGrid propertyGrid)
        {
            //private PropertyGridView GetPropertyGridView();
            //PropertyGridView is an internal class...
            Type type = typeof(PropertyGrid);
            BindingFlags flags = BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Instance;
            object propertyGridView = type.InvokeMember("gridView", flags, null, propertyGrid, null);
            return propertyGridView;
        }

        /// <summary>
        /// Gets the width of the left column.
        /// </summary>
        /// <param name="propertyGrid">The property grid.</param>
        /// <returns>
        /// The width of the left column.
        /// </returns>
        public static int GetInternalLabelWidth(this PropertyGrid propertyGrid)
        {
            //System.Windows.Forms.PropertyGridInternal.PropertyGridView
            object gridView = GetPropertyGridView(propertyGrid);

            const BindingFlags flags = BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Instance;

            //protected int InternalLabelWidth
            PropertyInfo prop = gridView.GetType().GetProperty("InternalLabelWidth", flags);
            object internalLabelWidth = prop.GetValue(gridView, BindingFlags.GetProperty, null, null, null);
            return (int)internalLabelWidth;
        }

        public static IEnumerable<GridItem> EnumerateAllItems(this PropertyGrid grid)
        {
            if (grid == null)
                yield break;

            // get to root item
            GridItem start = grid.SelectedGridItem;
            while (start.Parent != null)
            {
                start = start.Parent;
            }

            foreach (GridItem item in start.EnumerateAllItems())
            {
                yield return item;
            }
        }

        public static IEnumerable<GridItem> EnumerateAllItems(this GridItem item)
        {
            if (item == null)
                yield break;

            yield return item;
            foreach (GridItem child in item.GridItems)
            {
                foreach (GridItem gc in child.EnumerateAllItems())
                {
                    yield return gc;
                }
            }
        }
    }
}
