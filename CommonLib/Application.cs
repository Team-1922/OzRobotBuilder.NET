using CommonLib.Controller;
using CommonLib.Model;
using CommonLib.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    /// <summary>
    /// The base class for applications consuming the MVC aspect of this library
    /// </summary>
    public abstract class Application
    { 
        /// <summary>
        /// The document manager (only support for SDI right now)
        /// </summary>
        public SDIDocumentManager DocumentManager { get; private set; }
        /// <summary>
        /// The Controller (interface between views and document)
        /// </summary>
        public RobotDocController Controller { get; private set; }
        /// <summary>
        /// The View Manager (mostly a container of views)
        /// </summary>
        public ViewManager ViewManager { get; private set; }

        private bool ShouldStop = false;

        /// <summary>
        /// <see cref="SDIDocumentManager"/> is abstract and the consuming application must override some of its functions
        /// </summary>
        /// <returns>the document manager created by the consuming application</returns>
        protected abstract SDIDocumentManager CreateDocumentManager();
        /// <summary>
        /// <see cref="RobotDocController"/> is abstract and the consuming application must override some of its functions
        /// </summary>
        /// <returns>the Controller created by the consuming application</returns>
        protected abstract RobotDocController CreateController();
        /// <summary>
        /// Since <see cref="ViewManager"/> is not abstract, the consuming application can, but need not override or extend its functions
        /// </summary>
        /// <returns>the view manager created by the consuming application or built in</returns>
        protected virtual ViewManager CreateViewManager()
        {
            return new ViewManager();
        }
        /// <summary>
        /// All other initialization not covered by <see cref="CreateViewManager()"/>, <see cref="CreateDocumentManager()"/>, or <see cref="CreateController"/>
        /// This is also where view creation should go
        /// </summary>
        /// <returns>whether or not initialization was a success</returns>
        public abstract bool Init();
        /// <summary>
        /// Called every update cycle
        /// </summary>
        /// <param name="time">the time since the application started</param>
        public abstract void Update(TimeSpan time);
        /// <summary>
        /// Free any unmanaged resources or call other cleanup code
        /// </summary>
        public abstract void Cleanup();

        /// <summary>
        /// Tells the application to stop completely
        /// </summary>
        public void Stop()
        {
            ShouldStop = true;
        }
        /// <summary>
        /// This is the only method to call from Application in the main loop.  Call this from Main
        /// </summary>
        public void Run(string[] args)
        {
            //create the three main components
            DocumentManager = CreateDocumentManager();
            Controller = CreateController();
            ViewManager = CreateViewManager();
            if(null == DocumentManager)
            {
                //TOOD: log something
                return;
            }
            if(null == Controller)
            {
                return;
            }
            if (null == ViewManager)
            {
                return;
            }

            //init is called after each one is constructed
            if(!Init())
            {
                //TODO: log something
                return;
            }

            //update Loop
            System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
            while(!ShouldStop)
            {
                Update(sw.Elapsed);
                //TODO: something else?
            }

            //loop over
            ViewManager.Destroy();
        }

    }
}
