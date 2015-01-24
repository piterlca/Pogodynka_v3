using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pogodynka_v3
{
    public class StripMenuList : Controller
    {
        List<ToolStripMenuItem> MenuItems =new List<ToolStripMenuItem>();
        ToolStripMenuItem ParentMenuItem = new ToolStripMenuItem();

        public StripMenuList(View view)
        {
            ViewUsed = view;
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
            string modelID = ItemClicked.Text;

            if (!ViewUsed.isModelBeingViewed(modelID))
            {
                executeCommand(ItemClicked.Text.ToLower() + "ADD" );
            }
            else
            {
                executeCommand(ItemClicked.Text.ToLower() + "DEL");
            }
            ItemClicked.Checked = ViewUsed.isModelBeingViewed(modelID);
        }

    }
}
