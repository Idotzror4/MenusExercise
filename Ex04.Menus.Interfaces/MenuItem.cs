﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Interfaces
{
    public class MenuItem
    {
        private readonly string r_MenuTitle;
        private MenuItem m_PreviousMenu;
        private List<MenuItem> m_ItemsBelongThisLevel = new List<MenuItem>();
        private List<ISelectedLeafMenuItem> m_Listeners = new List<ISelectedLeafMenuItem>();
        private const string k_DottedLine = "=======================";
        private const string k_Arrow = "->";

        public MenuItem PreviousMenu
        {
            get { return m_PreviousMenu; }
            set { m_PreviousMenu = value; }
        }

        public MenuItem(string i_MenuTitle, MenuItem i_PreviousMenu)
        {
            r_MenuTitle = i_MenuTitle;
            this.m_PreviousMenu = i_PreviousMenu;
        }

        public int ItemsBelongThisLevelLenght
        {
            get { return m_ItemsBelongThisLevel.Count; }
        }

        public void AddListener(ISelectedLeafMenuItem i_NewListener)
        {
            m_Listeners.Add(i_NewListener);
        }

        private void notifyListenersThatAMenuWasChosen()
        {
            foreach (ISelectedLeafMenuItem listener in m_Listeners)
            {
                listener.ExecuteMenuItemAction();
            }
        }

        public MenuItem GetSubMenuChosenByUser(int i_Index)
        {
            return m_ItemsBelongThisLevel[i_Index];
        }

        public MenuItem AddMenuToCurrentLevelMenus(string i_MenuTitle)
        {
            MenuItem newMenu = new MenuItem(i_MenuTitle, this);

            m_ItemsBelongThisLevel.Add(newMenu);

            return newMenu;
        }

        public void SelectedOption()
        {
            Console.Clear();
            notifyListenersThatAMenuWasChosen();
            Console.WriteLine(string.Format("Press any key to continue..."));
            Console.ReadKey();
        }

        public void PrintMenu(string i_QuitTheMenu)
        {
            Console.Clear();
            Console.WriteLine($@"**{r_MenuTitle}**
{k_DottedLine}");

            for (int i = 0; i < m_ItemsBelongThisLevel.Count; i++)
            {
                Console.WriteLine($@"{i + 1} {k_Arrow} {m_ItemsBelongThisLevel[i].r_MenuTitle}");
            }

            Console.WriteLine($@"0 {k_Arrow} {i_QuitTheMenu}
Enter your request: (1 to {m_ItemsBelongThisLevel.Count} or press '0' to {i_QuitTheMenu})");
        }
    }
}
