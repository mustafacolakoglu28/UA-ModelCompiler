using ModelCompiler.ToForms;
using System.Windows.Forms;

namespace OOI.ModelCompilerUI.ToForms
{
  internal static class ToFormsConverters
  {
    internal static DialogResult Convert(this DialogResultCompiler result)
    {
      switch (result)
      {
        case DialogResultCompiler.None:
          return DialogResult.None;

        case DialogResultCompiler.OK:
          return DialogResult.OK;

        case DialogResultCompiler.Cancel:
          return DialogResult.Cancel;

        case DialogResultCompiler.Abort:
          return DialogResult.Abort;

        case DialogResultCompiler.Retry:
          return DialogResult.Retry;

        case DialogResultCompiler.Ignore:
          return DialogResult.Ignore;

        case DialogResultCompiler.Yes:
          return DialogResult.Yes;

        case DialogResultCompiler.No:
          return DialogResult.No;

        default:
          return DialogResult.Yes;
      }
    }

    internal static DialogResultCompiler Convert(this DialogResult result)
    {
      switch (result)
      {
        case DialogResult.None:
          return DialogResultCompiler.None;

        case DialogResult.OK:
          return DialogResultCompiler.OK;

        case DialogResult.Cancel:
          return DialogResultCompiler.Cancel;

        case DialogResult.Abort:
          return DialogResultCompiler.Abort;

        case DialogResult.Retry:
          return DialogResultCompiler.Retry;

        case DialogResult.Ignore:
          return DialogResultCompiler.Ignore;

        case DialogResult.Yes:
          return DialogResultCompiler.Yes;

        case DialogResult.No:
          return DialogResultCompiler.No;

        default:
          return DialogResultCompiler.Yes;
      }
    }

    internal static MessageBoxButtons Convert(this MessageBoxButtonsCompiler value)
    {
      switch (value)
      {
        case MessageBoxButtonsCompiler.OK:
          return MessageBoxButtons.OK;

        case MessageBoxButtonsCompiler.OKCancel:
          return MessageBoxButtons.OKCancel;

        case MessageBoxButtonsCompiler.AbortRetryIgnore:
          return MessageBoxButtons.AbortRetryIgnore;

        case MessageBoxButtonsCompiler.YesNoCancel:
          return MessageBoxButtons.YesNoCancel;

        case MessageBoxButtonsCompiler.YesNo:
          return MessageBoxButtons.YesNo;

        case MessageBoxButtonsCompiler.RetryCancel:
          return MessageBoxButtons.RetryCancel;

        default:
          return MessageBoxButtons.OK;
      }
    }

    internal static MessageBoxIcon Convert(this MessageBoxIconCompiler value)
    {
      switch (value)
      {
        case MessageBoxIconCompiler.None:
          return MessageBoxIcon.None;

        case MessageBoxIconCompiler.Hand:
          return MessageBoxIcon.Hand;

        case MessageBoxIconCompiler.Stop:
          return MessageBoxIcon.Stop;

        case MessageBoxIconCompiler.Error:
          return MessageBoxIcon.Error;

        case MessageBoxIconCompiler.Question:
          return MessageBoxIcon.Question;

        case MessageBoxIconCompiler.Exclamation:
          return MessageBoxIcon.Exclamation;

        case MessageBoxIconCompiler.Warning:
          return MessageBoxIcon.Warning;

        case MessageBoxIconCompiler.Asterisk:
          return MessageBoxIcon.Asterisk;

        case MessageBoxIconCompiler.Information:
          return MessageBoxIcon.Information;

        default:
          return MessageBoxIcon.None;
      }
    }
  }
}