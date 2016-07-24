using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.Controller;
using CommonLib.Model;
using System.Windows.Forms;
using OzRobotBuilder.NET.Views;

namespace OzRobotBuilder.NET
{
    /// <summary>
    /// The overriden Application for the CommonLib MVC pattern
    /// </summary>
    public class RobotBuilderApplication : CommonLib.Application
    {
        /// <summary>
        /// Nothing really needs to be done here yet
        /// </summary>
        public override void Cleanup()
        {
        }
        /// <summary>
        /// Create the window and run it
        /// </summary>
        /// <returns>true</returns>
        public override bool Init()
        {
            CommonLib.Model.Serializers.BuiltInLoaders.RegisterLoaders(DocumentManager);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var view = new MainView();
            view.Init(this);
            ViewManager.Views.Add("MainView", view);
            return true;
        }
        /// <summary>
        /// This locks up the application, therefore we MUST set the application to after the application runs
        /// </summary>
        /// <param name="time">time since program started</param>
        public override void Update(TimeSpan time)
        {
            ViewManager.Update(time);
            Application.Run(ViewManager.Views["MainView"] as MainView);
            Stop();
        }
        /// <summary>
        /// Creates a controller which opens with a File Open dialog
        /// </summary>
        /// <returns>the controller</returns>
        protected override RobotDocController CreateController()
        {
            return new KeyValueController(this);
        }
        /// <summary>
        /// Creates a folder document manager
        /// </summary>
        /// <returns>the document manager</returns>
        protected override SDIDocumentManager CreateDocumentManager()
        {
            return new FolderDocumentManager();
        }
    }
}
