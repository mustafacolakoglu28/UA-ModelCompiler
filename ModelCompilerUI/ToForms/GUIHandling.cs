//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using ModelCompiler.ToForms;
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