using Microsoft.Deployment.WindowsInstaller;
using System.Windows.Forms;

namespace ProductActivationAction
{
    public class CustomActions
    {
        [CustomAction]
        public static ActionResult ShowLicenseInfo(Session session)
        {
            frmLicenseInfo frmInfo = new frmLicenseInfo();
           
            if (frmInfo.ShowDialog() == DialogResult.Cancel)
            {
                return ActionResult.UserExit;
            }

            return ActionResult.Success;
        }
    }
}
