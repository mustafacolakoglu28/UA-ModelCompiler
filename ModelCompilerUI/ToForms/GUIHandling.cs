//__________________________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System;
using System.Windows.Forms;

namespace OOI.ModelCompilerUI.ToForms
{
  internal class GUIHandling : IGUIHandling
  {
    public void Show(string message)
    {
      MessageBox.Show(message);
    }

    public void Show(string message, string caption)
    {
      MessageBox.Show(message, caption);
    }

    public DialogResultCompiler Show(string text, string caption, MessageBoxButtonsCompiler buttons, MessageBoxIconCompiler icon)
    {
      return MessageBox.Show(text, caption, buttons.Convert(), icon.Convert()).Convert();
    }

    public void ShowDialog(Exception e)
    {
      ExceptionDlg dialog = new ExceptionDlg(e);
      dialog.ShowDialog();
    }
  }
}