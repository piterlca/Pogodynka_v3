using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pogodynka_v3
{
    public class StripMenuList : Controller
    {
        WinFormChart properView;
        List<ToolStripMenuItem> MenuItems =new List<ToolStripMenuItem>();
        ToolStripMenuItem ParentMenuItem = new ToolStripMenuItem();

        public StripMenuList(View view)
        {
            properView = (WinFormChart)view;
            ViewUsed = (WinFormChart)properView;

        }

        public void BindToMenuAndInit(ToolStripMenuItem menuPassed)
        {
            this.ParentMenuItem = menuPassed;
            
            foreach(string modelName in Model.getAvailableModels())
            {
                MenuItemInit(modelName, ParentMenuItem);
            }
        }

        private ToolStripMenuItem MenuItemInit(string nameDisplayed, 
               ToolStripMenuItem ParentMenuItem)
        {
            ToolStripMenuItem MenuItem = new ToolStripMenuItem(nameDisplayed, null, On_Item_Clicked);
            ParentMenuItem.DropDownItems.AddRange(new ToolStripItem[] { MenuItem });
            return MenuItem;
        }

        private void On_Item_Clicked(object sender, EventArgs e)
        {
            ToolStripMenuItem ItemClicked = (ToolStripMenuItem)sender;
            string seriesName = ItemClicked.Text;

            if (!properView.isSeriesDisplayed(seriesName))
            {
                executeCommand(ItemClicked.Text.ToLower() + "ADD" );
            }
            else
            {
                executeCommand(ItemClicked.Text.ToLower() + "DEL");
            }

            ItemClicked.Checked = properView.isSeriesDisplayed(seriesName);
        }

    }
}
